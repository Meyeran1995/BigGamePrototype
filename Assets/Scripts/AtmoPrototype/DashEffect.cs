using UnityEngine;

public class DashEffect : MonoBehaviour
{
    [SerializeField] private ADashEffectSO[] effects;

    public void StartEffect(Vector3 direction)
    {
        if(effects.Length == 0) return;
        
        foreach (var effect in effects)
        {
            effect.ApplyEffect(transform.parent.gameObject, direction);
        }
    }
    
    public void StopEffect()
    {
        if(effects.Length == 0) return;
        
        foreach (var effect in effects)
        {
            effect.StripEffect();
        }
    }

    private void FixedUpdate()
    {
        if(effects.Length == 0) return;
        
        foreach (var effect in effects)
        {
            effect.EffectFixedUpdate();
        }
    }
}
