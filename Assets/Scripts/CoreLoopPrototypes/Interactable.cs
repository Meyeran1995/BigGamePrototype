using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent interactionEvent;

    private void OnEnable() => PlayerInteraction.ActiveInteractables.Add(this);

    private void OnDisable() => PlayerInteraction.ActiveInteractables.Remove(this);

    internal void Interact() => interactionEvent.Invoke();

    [UsedImplicitly]
    public void DebugPrintToConsole() => Debug.Log($"You just interacted with {name}");
}
