using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DelegateTypes/MusicTheme")]
public class MusicThemeSO : ScriptableObject
{
    [SerializeField] private AK.Wwise.Event theme;
    private GameObject origin;

    public void StartTheme(GameObject emitter)
    {
        theme.Post(emitter);
        origin = emitter;
    }
    
    public void StopTheme()
    {
        theme.Stop(origin);
        origin = null;
    }
}
