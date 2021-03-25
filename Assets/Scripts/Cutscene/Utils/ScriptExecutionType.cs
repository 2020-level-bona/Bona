using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScriptExecutionType
{
    [InspectorName("오직 한번만 실행")]
    ONCE,
    [InspectorName("여러번 실행")]
    ANWAYS,
}
