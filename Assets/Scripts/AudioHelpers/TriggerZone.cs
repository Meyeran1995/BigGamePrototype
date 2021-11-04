using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Vector3 exitDirection;
    [SerializeField] private UnityEvent zoneEvent;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        float angle = Vector3.Angle(other.transform.position - transform.position, exitDirection);

        // if (angle < 90f)
        // {
        //     if (exitCam)
        //         exitCam.enabled = true;
        // }
        // else if (entryCam)
        // {
        //     entryCam.enabled = false;
        // }
    }

    private void OnValidate() => GetComponent<BoxCollider>().isTrigger = true;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + exitDirection.normalized * 5f);
    }
}
