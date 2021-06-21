using UnityEngine;

public class PuzzleRow : MonoBehaviour
{
    private Shroom[] shrooms;
    public BoolRow Solution;

    public int Length => shrooms.Length;

    private void Awake()
    {
        shrooms = GetComponentsInChildren<Shroom>();
    }

    public void ReactToDash()
    {
        foreach (var shroom in shrooms)
        {
            shroom.OnDash(0.125f);
        }
    }

    public bool IsSolved()
    {
        for (int i = 0; i < shrooms.Length; i++)
        {
            if (shrooms[i].IsSwitchedOn != Solution.GetSwitchValueAtIndex(i))
                return false;
        }

        return true;
    }
}
