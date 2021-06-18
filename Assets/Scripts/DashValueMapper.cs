using System.Collections.Generic;
using UnityEngine;

public static class DashValueMapper
{
    private static readonly Dictionary<Vector2, Color> directionToColorMap = new Dictionary<Vector2, Color>();
    private static Vector2 CurrentDirection;

    public static void Initialize()
    {
        directionToColorMap[Vector2.right] = Color.red;
        directionToColorMap[Vector2.left] = Color.green;
        directionToColorMap[Vector2.up] = Color.yellow;
        directionToColorMap[Vector2.down] = Color.blue;
    }

    public static void OnDashCalcValues()
    {
        float angle;
        float prevAngle = 360f;

        foreach (var entry in directionToColorMap)
        {
            angle = Vector2.SignedAngle(PlayerInputController.MoveDir, entry.Key);
            angle = angle < 0f ? -angle : angle;
            if (prevAngle < angle) continue;

            CurrentDirection = entry.Key;
            prevAngle = angle;
        }
    }

    public static Color GetColor(Vector2 setDir)
    {
        if (directionToColorMap.TryGetValue(setDir, out Color color))
        {
            return color;
        }
        else
        {
            float angle;
            float prevAngle = 360f;
            Vector2 approxDirection = Vector2.zero;

            foreach (var entry in directionToColorMap)
            {
                angle = Vector2.SignedAngle(setDir, entry.Key);
                angle = angle < 0f ? -angle : angle;
                if (prevAngle < angle) continue;

                approxDirection = entry.Key;
                prevAngle = angle;
            }

            return directionToColorMap[approxDirection];
        }
    }

    public static Color GetCurrentColor() => directionToColorMap[CurrentDirection];

    public static Vector2 GetCurrentDirection() => CurrentDirection;
}
