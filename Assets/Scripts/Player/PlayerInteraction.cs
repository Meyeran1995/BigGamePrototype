using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static readonly List<Interactable> ActiveInteractables = new List<Interactable>();

    public void TryInteract()
    {
        if(ActiveInteractables.Count == 0) return;

        ActiveInteractables[0].Interact();
    }
}
