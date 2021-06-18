using UnityEngine;

[System.Serializable]
public class BoolRow
{
    [SerializeField] private bool[] row;

    public int Length => row.Length;

    public bool GetSwitchValueAtIndex(int index) => row[index];
}

[CreateAssetMenu(menuName = "ScriptableObjects/PuzzleConfig")]
public class PuzzleConfig : ScriptableObject
{
    [SerializeField] private BoolRow[] solution;

    public BoolRow this[int index] => solution[index];

    public int NumberOfRows => solution.Length;
}
