using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationController
{
    string GetState();
    bool HasDone();
}
