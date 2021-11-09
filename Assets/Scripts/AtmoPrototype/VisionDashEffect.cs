using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DelegateTypes/VisionDashEffect")]
public class VisionDashEffect : ADashEffectSO
{
    [SerializeField] private float fadeSpeed;
    
    private Color currentColor;
    private float startingAlpha;
    private MaterialPropertyBlock propertyBlock;
    private int propertyID;
    private FungoVision vision;
    private bool isFadingIn;

    private void OnEnable()
    {
        propertyBlock = new MaterialPropertyBlock();
        propertyID = Shader.PropertyToID("_BaseColor");
    }

    public override void ApplyEffect(GameObject player, Vector3 direction)
    {
        if (!vision)
        {
            AcquireVision(player);
            var renderer = FungoVision.FungObjects[0];
            renderer.GetPropertyBlock(propertyBlock);
            
            var material = renderer.sharedMaterial;
            startingAlpha = material.color.a;
            currentColor = material.color;
        }

        currentColor.a = 0;
        ApplyColor();

        isFadingIn = true;
        
        vision.EnableFungoVision();
    }

    public override void StripEffect()
    {
        isFadingIn = false;
    }

    public override void EffectFixedUpdate()
    {
        if (isFadingIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private void AcquireVision(GameObject player)
    {
        vision = player.transform.GetChild(0).GetComponent<FungoVision>();
    }

    private void FadeIn()
    {
        if (currentColor.a == startingAlpha) return;

        currentColor.a = Mathf.Clamp(currentColor.a + fadeSpeed * Time.fixedDeltaTime, 0f, startingAlpha);
        ApplyColor();
    }

    private void FadeOut()
    {
        if (currentColor.a == 0f) return;
        
        currentColor.a = Mathf.Clamp01(currentColor.a - fadeSpeed * Time.fixedDeltaTime);
        ApplyColor();

        if (currentColor.a == 0f)
            vision.DisableFungoVision();
    }
    
    private void ApplyColor()
    {
        propertyBlock.SetColor(propertyID, currentColor);
        vision.SetPropertyBlocks(propertyBlock);
    }
}
