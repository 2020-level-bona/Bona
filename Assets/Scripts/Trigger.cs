using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    protected UnityEvent Event = new UnityEvent();

    public void AddListener(UnityAction action) {
        Event.AddListener(action);
    }
}
