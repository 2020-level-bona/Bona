using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEqualOp : BinaryOp
{
    public int Priority => 2;

    public object Do(object a, object b) {
        return !((bool) new EqualOp().Do(a, b));
    }
}
