using System.Collections.Generic;
using UnityEngine;

public static class DashValueMapper
{
    private static readonly Dictionary<Vector2, Color> directionToColorMap = new Dictionary<Vector2, Color>();

    public const float TOLERANCE = 1f;
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

    }

    public static Color GetCurrentColor() => directionToColorMap[CurrentDirection];

    public static Vector2 GetCurrentDirection() => CurrentDirection;
}
