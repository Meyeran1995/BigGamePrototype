using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PuzzleZone : MonoBehaviour
{
    [SerializeField] private int zoneIndex;
    private static int nextZoneIndex = 0;
    private void Awake() => zoneIndex = nextZoneIndex++;
    
    private void OnValidate() => GetComponent<SphereCollider>().isTrigger = true;

    private void OnTriggerEnter(Collider other) => PuzzleZoneConnector.OnZoneEntered(zoneIndex, transform.position);
}
