using System.Collections;
using System.Linq;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private PuzzleRow[] rows;
    [SerializeField] private PuzzleConfig activeSolution;

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
        foreach (var row in rows)
        {
            row.ReactToDash();
        }

        //TODO: Diese zahl muss immer angepasst werden, wenn die shroom reaction anim verändert wird
        yield return new WaitForSeconds(0.55f);

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
}
