using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeEffect : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Image targetImage;
    [SerializeField] private bool setActiveOnReset;
    private Color imageColor, originalColor;

    private void Awake()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        imageColor = targetImage.color;
        originalColor = imageColor;
    }

    public IEnumerator FadeOut()
    {
        while (imageColor.a > 0f)
        {
            imageColor.a -= fadeSpeed * Time.deltaTime;
            targetImage.color = imageColor;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    public IEnumerator FadeIn()
    {
        imageColor.a = 0f;
        gameObject.SetActive(true);

        while (imageColor.a < 1f)
        {
            imageColor.a += fadeSpeed * Time.deltaTime;
            targetImage.color = imageColor;
            yield return null;
        }
    }

    public void ResetEffect()
    {
        imageColor = originalColor;
        //gameObject.SetActive(setActiveOnReset);
    }

    public void FadeOutImmediate()
    {
        imageColor.a = 0f;
        targetImage.color = imageColor;
    }
}
