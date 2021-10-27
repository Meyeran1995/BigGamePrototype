using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraProjectionSwapper : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera followCamera;
    private float fieldOfView, orthographicSize;
    private bool isOrthographic;
    private Camera mainCam;

    private void Awake()
    {
        float distance = followCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
        mainCam = Camera.main;
        isOrthographic = mainCam.orthographic;
        
        if (isOrthographic)
        {
            orthographicSize = followCamera.m_Lens.OrthographicSize;
            fieldOfView = 2f * Mathf.Atan(orthographicSize / distance) / Mathf.Deg2Rad;
        }
        else
        {
            fieldOfView = followCamera.m_Lens.FieldOfView;
            // halfFrustumHeight is represented by orthographic size
            orthographicSize = distance * Mathf.Tan(fieldOfView * 0.5f * Mathf.Deg2Rad);
        }
    }

    public void OnCameraProjectionChange(InputAction.CallbackContext obj)
    {
        isOrthographic = !isOrthographic;
        mainCam.orthographic = isOrthographic;

        if (isOrthographic)
        {
            followCamera.m_Lens.OrthographicSize = orthographicSize;
        }
        else
        {
            followCamera.m_Lens.FieldOfView = fieldOfView;
        }
    }
}
