using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
