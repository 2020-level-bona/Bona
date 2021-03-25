using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnaryOp : Op
{
    object Do(object a);
}
