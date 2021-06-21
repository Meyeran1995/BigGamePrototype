using UnityEngine;

public class ShroomNode : MonoBehaviour
{
    private ShroomNode[] neighbors;
    private Shroom shroom;
    [SerializeField] private float neighborRadius;

    private void Awake()
    {
        shroom = GetComponent<Shroom>();
        Collider[] neighboringShrooms = Physics.OverlapSphere(transform.position, neighborRadius, 1 << 6);

        if(neighboringShrooms == null) return;

        neighbors = new ShroomNode[neighboringShrooms.Length];

        for (int i = 0; i < neighbors.Length; i++)
        {
            neighbors[i] = neighboringShrooms[i].GetComponent<ShroomNode>();
        }
    }

    private void Start() => Puzzle.RegisterNode(this); 

    public void NotifyNeighbors()
    {
        if (Puzzle.notifiedNodes.Contains(this)) return;

        Puzzle.notifiedNodes.Add(this);
        shroom.OnDash(Puzzle.CurrentWaittime);
        Puzzle.IncrementWaitTime();

        if(neighbors == null) return;

        foreach (var node in neighbors)
        {
            if(Puzzle.notifiedNodes.Contains(node)) continue;

            node.NotifyNeighbors();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, neighborRadius);
    }
}
