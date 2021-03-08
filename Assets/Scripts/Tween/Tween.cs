using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    static Tween instance;

    public static Tween Instance {
        get {
            if (instance == null) {
                instance = new Tween();
            }
            return instance;
        }
    }

    List<ITweenEntry> entries = new List<ITweenEntry>();

    public void Update() {
        entries.RemoveAll(x => x.HasDone());
        foreach (ITweenEntry entry in entries) {
            entry.Update();
        }
    }

    public static void Add(UnityEngine.Object owner, Action<float> action, float from, float to, float duration) {
        Instance.entries.Add(new TweenEntry<float>(owner, action, new FloatLerp(from, to), duration));
    }

    public static void Add(UnityEngine.Object owner, Action<Vector2> action, Vector2 from, Vector2 to, float duration) {
        Instance.entries.Add(new TweenEntry<Vector2>(owner, action, new Vector2Lerp(from, to), duration));
    }
}
