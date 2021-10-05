using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnEnable() => PlayerInteraction.ActiveInteractables.Add(this);

    private void OnDisable() => PlayerInteraction.ActiveInteractables.Remove(this);

    public void Interact()
    {
        Debug.Log($"You just interacted with {name}");
    }
}
