using UnityEngine;

public class Shroom2D : Shroom
{
    protected override void Awake()
    {
        shroomRenderer = GetComponent<Renderer>();
        originalColor = shroomRenderer.material.color;
    }

    protected override bool CheckSetDirection()
    {
        float angle = Vector2.SignedAngle(setDir, (Vector2)DashValueMapper.GetCurrentDirection());
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }

    protected override bool CheckResetDirection()
    {
        if (resetDir == Vector2.zero && !isSwitchedOn) return true;

        float angle = Vector2.SignedAngle(resetDir, (Vector2)DashValueMapper.GetCurrentDirection());
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }
}
