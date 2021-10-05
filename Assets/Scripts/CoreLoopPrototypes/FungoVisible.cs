using UnityEngine;

public class FungoVisible : MonoBehaviour
{
    private void Awake()
    {
        FungoVision.FungObjects.Add(gameObject);
    }
}
