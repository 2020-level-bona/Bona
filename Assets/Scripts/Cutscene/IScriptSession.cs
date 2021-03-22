using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptSession
{
    Queue<IScriptCommand> GetCommands();
    void MakeExpire();
    bool HasExpired();
}
