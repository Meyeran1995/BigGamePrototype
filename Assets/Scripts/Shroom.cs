using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Shroom : MonoBehaviour
{
    [Header("Set/Reset")]
    [SerializeField] private bool isSwitchedOn;
    [SerializeField] private Vector2 setDir, resetDir;
    private MeshRenderer shroomRenderer;

    public bool IsSwitchedOn => isSwitchedOn;

    [Header("Reaction")]
    [SerializeField] private float tolerance;
    private Color originalColor, switchOnColor;

    private void Awake()
    {
        shroomRenderer = GetComponent<MeshRenderer>();
        originalColor = shroomRenderer.material.color;
    }

    private void Start() => switchOnColor = DashValueMapper.GetColor(new Vector3(setDir.x, 0f, setDir.y));

    public void OnDash(float waitTime)
    {
        StartCoroutine(DashReactionAnim(waitTime));
    }

    private IEnumerator DashReactionAnim(float waitTime)
    {
        Color currentColor = IsSwitchedOn ? switchOnColor : originalColor;
        Color currentDashColor = DashValueMapper.GetCurrentColor();

        float t = 0f;

        while(waitTime > 0f)
        {
            shroomRenderer.material.color = Color.Lerp(currentColor, currentDashColor,  t);
            t += Time.deltaTime / 2f;
            waitTime -= Time.deltaTime;
            yield return null;
        }

        shroomRenderer.material.color = IsSwitchedOn ? switchOnColor : originalColor;

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
        float angle = Vector3.SignedAngle(new Vector3(setDir.x, 0f, setDir.y), DashValueMapper.GetCurrentDirection(), Vector3.up);
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }

    private bool CheckResetDirection()
    {
        if (resetDir == Vector2.zero && !isSwitchedOn) return true;

        float angle = Vector3.SignedAngle(new Vector3(resetDir.x, 0f, resetDir.y), DashValueMapper.GetCurrentDirection(), Vector3.up);
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }

    private void SwitchOn()
    {
        if (isSwitchedOn) return;

        isSwitchedOn = true;
        shroomRenderer.material.color = switchOnColor;
    }

    private void SwitchOff()
    {
        isSwitchedOn = false;
        shroomRenderer.material.color = originalColor;
    }
}
