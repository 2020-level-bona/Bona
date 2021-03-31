using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScriptExecutionWhen
{
    [InspectorName("컷씬 재생 중에는 실행 불가능")]
    NOT_IN_CUTSCENE,
    [InspectorName("컷씬과 함께 실행 가능")]
    WITH_CUTSCENE
}
