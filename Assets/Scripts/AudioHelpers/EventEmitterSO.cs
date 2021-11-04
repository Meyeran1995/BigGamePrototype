using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DelegateTypes/EventEmitter")]
public class EventEmitterSO : ScriptableObject
{
    [SerializeField] private AK.Wwise.Event theme;
    private GameObject origin;

    public void StartEmitting(GameObject emitter)
    {
        theme.Post(emitter);
        origin = emitter;
    }
    
    public void StopEmitting()
    {
        theme.Stop(origin);
        origin = null;
    }
}
