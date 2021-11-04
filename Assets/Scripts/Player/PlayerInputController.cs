using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PrototypeInputs inputControls;
    private PlayerMovement movement;
    private FungoVision vision;
    private CameraProjectionSwapper projectionSwapper;
    
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        vision = GetComponent<FungoVision>();
        projectionSwapper = Camera.main.GetComponent<CameraProjectionSwapper>();
        
        inputControls = new PrototypeInputs();
        inputControls.Player.MoveHorizontal.performed += OnMoveDirH;
        inputControls.Player.MoveHorizontal.canceled += OnMoveStopH;
        inputControls.Player.MoveVertical.performed += OnMoveDirV;
        inputControls.Player.MoveVertical.canceled += OnMoveStopV;
        inputControls.Player.Dash.started += OnDash;
        inputControls.Player.PadMove.performed += OnMove;
        inputControls.Player.PadMove.canceled += OnMoveStop;
        inputControls.Player.SwapProjection.started += OnCameraProjectionChange;
        inputControls.Player.Interact.started += GetComponent<PlayerInteraction>().OnTryInteract;
        inputControls.Player.Fungovision.started += OnFungovisionToggle;
    }

    private void OnEnable()
    {
        inputControls.Player.MoveHorizontal.Enable();
        inputControls.Player.MoveVertical.Enable();
        inputControls.Player.Dash.Enable();
        inputControls.Player.Interact.Enable();
        inputControls.Player.SwapProjection.Enable();
        inputControls.Player.PadMove.Enable();
        inputControls.Player.Fungovision.Enable();
    }

    private void OnDisable()
    {
        inputControls.Player.MoveHorizontal.Disable();
        inputControls.Player.MoveVertical.Disable();
        inputControls.Player.SwapProjection.Disable();
        inputControls.Player.Interact.Disable();
        inputControls.Player.Dash.Disable();
        inputControls.Player.PadMove.Disable();
        inputControls.Player.Fungovision.Disable();
    }

    private void OnMoveDirV(InputAction.CallbackContext context) =>
        movement.AddVerticalMovement(context.ReadValue<float>());
    
    private void OnMoveDirH(InputAction.CallbackContext context) =>
        movement.AddHorizontalMovement(context.ReadValue<float>());

    private void OnMove(InputAction.CallbackContext context) => 
        movement.AddMovement(context.ReadValue<Vector2>());

    private void OnMoveStopH(InputAction.CallbackContext context) => movement.StopHorizontalMovement();

    private void OnMoveStopV(InputAction.CallbackContext context) => movement.StopVerticalMovement();

    private void OnMoveStop(InputAction.CallbackContext context) => movement.StopMovement();

    private void OnDash(InputAction.CallbackContext context) => movement.Dash();

    private void OnFungovisionToggle(InputAction.CallbackContext obj) => vision.ToggleFungoVision();
    
    private void OnCameraProjectionChange(InputAction.CallbackContext obj) => projectionSwapper.ChangeCameraProjection();
}
