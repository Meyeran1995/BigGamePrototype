using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class CameraZone : MonoBehaviour
{
    [SerializeField] private Vector3 exitDirection;
    [SerializeField] private CinemachineVirtualCamera entryCam, exitCam;

    private void Awake()
    {
        if (entryCam)
            entryCam.enabled = false;
        if (exitCam)
            exitCam.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(entryCam)
            entryCam.enabled = true;
        if (exitCam)
            exitCam.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        float angle = Vector3.Angle(other.transform.position - transform.position, exitDirection);

        if (angle < 90f)
        {
            if (exitCam)
                exitCam.enabled = true;
        }
        else if (entryCam)
        {
            entryCam.enabled = false;
        }
    }

    private void OnValidate() => GetComponent<BoxCollider>().isTrigger = true;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + exitDirection.normalized * 5f);
    }
}
