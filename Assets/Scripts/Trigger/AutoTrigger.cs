using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrigger : Trigger
{
    void Update() {
        Invoke();
    }
}
