using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStopper : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event eventToStop;
    [SerializeField] private GameObject correspondingGameObject;

    private void OnTriggerEnter(Collider other)
    {
        eventToStop.Stop(correspondingGameObject);
    }
}
