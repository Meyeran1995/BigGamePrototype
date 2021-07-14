using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IsometricObject))]
public class SortingOrderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        IsometricObject iObj = target as IsometricObject;
        if (GUILayout.Button("Set up Sorting Layer"))
            iObj.ComputeSortingOrder();
    }
}