﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour, ISession
{
    static Session instance;
    public static Session Instance {
        get {
            if (!instance)
                instance = FindObjectOfType<Session>();
            return instance;
        }
    }

    public static Namespace CurrentScene => Instance.GetNamespace("scene").GetNamespace(SceneManager.GetActiveScene().name);
    public static Namespace Inventory => Instance.GetNamespace("inventory");
    public static Namespace Story => Instance.GetNamespace("story");
    public static Namespace Setting => Instance.GetNamespace("setting");
    public static Namespace Statistic => Instance.GetNamespace("statistic");

    public SessionHolder sessionHolder;

    FileIO fileIO;

    void Awake() {
        fileIO = new FileIO();
    }

    public Namespace GetNamespace(string name) {
        if (sessionHolder.table == null)
            sessionHolder.table = new Dictionary<string, object>();
        if (!sessionHolder.table.ContainsKey(name))
            sessionHolder.table[name] = new Dictionary<string, object>();
        
        return new Namespace(name, sessionHolder.table[name] as Dictionary<string, object>);
    }

    public string Serialize() {
        return MiniJSON.Json.Serialize(sessionHolder.table ?? new Dictionary<string, object>());
    }

    public void Deserialize(string data) {
        sessionHolder.table = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
    }

    public void Clear() {
        sessionHolder.table = null;
    }

    public void Save() {
        EventManager.Instance.OnPreSave?.Invoke();

        fileIO.Write(Serialize());
    }

    public void Load() {
        string content = fileIO.Read();
        if (content != null)
            Deserialize(content);
    }
}
