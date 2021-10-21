using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PrototypeInputs inputControls;
    private PlayerMovement movement;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();

        inputControls = new PrototypeInputs();
        inputControls.Player.MoveHorizontal.performed += OnMoveDirH;
        inputControls.Player.MoveHorizontal.canceled += OnMoveStopH;
        inputControls.Player.MoveVertical.performed += OnMoveDirV;
        inputControls.Player.MoveVertical.canceled += OnMoveStopV;
        inputControls.Player.Dash.started += OnDash;
        inputControls.Player.SwapProjection.started += Camera.main.GetComponent<CameraProjectionSwapper>().OnCameraProjectionChange;
        inputControls.Player.Interact.started += GetComponent<PlayerInteraction>().OnTryInteract;
    }
    private void OnEnable()
    {
        inputControls.Player.MoveHorizontal.Enable();
        inputControls.Player.MoveVertical.Enable();
        inputControls.Player.Dash.Enable();
        inputControls.Player.Interact.Enable();
        inputControls.Player.SwapProjection.Enable();
    }

    private void OnDisable()
    {
        inputControls.Player.MoveHorizontal.Disable();
        inputControls.Player.MoveVertical.Disable();
        inputControls.Player.SwapProjection.Disable();
        inputControls.Player.Interact.Disable();
        inputControls.Player.Dash.Disable();
    }

    protected virtual void OnMoveDirV(InputAction.CallbackContext context) =>
        movement.AddVerticalMovement(context.ReadValue<float>());
    
    private void OnMoveDirH(InputAction.CallbackContext context) =>
        movement.AddHorizontalMovement(context.ReadValue<float>());

    private void OnMoveStopH(InputAction.CallbackContext context) => movement.StopHorizontalMovement();

    protected virtual void OnMoveStopV(InputAction.CallbackContext context) => movement.StopVerticalMovement();

    private void OnDash(InputAction.CallbackContext context) => movement.Dash();
}
