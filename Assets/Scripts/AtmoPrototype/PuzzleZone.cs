using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PuzzleZone : MonoBehaviour
{
    [SerializeField] private int zoneIndex;
    private static int nextZoneIndex = 0;
    protected static PlayerMovement movement;

    private void Awake()
    {
        zoneIndex = nextZoneIndex++;
        if (!movement)
            movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnValidate() => GetComponent<SphereCollider>().isTrigger = true;

    protected virtual void ZoneInteraction()
    {
        if (!movement.isDashing) return;
        PuzzleZoneConnector.OnZoneEntered(zoneIndex, transform.position);
    }

    protected virtual void OnTriggerEnter(Collider other) => ZoneInteraction();

    protected virtual void OnTriggerExit(Collider other) => ZoneInteraction();
}
