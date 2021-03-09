using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 클래스는 오로지 지시용으로만 사용된다.
public class WaitForTween
{
    public ITweenEntry tweenEntry;

    public WaitForTween(ITweenEntry tweenEntry) {
        this.tweenEntry = tweenEntry;
    }
}
