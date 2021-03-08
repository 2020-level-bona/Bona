using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 부가적인 기능을 사용하기 위한 Wrapper 클래스
public class CutsceneEnumerator : IEnumerator
{
    IEnumerator coroutine;
    ChatQueue chatQueue;

    public object Current {
        get {
            if (coroutine.Current is WaitForSkippingChat) {
                return new WaitWhile(() => chatQueue.IsDisplaying);
            }
            return coroutine.Current;
        }
    }

    public CutsceneEnumerator(IEnumerator coroutine, ChatQueue chatQueue) {
        this.coroutine = coroutine;
        this.chatQueue = chatQueue;
    }

    public bool MoveNext() {
        return coroutine.MoveNext();
    }

    public void Reset() {
        coroutine.Reset();
    }
}
