using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILerpable<T>
{
    T Lerp(float t);
}
