using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BScriptExecutor))]
public class ScriptTrigger : MonoBehaviour
{
    public void Run() {
        GetComponent<BScriptExecutor>().Run();
    }
}
