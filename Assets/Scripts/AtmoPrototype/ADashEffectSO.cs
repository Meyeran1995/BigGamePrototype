using UnityEngine;

public abstract class ADashEffectSO : ScriptableObject
{
    public abstract void ApplyEffect(GameObject player, Vector3 direction);
    public abstract void StripEffect();
    public abstract void EffectFixedUpdate();
}
