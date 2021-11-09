using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Vector3 exitDirection;
    [SerializeField] private UnityEvent zoneEnterEvent, zoneLeaveEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (GetAngle(other) < 90f) return;
        
        zoneEnterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (GetAngle(other) < 90f) return;
        
        zoneLeaveEvent.Invoke();
    }
    
    private float GetAngle(Collider other) => Vector3.Angle(other.transform.position - transform.position, exitDirection);

    private void OnValidate() => GetComponent<BoxCollider>().isTrigger = true;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + exitDirection.normalized * 5f);
    }
}
