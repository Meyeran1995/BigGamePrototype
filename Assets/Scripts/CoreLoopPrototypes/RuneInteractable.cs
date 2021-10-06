using JetBrains.Annotations;
using UnityEngine;

public class RuneInteractable : Interactable
{
    [SerializeField] private Sprite runeSymbol;
    private bool acquired;

    [UsedImplicitly]
    public void Acquire()
    {
        if (acquired) return;
        RuneInventory.AcquireRune(runeSymbol);
    }
}
