using System.Collections.Generic;
using UnityEngine;

public class FungoVision : MonoBehaviour
{
    public static readonly List<GameObject> FungObjects = new List<GameObject>();

    public void EnableFungoVision()
    {
        foreach (var fung in FungObjects)
        {
            fung.SetActive(true);
        }
    }

    public void DisableFungoVision()
    {
        foreach (var fung in FungObjects)
        {
            fung.SetActive(false);
        }
    }
}
