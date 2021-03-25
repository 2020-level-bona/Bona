using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultOp : BinaryOp
{
    public int Priority => 5;

    public object Do(object a, object b) {
        if (a is bool)
            throw new BSUnsupportedTypeException(-1, "왼쪽 피연산자의 타입 bool은 지원되지 않습니다.");
        if (b is bool)
            throw new BSUnsupportedTypeException(-1, "오른쪽 피연산자의 타입 bool은 지원되지 않습니다.");
        
        if (a is null) {
            a = 0;
        }
        if (b is null) {
            b = 0;
        }

        if (a is long && b is long) {
            return (long) a * (long) b;
        }
        return System.Convert.ToDouble(a) * System.Convert.ToDouble(b);
    }
}
