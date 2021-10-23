using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerCollision collision;
    
    [Header("Basic Movement")]
    [Range(1f, 20f)] public float moveSpeed;
    private Vector3 moveDir;
    private bool isMoving;
    [SerializeField] private bool invertMovement;

    [Header("Dashing")]
    [SerializeField] [Range(1.25f, 2f)]private float dashPower;
    [SerializeField] private int dashFrames;
    [HideInInspector] public bool isDashing;
    [SerializeField] private DashEffect effect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collision = GetComponent<PlayerCollision>();
    }
    
    private void FixedUpdate()
    {
        if (!isMoving || isDashing) return;
        transform.hasChanged = true;
        Move();
    }
    
    /// <summary>
    /// Moves into current direction, after probing for collision
    /// </summary>
    private void Move()
    {
        var currentMovement = moveDir * Time.fixedDeltaTime;
        
        if(collision.ProbeCollisionOnGroundPlane(currentMovement))
        {
            rb.MovePosition(collision.GetImpactPosition());
        }
        else
        {
            rb.MovePosition(rb.position + currentMovement);
        }
    }

    /// <summary>
    /// Adds vertical movement to this players MoveVector
    /// </summary>
    /// <param name="amount">Amount of movement to be added</param>
    public void AddVerticalMovement(float amount)
    {
        moveDir.z += invertMovement ? -amount * moveSpeed : amount * moveSpeed;
        isMoving = true;
    }
    
    /// <summary>
    /// Adds horizontal movement to this players MoveVector
    /// </summary>
    /// <param name="amount">Amount of movement to be added</param>
    public void AddHorizontalMovement(float amount)
    {
        moveDir.x += invertMovement ? -amount * moveSpeed : amount * moveSpeed;
        isMoving = true;
    }

    /// <summary>
    /// Stops movement on the vertical axis
    /// </summary>
    public void StopVerticalMovement()
    {
        moveDir.z = 0f;
        isMoving = moveDir.x != 0f;
    }
    
    /// <summary>
    /// Stops movement on the horizontal axis
    /// </summary>
    public void StopHorizontalMovement()
    {
        moveDir.x = 0f;
        isMoving = moveDir.z != 0f;
    }
    
    /// <summary>
    /// Performs a dash in movement direction. Can only be used whilst moving
    /// </summary>
    public void Dash()
    {
        if (!isMoving || isDashing) return;
        
        isDashing = true;
        StartCoroutine(PerformDash());
    }
    
    private IEnumerator PerformDash()
    {
        var startingPos = rb.position;
        var dashDirection = moveDir * dashPower;
        var targetPos = startingPos + dashDirection;
        var dashIncrement = dashDirection / dashFrames;
        bool hasCollided = false;
        float targetDist = 0f;

        //Debug.Log($"Attempting to dash from {startingPos} to {targetPos}");
        
        if(collision.ProbeCollisionOnGroundPlane(dashDirection))
        {
            targetPos = collision.GetImpactPosition();
            hasCollided = true;
            targetDist = (rb.position - targetPos).magnitude;
            
            if (rb.position == targetPos)
            {
                isDashing = false;
                //Debug.Log("We are directly in front of a wall");
                yield break;
            }
        }

        effect.ToggleEffect();
        effect.OrderShadows(moveDir.normalized);

        Vector3 nextPos = rb.position;
        
        for (int i = dashFrames; i > 0; i--)
        {
            nextPos += dashIncrement;
            
            if (hasCollided)
            {
                if (Vector3.Distance(startingPos, nextPos) > targetDist)
                {
                    //Debug.Log($"Overshot targeted position {targetPos}, with nextPos {nextPos}");
                    rb.MovePosition(targetPos);
                    yield return new WaitForFixedUpdate();
                    ExitDash();
                    yield break;
                }
            }
            
            //Debug.Log($"Going to {nextPos}");
            rb.MovePosition(nextPos);
            yield return new WaitForFixedUpdate();
        }

        ExitDash();
    }

    private void ExitDash()
    {
        isDashing = false;
        effect.ToggleEffect();
    }
}
