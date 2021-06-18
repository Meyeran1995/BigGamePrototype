using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PrototypeInputs inputControls;
    private Rigidbody2D rb;

    [Range(1f, 10f)]
    [Header("Basic Movement")]
    public float moveSpeed;
    private static Vector3 moveDir;
    private bool isMoving;
    public static Vector2 MoveDir => moveDir;

    [Header("Dashing")]
    public float dashPower;
    [SerializeField] private int dashFrames;
    [HideInInspector]
    public bool isDashing;

    [SerializeField] private Puzzle activePuzzle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DashValueMapper.Initialize();

        inputControls = new PrototypeInputs();
        inputControls.Player.MoveHorizontal.performed += OnMoveDirH;
        inputControls.Player.MoveHorizontal.canceled += OnMoveStopH;
        inputControls.Player.MoveVertical.performed += OnMoveDirV;
        inputControls.Player.MoveVertical.canceled += OnMoveStopV;
        inputControls.Player.Dash.started += OnDash;
    }

    private void FixedUpdate()
    {
        if (!isMoving || isDashing) return;

        transform.position += moveDir * Time.fixedDeltaTime;
        rb.velocity = Vector2.zero;
        rb.angularDrag = 0f;
    }

    public void OnEnable()
    {
        inputControls.Player.MoveHorizontal.Enable();
        inputControls.Player.MoveVertical.Enable();
        inputControls.Player.Dash.Enable();
    }

    private void OnDisable()
    {
        inputControls.Player.MoveHorizontal.Disable();
        inputControls.Player.MoveVertical.Disable();
        inputControls.Player.Dash.Disable();
    }

    /// <summary>
    /// Adds vertical movement to this players MoveVector
    /// </summary>
    /// <param name="context"></param>
    private void OnMoveDirV(InputAction.CallbackContext context)
    {
        moveDir.y += context.ReadValue<float>() * moveSpeed;
        isMoving = true;
    }

    /// <summary>
    /// Adds horizontal movement to this players MoveVector
    /// </summary>
    /// <param name="context"></param>
    private void OnMoveDirH(InputAction.CallbackContext context)
    {
        moveDir.x += context.ReadValue<float>() * moveSpeed;
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
    private void OnMoveStopV(InputAction.CallbackContext context)
    {
        moveDir.y = 0f;
        isMoving = moveDir != Vector3.zero;
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        if (!isMoving) return;

        isDashing = true;
        StartCoroutine(PerformDash());
        DashValueMapper.OnDashCalcValues();
        StartCoroutine(activePuzzle.ReactToDash());
    }

    private IEnumerator PerformDash()
    {
        var targetDir = moveDir;
        targetDir.Normalize();
        var targetPos = transform.position + targetDir * dashPower;
        var dashIncrement = (targetPos - transform.position).magnitude / dashFrames;

        for (int i = dashFrames; i > 0; i--)
        {
            rb.MovePosition(transform.position + targetDir * dashIncrement);
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
    }
}
