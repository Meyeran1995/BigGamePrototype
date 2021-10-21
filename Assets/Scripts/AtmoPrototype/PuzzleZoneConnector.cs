using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleZoneConnector : MonoBehaviour
{
    private static Tuple<int, Vector3> lastPosition, currentPosition;
    private static bool firstZoneEntered;
    private static List<Tuple<int, int>> connections;

    [SerializeField] private Material connectionMaterial;
    private static Material staticMaterial;

    private void Awake()
    {
        connections = new List<Tuple<int, int>>();
        staticMaterial = connectionMaterial;
    }

    public static void OnZoneEntered(int zoneIndex, Vector3 zonePosition)
    {
        if (firstZoneEntered)
        {
            if (zoneIndex == currentPosition.Item1) return;
            
            lastPosition = currentPosition;
            currentPosition = new Tuple<int, Vector3>(zoneIndex,zonePosition);
            OnZonesConnected();
        }
        else
        {
            currentPosition = new Tuple<int, Vector3>(zoneIndex,zonePosition);
            firstZoneEntered = true;
        }
    }

    private static void OnZonesConnected()
    {
        firstZoneEntered = false;

        if (IsAlreadyConnected(lastPosition.Item1, currentPosition.Item1)) return;
        
        LineRenderer line = new GameObject($"Connector {lastPosition.Item2} to {currentPosition.Item2}", typeof(LineRenderer))
                .GetComponent<LineRenderer>();
        
        line.SetPosition(0, lastPosition.Item2);
        line.SetPosition(1, currentPosition.Item2);
        line.sharedMaterial = staticMaterial;
        
        connections.Add(new Tuple<int, int>(lastPosition.Item1, currentPosition.Item1));
    }

    private static bool IsAlreadyConnected(int firstIndex, int secondIndex)
    {
        foreach (var connection in connections)
        {
            if (connection.Item1 != firstIndex && connection.Item2 != firstIndex) continue;
            
            if (connection.Item2 == secondIndex || connection.Item1 == secondIndex)
            {
                return true;
            }
        }

        return false;
    }
}
