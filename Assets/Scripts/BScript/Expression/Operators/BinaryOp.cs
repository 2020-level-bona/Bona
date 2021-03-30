using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BinaryOp : Op
{
    object Do(object a, object b);
}
