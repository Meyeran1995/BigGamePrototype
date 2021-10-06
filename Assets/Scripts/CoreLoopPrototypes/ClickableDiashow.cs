using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ClickableDiashow : MonoBehaviour
{
    [SerializeField] private Page[] pages;

    private int currentPage;
    private Image front, back;
    private ImageFadeEffect fadeEffect;
    private Button progressButton;

    private void Awake()
    {
        progressButton = GetComponent<Button>();
        progressButton.interactable = false;

        front = transform.GetChild(1).GetComponent<Image>();
        fadeEffect = front.GetComponent<ImageFadeEffect>();
        back = transform.GetChild(0).GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public IEnumerator Play()
    {
        back.gameObject.SetActive(true);

        var fade = back.GetComponent<ImageFadeEffect>();
        fade.FadeOutImmediate();

        yield return StartCoroutine(fade.FadeIn());

        progressButton.interactable = true;
    }

    [UsedImplicitly]
    public void ProgressDiashow()
    {
        if (++currentPage < pages.Length)
        {
            NextPage();
        }
        else
        {
            StartCoroutine(ExitSequence());
        }

        progressButton.interactable = false;
    }

    private void NextPage()
    {
        front.sprite = back.sprite;
        front.gameObject.SetActive(true);
        back.sprite = pages[currentPage].Picture;

        fadeEffect.ResetEffect();
        StartCoroutine(FadeCurrentPage());
    }

    private IEnumerator FadeCurrentPage()
    {
        yield return StartCoroutine(fadeEffect.FadeOut());

        progressButton.interactable = true;
    }

    private IEnumerator ExitSequence()
    {
        GetComponent<Image>().raycastTarget = false;

        var fade = back.GetComponent<ImageFadeEffect>();

        yield return StartCoroutine(fade.FadeOut());
        
        back.enabled = false;
        fade.ResetEffect();
    }
}
