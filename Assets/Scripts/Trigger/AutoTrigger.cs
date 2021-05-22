using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrigger : Trigger
{
    public bool onlyOnce = false;

    void Start() {
        if (onlyOnce) Invoke();
    }

    void Update() {
        if (!onlyOnce) Invoke();
    }
}
