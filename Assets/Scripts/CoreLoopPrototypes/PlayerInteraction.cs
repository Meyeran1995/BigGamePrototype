using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static readonly List<Interactable> ActiveInteractables = new List<Interactable>();

    public void OnTryInteract(InputAction.CallbackContext context)
    {
        if(ActiveInteractables.Count == 0) return;

        ActiveInteractables[0].Interact();
    }
}
