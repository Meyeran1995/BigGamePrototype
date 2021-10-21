using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerCollision : MonoBehaviour
{
   private Rigidbody rb;
   private Vector3 halfExtents, lastProbeDirection, lastImpactPosition;
   private bool lastProbeWasHit;

   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
      halfExtents = GetComponent<BoxCollider>().size / 2f;
   }

   /// <summary>
   /// Probes for collisions on the XZ-plane in given direction
   /// </summary>
   /// <param name="movementDirection"></param>
   /// <returns></returns>
   public bool ProbeCollisionOnGroundPlane(Vector3 movementDirection)
   {
      // works only for static collisions
      if (lastProbeWasHit && movementDirection == lastProbeDirection) return true;
      
      var extentOffset = movementDirection.normalized;
      extentOffset.Scale(halfExtents);

      lastProbeWasHit = Physics.Raycast(rb.position, movementDirection, out var hitInfo,
         (movementDirection + extentOffset).magnitude, ~0, QueryTriggerInteraction.Ignore);

      if (!lastProbeWasHit) return false;
      
      lastProbeDirection = movementDirection;
      
      var contactPosition = hitInfo.point - extentOffset;
      contactPosition.y = rb.position.y;

      lastImpactPosition = contactPosition;

      return true;
   }

   /// <summary>
   /// Gets an Available free position from last impact
   /// </summary>
   /// <returns>Available free position at impact point of a collision</returns>
   public Vector3 GetImpactPosition() => lastImpactPosition;
}
