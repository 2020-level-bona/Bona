using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraOperatorType
{
    REPLACE, // 카메라의 좌표는 해당 오퍼레이터의 좌표로 설정된다. 일반적인 카메라 움직임에 사용된다.
    ADDITIVE // 카메라의 좌표는 이전 오퍼레이터까지 계산된 좌표에 덧셈하여 설정된다. 카메라 진동 표현에 사용될 수 있다.
}
