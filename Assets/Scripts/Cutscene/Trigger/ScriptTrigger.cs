using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BScriptExecutor))]
public class ScriptTrigger : MonoBehaviour
{
    BScriptExecutor executor;
    public string startCondition;

    public void Run() {
        executor = GetComponent<BScriptExecutor>(); // TODO: Caching?
        if (executor.CanRun() && CheckStartCondition())
            executor.Run();
    }

    bool CheckStartCondition() {
        if (startCondition == null || startCondition == "")
            return true;
        object result = Expression.Eval(startCondition);
        if (result is null)
            return false;
        if (result is bool)
            return (bool) result;
        if (result is int || result is long)
            return System.Convert.ToInt64(result) != 0;
        if (result is float || result is double)
            return System.Convert.ToDouble(result) != 0;
        return false;
    }
}
