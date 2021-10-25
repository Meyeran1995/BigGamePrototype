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

    public static void OnZoneEntered(PuzzleZone zone)
    {
        if (currentZone)
        {
            if (zone == currentZone) return;
            
            lastZone = currentZone;
            currentZone = zone;
            OnZonesConnected();
        }
        else
        {
            currentZone = zone;
            playerTrail.ConnectToPlayer(zone);
        }
    }

    private static void OnZonesConnected()
    {
        if (lastZone.IsConnected) return;
        
        if (!lastZone.CanBeConnectedToZone(currentZone))
        {
            ClearActiveZones();
            return;
        }

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
        playerTrail.Disconnect();
    }

    public static void ResetConnections()
    {
        foreach (Transform linerenderer in rendererParent)
        {
            Destroy(linerenderer.gameObject);
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
