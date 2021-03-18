using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Session : ISession
{
    static Session instance;
    public static Session Instance {
        get {
            if (instance == null) {
                instance = new Session();
            }
            return instance;
        }
    }

    public static Namespace CurrentScene => Instance.GetNamespace("scene").GetNamespace(SceneManager.GetActiveScene().name);
    public static Namespace Inventory => Instance.GetNamespace("inventory");
    public static Namespace Story => Instance.GetNamespace("story");
    public static Namespace Setting => Instance.GetNamespace("setting");
    public static Namespace Statistic => Instance.GetNamespace("statistic");

    // public SessionHolder sessionHolder;
    Dictionary<string, object> table; // @Temporary

    FileIO fileIO;

    private Session() {
        fileIO = new FileIO();

        Load();
    }

    public Namespace GetNamespace(string name) {
        if (table == null)
            table = new Dictionary<string, object>();
        if (!table.ContainsKey(name))
            table[name] = new Dictionary<string, object>();
        
        return new Namespace(name, table[name] as Dictionary<string, object>);
    }

    public string Serialize() {
        return MiniJSON.Json.Serialize(table ?? new Dictionary<string, object>());
    }

    public void Deserialize(string data) {
        table = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
    }

    public void Clear() {
        table = null;
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
