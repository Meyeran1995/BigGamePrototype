using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleZoneConnector : MonoBehaviour
{
    public static readonly List<PuzzleZone> Zones = new List<PuzzleZone>();
    private static PuzzleZone lastZone, currentZone;

    [Header("Connections")]
    [SerializeField] private Material connectionMaterial;
    private static Material staticMaterial;
    private static Transform rendererParent;
    private static PlayerPuzzleTrail playerTrail;

    public static bool FirstZoneAcquired => currentZone != null;

    private void Awake()
    {
        staticMaterial = connectionMaterial;
        rendererParent = GameObject.FindGameObjectWithTag("Lineparent").transform;
        playerTrail = GameObject.FindGameObjectWithTag("Trail").GetComponent<PlayerPuzzleTrail>();
    }

    public static void OnZonePulled(PuzzleZone zone)
    {
        currentZone = zone;
        playerTrail.ConnectToPlayer(zone);
    }
    
    public static void OnZoneConnectionAttempted(PuzzleZone zone)
    {
        if (zone == currentZone || !currentZone.CanBeConnectedToZone(zone))
        {
            ClearActiveZones();
            return;
        }
        
        OnZonesConnected(zone);
    }

    private static void OnZonesConnected(PuzzleZone zone)
    {
        lastZone = currentZone;
        currentZone = zone;
        
        lastZone.SetToConnected();
        
        var lastZonePosition = lastZone.transform.position;
        var currentZonePosition = currentZone.transform.position;
        
        LineRenderer line = new GameObject($"Connector {lastZonePosition} to {currentZonePosition}", typeof(LineRenderer))
                .GetComponent<LineRenderer>();
        
        line.SetPosition(0, lastZonePosition);
        line.SetPosition(1, currentZonePosition);
        line.sharedMaterial = staticMaterial;
        line.transform.SetParent(rendererParent);
        
        ClearActiveZones();
        CheckForPuzzleSolveProcess();
    }

    private static void CheckForPuzzleSolveProcess()
    {
        if (Zones.Any(zone => !zone.IsConnected)) return;

        Debug.Log("Puzzle solved");
        AkSoundEngine.PostEvent("Play_SFX_PuzzleSolved", rendererParent.gameObject);
        playerTrail.Disconnect();
    }

    public static void ResetConnections()
    {
        foreach (Transform line in rendererParent)
        {
            Destroy(line.gameObject);
        }

        foreach (var zone in Zones)
        {
            zone.ResetConnectionStatus();
        }
        
        ClearActiveZones();
    }

    private static void ClearActiveZones()
    {
        currentZone = null;
        lastZone = null;
        playerTrail.Disconnect();
    }
}
