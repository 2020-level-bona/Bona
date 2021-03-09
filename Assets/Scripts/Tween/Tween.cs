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

    public static ITweenEntry Add(UnityEngine.Object owner, Action<float> action, float from, float to, float duration, Action finishCallback = null) {
        ITweenEntry entry = new TweenValueEntry<float>(owner, action, new FloatLerp(from, to), duration, finishCallback);
        Instance.entries.Add(entry);
        return entry;
    }

    public static ITweenEntry Add(UnityEngine.Object owner, Action<Vector2> action, Vector2 from, Vector2 to, float duration, Action finishCallback = null) {
        ITweenEntry entry = new TweenValueEntry<Vector2>(owner, action, new Vector2Lerp(from, to), duration, finishCallback);
        Instance.entries.Add(entry);
        return entry;
    }

    public static ITweenEntry Add(Movable movable, Vector2 to, float speed, Action finishCallback = null) {
        ITweenEntry entry = new TweenPathEntry(movable, new Path(PathType.LINEAR, new Vector2[] {movable.position, to}), speed, finishCallback);
        Instance.entries.Add(entry);
        return entry;
    }

    public static ITweenEntry Add(Movable movable, Path path, float speed, Action finishCallback = null) {
        ITweenEntry entry = new TweenPathEntry(movable, path, speed, finishCallback);
        Instance.entries.Add(entry);
        return entry;
    }
}
