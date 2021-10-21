using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private PuzzleRow[] rows;
    [SerializeField] private PuzzleConfig activeSolution;

    public static readonly List<ShroomNode> notifiedNodes = new List<ShroomNode>();
    private static readonly List<ShroomNode> nodes = new List<ShroomNode>();

    [SerializeField] private float baseWaitTime;
    private const float WAIT_INCREMENT = 0.125f;
    public static float CurrentWaittime { get; private set; }

    private void Awake()
    {
        rows = GetComponentsInChildren<PuzzleRow>();
    }

    private void Start()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].Solution = activeSolution[i];
        }
    }

    public IEnumerator ReactToDash()
    {
        //foreach (var row in rows)
        //{
        //    row.ReactToDash();
        //}

        notifiedNodes.Clear();
        CurrentWaittime = baseWaitTime;

        foreach (var node in nodes)
        {
            node.NotifyNeighbors();
        }

        yield return new WaitUntil(() => notifiedNodes.Count == nodes.Count);

        CheckCurrentPuzzleState();
    }

    private void CheckCurrentPuzzleState()
    {
        if (rows.Any(row => !row.IsSolved())) return;

        OnPuzzleSolved();
    }

    private void OnPuzzleSolved()
    {
        Debug.Log("You win!");
    }

    public static void RegisterNode(ShroomNode node) => nodes.Add(node);

    public static void IncrementWaitTime() => CurrentWaittime += WAIT_INCREMENT;
}
