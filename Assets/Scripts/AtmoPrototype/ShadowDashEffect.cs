using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DelegateTypes/ShadowDashEffect")]
public class ShadowDashEffect : ADashEffectSO
{
    [Range(0f, 10f)] [SerializeField] protected float spacing = 1.1f;
    private GameObject[] shadows;
    private Transform lastParent;
    
    public override void ApplyEffect(GameObject player, Vector3 direction)
    {
        AcquireShadows(player);
        OrderShadows(direction);
            
        foreach (GameObject s in shadows)
        {
            s.SetActive(true);
        }
    }

    public override void StripEffect()
    {
        foreach (GameObject s in shadows)
        {
            s.SetActive(false);
        }
    }

    public override void EffectFixedUpdate()
    {
    }

    private void AcquireShadows(GameObject player)
    {
        var shadowTransform = player.transform.GetChild(1);

        if (shadowTransform == lastParent) return;

        lastParent = player.transform;
        
        shadows = new GameObject[shadowTransform.childCount];
        for (int i = 0; i < shadowTransform.childCount; i++)
        {
            shadows[i] = shadowTransform.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// Orders the effects shadows according to a given direction
    /// </summary>
    /// <param name="dashDirection">Normalized vector3 aligned with desired dash direction</param>
    private void OrderShadows(Vector3 dashDirection)
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
}
