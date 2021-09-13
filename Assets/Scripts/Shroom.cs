using System.Collections;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    [Header("Set/Reset")]
    [SerializeField] protected bool isSwitchedOn;
    [SerializeField] protected Vector2 setDir, resetDir;
    protected Renderer shroomRenderer;

    public bool IsSwitchedOn => isSwitchedOn;

    [Header("Reaction")]
    [SerializeField] protected float tolerance;
    protected Color originalColor, switchOnColor;

    protected virtual void Awake()
    {
        shroomRenderer = GetComponent<MeshRenderer>();
        originalColor = shroomRenderer.material.color;
    }

    private void Start() => switchOnColor = DashValueMapper.GetColor(new Vector3(setDir.x, 0f, setDir.y));

    public void OnDash(float waitTime) => StartCoroutine(DashReactionAnim(waitTime));

    private IEnumerator DashReactionAnim(float waitTime)
    {
        Color currentColor = IsSwitchedOn ? switchOnColor : originalColor;
        Color currentDashColor = DashValueMapper.GetCurrentColor();

        float t = 0f, halfTime = waitTime / 2f;

        while(waitTime > halfTime)
        {
            shroomRenderer.material.color = Color.Lerp(currentColor, currentDashColor,  t);
            t += Time.deltaTime / 2f;
            waitTime -= Time.deltaTime;
            yield return null;
        }

        while (waitTime > 0f)
        {
            shroomRenderer.material.color = Color.Lerp(currentColor, currentDashColor, t);
            t -= Time.deltaTime / 2f;
            waitTime -= Time.deltaTime;
            yield return null;
        }

        if (CheckSetDirection())
        {
            SwitchOn();
        }
        else if(CheckResetDirection())
        {
            SwitchOff();
        }
    }

    protected virtual bool CheckSetDirection()
    {
        float angle = Vector3.SignedAngle(new Vector3(setDir.x, 0f, setDir.y), DashValueMapper.GetCurrentDirection(), Vector3.up);
        angle = angle < 0f ? -angle : angle;

        return angle <= tolerance;
    }

    protected virtual bool CheckResetDirection()
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
