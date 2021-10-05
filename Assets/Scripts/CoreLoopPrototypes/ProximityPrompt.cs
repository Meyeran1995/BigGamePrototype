using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ProximityPrompt : MonoBehaviour
{
    private GameObject prompt;
    [SerializeField] private float proximityArea;
    [SerializeField] private bool startsActive;

    private void Awake()
    {
        prompt = transform.GetChild(0).gameObject;
        prompt.SetActive(startsActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            prompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            prompt.SetActive(false);
    }

    private void OnValidate()
    {
        BoxCollider coll = GetComponent<BoxCollider>();
        coll.isTrigger = true;
        coll.size = new Vector3(proximityArea, coll.size.y, proximityArea);
    }
}
