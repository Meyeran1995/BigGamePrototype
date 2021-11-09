using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FungoVisible : MonoBehaviour
{
    private void Awake()
    {
        FungoVision.FungObjects.Add(GetComponent<MeshRenderer>());
        gameObject.SetActive(false);
    }
}
