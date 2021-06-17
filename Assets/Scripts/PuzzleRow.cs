using UnityEngine;

public class PuzzleRow : MonoBehaviour
{
    private Shroom[] shrooms;

    public Shroom[] Shrooms => shrooms;

    private void Awake()
    {
        shrooms = GetComponentsInChildren<Shroom>();
    }
}
