using System.Collections.Generic;
using UnityEngine;

public class FungoVision : MonoBehaviour
{
    public static readonly List<GameObject> FungObjects = new List<GameObject>();
    private bool visionIsActive;

    public void ToggleFungoVision()
    {
        visionIsActive = !visionIsActive;
        
        foreach (var fung in FungObjects)
        {
            fung.SetActive(visionIsActive);
        }
    }
}
