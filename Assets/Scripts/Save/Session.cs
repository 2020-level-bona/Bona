using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : MonoBehaviour, ISession
{
    public static Session Instance;

    public static Namespace CurrentScene => Instance.GetNamespace("scene").GetNamespace(SceneManager.GetActiveScene().name);
    public static Namespace Inventory => Instance.GetNamespace("inventory");

    public SessionHolder sessionHolder;

    FileIO fileIO;

    void Awake() {
        Instance = this;

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
        fileIO.Write(Serialize());
    }

    public void Load() {
        string content = fileIO.Read();
        if (content != null)
            Deserialize(content);
    }
}
