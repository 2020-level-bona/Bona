using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatLerp : ILerpable<float>
{
    public float from, to;

    public FloatLerp(float from, float to) {
        this.from = from;
        this.to = to;
    }

    public float Lerp(float t) {
        return Mathf.Lerp(from, to, t);
    }
}
