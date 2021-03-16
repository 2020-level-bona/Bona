using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Namespace
{
    string name;
    Dictionary<string, object> table;

    public Namespace(string name) : this(name, new Dictionary<string, object>()) {

    }

    public Namespace(string name, Dictionary<string, object> table) {
        this.name = name;
        this.table = table;
    }

    public bool GetBool(string key, bool def) {
        if (table.ContainsKey(key))
            return (bool) table[key];
        return def;
    }

    public int GetInt(string key, int def) {
        if (table.ContainsKey(key))
            return (int) table[key];
        return def;
    }

    public float GetFloat(string key, float def) {
        if (table.ContainsKey(key))
            return (float) table[key];
        return def;
    }

    public Vector2 GetVector2(string key, Vector2 def) {
        if (table.ContainsKey(key)) {
            Dictionary<string, object> vector = table[key] as Dictionary<string, object>;
            return new Vector2((float) vector["x"], (float) vector["y"]);
        }
        return def;
    }

    public List<T> GetList<T>(string key, List<T> def) {
        if (table.ContainsKey(key)) {
            List<T> result = new List<T>();
            foreach (object obj in table[key] as IList) {
                result.Add((T) obj);
            }
            return result;
        }
        return def;
    }

    public Dictionary<string, object> GetDictionary(string key, Dictionary<string, object> def) {
        if (table.ContainsKey(key)) {
            return table[key] as Dictionary<string, object>;
        }
        return def;
    }

    public Namespace GetNamespace(string key) {
        Namespace ns = new Namespace(key, GetDictionary(key, new Dictionary<string, object>()));
        return ns;
    }

    public void Set(string key, bool value) {
        table[key] = value;
    }

    public void Set(string key, int value) {
        table[key] = value;
    }

    public void Set(string key, float value) {
        table[key] = value;
    }

    public void Set(string key, Vector2 value) {
        Dictionary<string, object> vector = new Dictionary<string, object>();
        vector["x"] = value.x;
        vector["y"] = value.y;
        Set(key, vector);
    }

    public void Set(string key, IList value) {
        table[key] = value;
    }

    public void Set(string key, Dictionary<string, object> value) {
        table[key] = value;
    }

    public void Set(string key, Namespace value) {
        Set(key, value.table);
    }

    public void Remove(string key) {
        table.Remove(key);
    }

    public bool Contains(string key) {
        return table.ContainsKey(key);
    }
}
