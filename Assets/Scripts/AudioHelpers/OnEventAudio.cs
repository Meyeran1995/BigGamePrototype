using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event eventToplay;
    [SerializeField] private GameObject owner;

    public void PlayEvent() => eventToplay.Post(owner);
}
