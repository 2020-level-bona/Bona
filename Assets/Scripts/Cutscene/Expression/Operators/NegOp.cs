using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegOp : UnaryOp
{
    public int Priority => 6;

    public object Do(object a) {
        if (a is null)
            a = false;
        
        if (!(a is bool))
            throw new BSUnsupportedTypeException(-1, "오직 bool 타입만 지원합니다.");
        
        return !((bool) a);
    }
}
