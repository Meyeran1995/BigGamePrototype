using UnityEngine;
using UnityEngine.UI;

public class RuneInventory : MonoBehaviour
{
    [SerializeField] private int size;
    private static int nextSlot;
    private static Image[] slots;

    private void Awake()
    {
        slots = new Image[size];
        GameObject template = transform.GetChild(0).gameObject;
        slots[0] = template.GetComponent<Image>();

        for (int i = 1; i < size; i++)
        {
            slots[i] = Instantiate(template, transform).GetComponent<Image>();
        }
    }

    public static void AcquireRune(Sprite rune)
    {
        if (nextSlot < slots.Length)
        {
            var slot = slots[nextSlot];
            slot.sprite = rune;
            slot.gameObject.SetActive(true);
            nextSlot++;
        }
        else
        {
            Debug.LogError("No more slots left for runes");
        }
    }

    public static int GetRuneAmount() => nextSlot;
}
