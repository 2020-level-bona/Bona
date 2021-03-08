using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 컷신 제어용 Wrapper 클래스
public class CutsceneEnumerator : IEnumerator
{
    IEnumerator coroutine;

    public object Current {
        get => coroutine.Current;
    }

    public CutsceneEnumerator(IEnumerator coroutine) {
        this.coroutine = coroutine;
    }

    public bool MoveNext() {
        return coroutine.MoveNext();
    }

    public void Reset() {
        coroutine.Reset();
    }
}
