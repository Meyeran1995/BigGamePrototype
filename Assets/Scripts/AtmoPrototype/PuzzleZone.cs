using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PuzzleZone : MonoBehaviour, IEquatable<PuzzleZone>
{
    [SerializeField] private int zoneIndex;
    [SerializeField] private PuzzleZone partner;
    private int zonePartnerIndex;
    public int ZoneIndex => zoneIndex;
    public int PartnerIndex => zonePartnerIndex;
    
    protected static PlayerMovement movement;

    [SerializeField] private bool isConnected;
    public bool IsConnected => isConnected;

    private void Awake()
    {
        if (partner)
        {
            zonePartnerIndex = partner.zoneIndex;
        }
        else
        {
            isConnected = true;
        }
        
        if (!movement)
            movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start() => PuzzleZoneConnector.Zones.Add(this);

    public void SetToConnected() => isConnected = true;
    
    public void ResetConnectionStatus() => isConnected = !partner;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!movement.isDashing && !PuzzleZoneConnector.FirstZoneAcquired) return;
        PuzzleZoneConnector.OnZoneEntered(this);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!movement.isDashing) return;
        PuzzleZoneConnector.OnZoneEntered(this);
    }

    public bool CanBeConnectedToZone(PuzzleZone other) => other != null && partner != null && other.zoneIndex == zonePartnerIndex;
    
    public bool Equals(PuzzleZone other) => other != null && other.zoneIndex == zoneIndex && other.zonePartnerIndex == zonePartnerIndex;
    
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

}
