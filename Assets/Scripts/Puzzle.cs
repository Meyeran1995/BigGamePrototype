using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private PuzzleRow[] rows;
    [SerializeField] private PuzzleConfig activeSolution;

    public void ReactToDash()
    {

    }

    private void CheckCurrentPuzzleState()
    {

    }

    private void OnPuzzleSolved()
    {
        Debug.Log("You win!");
    }
}
