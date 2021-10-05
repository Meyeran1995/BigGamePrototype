using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PrototypeInputs inputControls;
    private Rigidbody rb;
    [SerializeField] private Puzzle activePuzzle;

    [Range(1f, 10f)]
    [Header("Basic Movement")]
    public float moveSpeed;
    protected static Vector3 moveDir;
    protected bool isMoving;
    public static Vector3 MoveDir => moveDir;

    [Header("Dashing")]
    public float dashPower;
    [SerializeField] protected int dashFrames;
    [HideInInspector]
    public bool isDashing;
    [SerializeField] protected DashEffect effect;

    private void Awake()
    {
        if (!effect)
            effect = GetComponent<DashEffect>();
        DashValueMapper.Initialize();

        inputControls = new PrototypeInputs();
        inputControls.Player.MoveHorizontal.performed += OnMoveDirH;
        inputControls.Player.MoveHorizontal.canceled += OnMoveStopH;
        inputControls.Player.MoveVertical.performed += OnMoveDirV;
        inputControls.Player.MoveVertical.canceled += OnMoveStopV;
        inputControls.Player.Dash.started += OnDash;
        inputControls.Player.SwapProjection.started += Camera.main.GetComponent<CameraProjectionSwapper>().OnCameraProjectionChange;
        inputControls.Player.Interact.started += GetComponent<PlayerInteraction>().OnTryInteract;
    }

    protected virtual void Start() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        if (!isMoving || isDashing) return;
        transform.hasChanged = true;
        Move();
    }

    protected virtual void Move() => rb.MovePosition(rb.position + moveDir * Time.fixedDeltaTime);

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

    /// <summary>
    /// Adds vertical movement to this players MoveVector
    /// </summary>
    /// <param name="context"></param>
    protected virtual void OnMoveDirV(InputAction.CallbackContext context)
    {
        moveDir.z -= context.ReadValue<float>() * moveSpeed;
        isMoving = true;
    }

    /// <summary>
    /// Adds horizontal movement to this players MoveVector
    /// </summary>
    /// <param name="context"></param>
    private void OnMoveDirH(InputAction.CallbackContext context)
    {
        moveDir.x -= context.ReadValue<float>() * moveSpeed;
        isMoving = true;
    }

    /// <summary>
    /// Stops horizontal movement
    /// </summary>
    /// <param name="context"></param>
    private void OnMoveStopH(InputAction.CallbackContext context)
    {
        moveDir.x = 0f;
        isMoving = moveDir != Vector3.zero;
    }

    /// <summary>
    /// Stops vertical movement
    /// </summary>
    /// <param name="context"></param>
    protected virtual void OnMoveStopV(InputAction.CallbackContext context)
    {
        moveDir.z = 0f;
        isMoving = moveDir != Vector3.zero;
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        if (!isMoving) return;

        isDashing = true;
        StartCoroutine(PerformDash());

        if (!activePuzzle) return;
        DashValueMapper.OnDashCalcValues();
        StartCoroutine(activePuzzle.ReactToDash());
    }

    protected virtual IEnumerator PerformDash()
    {
        CinemachineShake.Instance.ShakeCamera(4f, 0.1f);

        var targetDir = moveDir;
        targetDir.Normalize();
        var targetPos = transform.position + targetDir * dashPower;
        var dashIncrement = (targetPos - transform.position).magnitude / dashFrames;

        effect.ToggleEffect();
        effect.OrderShadows(targetDir);

        for (int i = dashFrames; i > 0; i--)
        {
            rb.MovePosition(rb.position + targetDir * dashIncrement);
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
        effect.ToggleEffect();
    }
}
