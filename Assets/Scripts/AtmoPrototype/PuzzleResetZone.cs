
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PuzzleResetZone : PuzzleZone
{
    private void ZoneInteraction() => PuzzleZoneConnector.ResetConnections();

    protected override void OnTriggerEnter(Collider other) => ZoneInteraction();

    protected override void OnTriggerExit(Collider other) => ZoneInteraction();

#if UNITY_EDITOR
    protected override void OnDrawGizmos() => Handles.Label(transform.position, "R");

    protected override void OnValidate()
    {
    }
#endif
}
