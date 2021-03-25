using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrOp : BinaryOp
{
    public int Priority => 0;

    public object Do(object a, object b) {
        if (a is null)
            a = false;
        
        if (b is null)
            b = false;
        
        if (!(a is bool))
            throw new BSUnsupportedTypeException(-1, $"왼쪽 피연산자의 타입 {a.GetType()}은 지원되지 않습니다.");
        if (!(b is bool))
            throw new BSUnsupportedTypeException(-1, $"오른쪽 피연산자의 타입 {b.GetType()}은 지원되지 않습니다.");
        
        return (bool) a || (bool) b;
    }
}
