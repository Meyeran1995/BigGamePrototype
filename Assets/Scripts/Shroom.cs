using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shroom : MonoBehaviour
{
    [Header("Set/Reset")]
    [SerializeField] private bool isSwitchedOn;
    [SerializeField] private Vector2 setDir, resetDir;

    [Header("Reaction")]
    [SerializeField] private float tolerance;
    private Color originalColor;

    private void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    public void OnDash()
    {

    }

    private IEnumerator DashReactionAnim()
    {
        yield return null;
    }

    private void SwitchOn()
    {

    }

    private void SwitchOff()
    {

    }
}
