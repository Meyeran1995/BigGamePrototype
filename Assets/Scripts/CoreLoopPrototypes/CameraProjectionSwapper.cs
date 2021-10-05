using UnityEngine;
using UnityEngine.InputSystem;

public class CameraProjectionSwapper : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float distance;
    private float fieldOfView, orthographicSize;

    private void Awake()
    {
        mainCamera = Camera.main;
        fieldOfView = mainCamera.fieldOfView;
        // halfFrustumHeight is represented by orthographic size
        orthographicSize = distance * Mathf.Tan(fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    public void OnCameraProjectionChange(InputAction.CallbackContext obj)
    {
        mainCamera.orthographic = !mainCamera.orthographic;

        if (mainCamera.orthographic)
        {
            mainCamera.orthographicSize = orthographicSize;
        }
        else
        {
            mainCamera.fieldOfView = fieldOfView;
        }
    }
}
