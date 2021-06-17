using UnityEngine;

[System.Serializable]
public class BoolColumn
{
    public bool[] column;
}

[CreateAssetMenu(menuName = "ScriptableObjects/PuzzleConfig")]
public class PuzzleConfig : ScriptableObject
{
    [SerializeField] private BoolColumn[] solution;

    public BoolColumn[] Solution => solution;
}
