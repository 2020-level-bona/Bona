using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour, ISession
{
    public static Session Instance;

    public SessionHolder sessionHolder;

    void Awake() {
        Instance = this;
    }

    public Namespace GetNamespace(string name) {
        if (sessionHolder.table == null || !sessionHolder.table.ContainsKey(name))
            return new Namespace(name);
        return new Namespace(name, sessionHolder.table[name] as Dictionary<string, object>);
    }

    public string Serialize() {
        return MiniJSON.Json.Serialize(sessionHolder.table ?? new Dictionary<string, object>());
    }

    public void Deserialize(string data) {
        sessionHolder.table = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
    }
}
