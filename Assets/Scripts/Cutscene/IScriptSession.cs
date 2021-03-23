﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptSession
{
    List<LinePointer> linePointers {get; set;}
    Queue<IScriptCommand> GetCommands();
    void MakeExpire();
    bool HasExpired();
}
