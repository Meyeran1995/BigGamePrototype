using UnityEngine;

public class DashEffect2D : DashEffect
{
    public override void OrderShadows(Vector3 dashDirection)
    {
        dashDirection = -dashDirection;
        float currentX, currentY;

        for (int i = 1; i <= shadows.Length; i++)
        {
            currentX = i * dashDirection.x * spacing;
            currentY = i * dashDirection.y * spacing;
            shadows[i - 1].transform.position = shadows[i - 1].transform.parent.position + new Vector3(currentX, currentY, 0f);
        }
    }
}
