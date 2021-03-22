using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptCommand
{
    string Keyword {get;}
    bool Blocking {get;}

    IEnumerator GetCoroutine();
}
