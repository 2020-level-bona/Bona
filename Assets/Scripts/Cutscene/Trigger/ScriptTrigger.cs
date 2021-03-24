using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BScriptExecutor))]
public class ScriptTrigger : MonoBehaviour
{
    BScriptExecutor executor;
    public string startCondition;
    public TriggerExecutionType executionType;

    public void Run() {
        CheckUniqueName();

        if (executionType == TriggerExecutionType.ONCE && Session.CurrentScene.GetBool(name))
            return;
        
        Session.CurrentScene.Set(name, true);

        executor = GetComponent<BScriptExecutor>(); // TODO: Caching?
        if (executor.CanRun() && CheckStartCondition())
            executor.Run();
    }

    void CheckUniqueName() {
        if (name == "No Name")
            throw new System.Exception("이름이 기본 이름인 'No Name'으로 설정되어 있습니다. 이름을 변경해주세요.");
        
        foreach (ScriptTrigger scriptTrigger in FindObjectsOfType<ScriptTrigger>()) {
            if (scriptTrigger == this)
                continue;
            if (scriptTrigger.name == name)
                throw new System.Exception($"동일한 이름({name})을 가진 두 ScriptTrigger가 존재합니다.");
        }
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
