using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class IsometricObject : MonoBehaviour
{
    private const int IsometricRangePerUnit = 100;

    private Renderer rend;

    [SerializeField] private bool is3D;

    [Tooltip("Will this object be moved during runtime?")]
    [SerializeField] private bool isDynamic;

    [Tooltip("Will use this object as pivot while computing z-order")]
    [SerializeField] private Transform target;

    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    [SerializeField] private int targetOffset;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        if (target == null)
            target = transform;

        ComputeSortingOrder();

        this.enabled = isDynamic;
    }


    private void FixedUpdate()
    {
        if (!transform.hasChanged) return;
        transform.hasChanged = false;
        ComputeSortingOrder();
    }

    public void ComputeSortingOrder()
    {
        if (!target)
            target = transform;

        if (is3D)
        {
            rend.sortingOrder = -Mathf.RoundToInt(target.position.z * IsometricRangePerUnit) + targetOffset;
        }
        else
        {
            rend.sortingOrder = -Mathf.RoundToInt(target.position.y * IsometricRangePerUnit) + targetOffset;
        }
    }
}
