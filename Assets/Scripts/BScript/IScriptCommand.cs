﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptCommand
{
    bool Blocking {get;}
    int LineNumber {get;}

    IEnumerator GetCoroutine();
}