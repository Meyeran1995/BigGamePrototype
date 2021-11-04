using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PuzzleZone : MonoBehaviour, IEquatable<PuzzleZone>
{
    protected static PlayerMovement movement;
    [SerializeField] private int zoneIndex;
    [SerializeField] private PuzzleZone partner;
    [SerializeField] private bool isConnected;
    
    public int ZoneIndex => zoneIndex;
    public int PartnerIndex => partner != null ? partner.zoneIndex : -1;

    public PuzzleZone Partner => partner;
    public bool IsConnected => isConnected || partner == null;

    private void Awake()
    {
        if (!movement)
            movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start() => PuzzleZoneConnector.Zones.Add(this);

    public void SetToConnected() => isConnected = true;
    
    public void ResetConnectionStatus() => isConnected = !partner;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!PuzzleZoneConnector.FirstZoneAcquired) return;
        PuzzleZoneConnector.OnZoneConnectionAttempted(this);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (isConnected || !movement.isDashing) return;
        PuzzleZoneConnector.OnZonePulled(this);
    }

    public bool CanBeConnectedToZone(PuzzleZone other) => other != null && partner != null && other.zoneIndex == partner.zoneIndex;
    
    public bool Equals(PuzzleZone other) => other != null && other.zoneIndex == zoneIndex && other.PartnerIndex == partner.zoneIndex;

    #region EditorOnly

#if UNITY_EDITOR

    protected virtual void OnValidate()
    {
        GetComponent<SphereCollider>().isTrigger = true;
        gameObject.name = $"Puzzle zone {zoneIndex}";
    }

    protected virtual void OnDrawGizmos()
    {
        var currentPosition = transform.position;
        Handles.Label(currentPosition, zoneIndex.ToString());
        if(partner)
            Handles.DrawLine(currentPosition, partner.transform.position);
    }
    
#endif

    #endregion
}
