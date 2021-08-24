using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraOperator
{
    CameraOperatorType type {get;}

    Vector2 GetCameraPosition();

    void Update();

    void ResetState();
    
    void ForceFinish();

    bool HasDone();
}
