using JetBrains.Annotations;
using UnityEngine;

public class RuneDestination : Interactable
{
    [SerializeField] private GameObject diashow;
    private int runesPlaced;

    [UsedImplicitly]
    public void PlaceRune()
    {
        int currentRune = RuneInventory.GetRuneAmount();

        if (currentRune == 0)
        {
            Debug.Log("No runes available");
        }
        else if (currentRune > runesPlaced)
        {
            runesPlaced++;
            diashow.SetActive(true);
            StartCoroutine(diashow.GetComponent<ClickableDiashow>().Play());
        }
    }
}
