using System.Collections.Generic;
using UnityEngine;

public static class DashValueMapper
{
    private static readonly Dictionary<Vector3, Color> directionToColorMap = new Dictionary<Vector3, Color>();
    private static Vector3 CurrentDirection;

    public static void Initialize()
    {
        directionToColorMap[Vector3.right] = Color.red;
        directionToColorMap[Vector3.left] = Color.green;
        directionToColorMap[Vector3.forward] = Color.yellow;
        directionToColorMap[Vector3.back] = Color.blue;
    }

    // public static void OnDashCalcValues()
    // {
    //     float angle;
    //     float prevAngle = 360f;
    //
    //     foreach (var entry in directionToColorMap)
    //     {
    //         angle = Vector3.SignedAngle(PlayerInputController.MoveDir, entry.Key, Vector3.up);
    //         angle = angle < 0f ? -angle : angle;
    //         if (prevAngle < angle) continue;
    //
    //         CurrentDirection = entry.Key;
    //         prevAngle = angle;
    //     }
    // }

    public static Color GetColor(Vector3 setDir)
    {
        if (directionToColorMap.TryGetValue(setDir, out Color color))
        {
            return color;
        }
        else
        {
            float angle;
            float prevAngle = 360f;
            Vector3 approxDirection = Vector3.zero;

            foreach (var entry in directionToColorMap)
            {
                angle = Vector3.SignedAngle(setDir, entry.Key, Vector3.up);
                angle = angle < 0f ? -angle : angle;
                if (prevAngle < angle) continue;

                approxDirection = entry.Key;
                prevAngle = angle;
            }

            return directionToColorMap[approxDirection];
        }
    }

    public static Color GetCurrentColor() => directionToColorMap[CurrentDirection];

    public static Vector3 GetCurrentDirection() => CurrentDirection;
}
