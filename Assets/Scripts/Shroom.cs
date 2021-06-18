using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shroom : MonoBehaviour
{
    [Header("Set/Reset")]
    [SerializeField] private bool isSwitchedOn;
    [SerializeField] private Vector2 setDir, resetDir;
    private SpriteRenderer shroomRenderer;

    public bool IsSwitchedOn => isSwitchedOn;

    [Header("Reaction")]
    [SerializeField] private float tolerance;
    private Color originalColor, switchOnColor;

    private void Awake()
    {
        shroomRenderer = GetComponent<SpriteRenderer>();
        originalColor = shroomRenderer.color;
    }

    private void Start() => switchOnColor = DashValueMapper.GetColor(setDir);

    public void OnDash()
    {
        StartCoroutine(DashReactionAnim());
    }

    private IEnumerator DashReactionAnim()
    {
        shroomRenderer.color = DashValueMapper.GetCurrentColor();

        yield return new WaitForSeconds(0.125f);

        shroomRenderer.color = IsSwitchedOn ? switchOnColor : originalColor;

        yield return new WaitForSeconds(0.125f);

        if (CheckSetDirection())
        {
            SwitchOn();
        }
        else if(CheckResetDirection())
        {
            SwitchOff();
        }
    }

    private bool CheckSetDirection()
    {
        float angle = Vector2.SignedAngle(setDir, DashValueMapper.GetCurrentDirection());
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }

    private bool CheckResetDirection()
    {
        if (resetDir == Vector2.zero) return false;

        float angle = Vector2.SignedAngle(resetDir, DashValueMapper.GetCurrentDirection());
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }

    private void SwitchOn()
    {
        if (isSwitchedOn) return;

        isSwitchedOn = true;
        shroomRenderer.color = switchOnColor;
    }

    private void SwitchOff()
    {
        isSwitchedOn = false;
        shroomRenderer.color = originalColor;
    }
}
