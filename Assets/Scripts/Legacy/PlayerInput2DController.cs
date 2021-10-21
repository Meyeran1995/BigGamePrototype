using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput2DController : PlayerInputController
{
    private Rigidbody2D rb2D;

    // protected override void Start() => rb2D = GetComponent<Rigidbody2D>();
    //
    // protected override void Move() => rb2D.MovePosition(rb2D.position + (Vector2)moveDir * Time.fixedDeltaTime);
    //
    // protected override void OnMoveDirV(InputAction.CallbackContext context)
    // {
    //     moveDir.y += context.ReadValue<float>() * moveSpeed;
    //     isMoving = true;
    // }
    //
    // protected override void OnMoveStopV(InputAction.CallbackContext context)
    // {
    //     moveDir.y = 0f;
    //     isMoving = moveDir != Vector3.zero;
    // }
    //
    // protected override IEnumerator PerformDash()
    // {
    //     CinemachineShake.Instance.ShakeCamera(4f, 0.1f);
    //
    //     Vector2 targetDir = moveDir;
    //     targetDir.Normalize();
    //     var targetPos = transform.position + (Vector3)targetDir * dashPower;
    //     var dashIncrement = (targetPos - transform.position).magnitude / dashFrames;
    //
    //     effect.ToggleEffect();
    //     effect.OrderShadows(targetDir);
    //
    //     for (int i = dashFrames; i > 0; i--)
    //     {
    //         rb2D.MovePosition(rb2D.position + targetDir * dashIncrement);
    //         yield return new WaitForFixedUpdate();
    //     }
    //
    //     isDashing = false;
    //     effect.ToggleEffect();
    // }
}
