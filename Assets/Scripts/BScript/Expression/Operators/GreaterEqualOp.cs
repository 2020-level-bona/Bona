using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterEqualOp : BinaryOp
{
    public int Priority => 3;

    bool leftIsGreater;

    public GreaterEqualOp(bool leftIsGreater) {
        this.leftIsGreater = leftIsGreater;
    }

    public object Do(object a, object b) {
        if (a is null)
            a = 0;
        if (b is null)
            b = 0;
        
        if (a is bool)
            throw new BSUnsupportedTypeException(-1, "왼쪽 피연산자의 타입 bool은 지원되지 않습니다.");
        if (b is bool)
            throw new BSUnsupportedTypeException(-1, "오른쪽 피연산자의 타입 bool은 지원되지 않습니다.");
        
        if (a is long && b is long)
            return leftIsGreater ? ((long) a >= (long) b) : ((long) a <= (long) b);
        
        return leftIsGreater ? (System.Convert.ToDouble(a) >= System.Convert.ToDouble(b))
            : (System.Convert.ToDouble(a) <= System.Convert.ToDouble(b));
    }
}
