using UnityEngine;

public class DashEffect : MonoBehaviour
{
    [Range(0f, 10f)]
    public float spacing;
    private GameObject[] shadows;

    private void Awake()
    {
        shadows = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            shadows[i] = transform.GetChild(i).gameObject;
            shadows[i].SetActive(false);
        }
    }

    public void ToggleEffect()
    {
        foreach (GameObject s in shadows)
        {
            s.SetActive(!s.activeSelf);
        }
    }

    /// <summary>
    /// Orders the effects shadows according to a given direction
    /// </summary>
    /// <param name="dashDirection">Normalized vector3 aligned with desired dash direction</param>
    public void OrderShadows(Vector3 dashDirection)
    {
        dashDirection = -dashDirection;
        float currentX, currentZ;

        for (int i = 1; i <= shadows.Length; i++)
        {
            currentX = i * dashDirection.x * spacing;
            currentZ = i * dashDirection.z * spacing;
            shadows[i - 1].transform.position = shadows[i - 1].transform.parent.position + new Vector3(currentX, 0f, currentZ);
        }
    }

    private void OnValidate()
    {
        shadows = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            shadows[i] = transform.GetChild(i).gameObject;
        }
        OrderShadows(Vector3.right);
    }
}
