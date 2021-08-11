using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Namespace
{
    string name;
    Dictionary<string, object> table;

    public Namespace(string name, Dictionary<string, object> table) {
        this.name = name;
        this.table = table;
    }

    public bool GetBool(string key, bool def = false) {
        if (table.ContainsKey(key))
            return (bool) table[key];
        return def;
    }

    public int GetInt(string key, int def = 0) {
        if (table.ContainsKey(key))
            return System.Convert.ToInt32(table[key]);
        return def;
    }

    public long GetLong(string key, long def = 0) {
        if (table.ContainsKey(key))
            return System.Convert.ToInt64(table[key]);
        return def;
    }

    public float GetFloat(string key, float def = 0f) {
        if (table.ContainsKey(key))
            return System.Convert.ToSingle(table[key]);
        return def;
    }

    public double GetDouble(string key, double def = 0d) {
        if (table.ContainsKey(key))
            return System.Convert.ToDouble(table[key]);
        return def;
    }

    public string GetString(string key, string def = null) {
        if (table.ContainsKey(key))
            return table[key] as string;
        return def;
    }

    public Vector2 GetVector2(string key, Vector2 def) {
        if (table.ContainsKey(key)) {
            Dictionary<string, object> vector = table[key] as Dictionary<string, object>;
            return new Vector2((float) vector["x"], (float) vector["y"]);
        }
        return def;
    }

    public Item GetItem(string key, Item def = null) {
        if (table.ContainsKey(key)) {
            Dictionary<string, object> item = table[key] as Dictionary<string, object>;
            return new Item((ItemType) System.Enum.Parse(typeof(ItemType), item["type"] as string), System.Convert.ToInt32(item["quantity"]));
        }
        return def;
    }

    public List<T> GetList<T>(string key, List<T> def = null) {
        if (table.ContainsKey(key)) {
            List<T> result = new List<T>();
            foreach (object obj in table[key] as IList) {
                result.Add((T) obj);
            }
            return result;
        }
        return def;
    }

    private Dictionary<string, object> GetDictionary(string key) {
        return table[key] as Dictionary<string, object>;
    }

    public Namespace GetNamespace(string key) {
        if (!table.ContainsKey(key))
            table[key] = new Dictionary<string, object>();
        Namespace ns = new Namespace(key, GetDictionary(key));
        return ns;
    }

    public object GetRaw(string key) {
        if (!table.ContainsKey(key))
            return null;
        return table[key];
    }

    public void Set(string key, bool value) {
        table[key] = value;
    }

    public void Set(string key, long value) {
        table[key] = value;
    }

    public void Set(string key, double value) {
        table[key] = value;
    }

    public void Set(string key, string value) {
        table[key] = value;
    }

    public void Set(string key, Vector2 value) {
        Dictionary<string, object> vector = new Dictionary<string, object>();
        vector["x"] = value.x;
        vector["y"] = value.y;
        Set(key, vector);
    }

    public void Set(string key, Item value) {
        Dictionary<string, object> item = new Dictionary<string, object>();
        item["type"] = value.type.ToString();
        item["quantity"] = value.quantity;
        Set(key, item);
    }

    public void Set(string key, IList value) {
        table[key] = value;
    }

    private void Set(string key, Dictionary<string, object> value) {
        table[key] = value;
    }

    public void Set(string key, Namespace value) {
        Set(key, value.table);
    }

    public void SetRaw(string key, object value) {
        if (value is null || value is bool || value is long || value is double)
            table[key] = value;
        else if (value is int)
            table[key] = System.Convert.ToInt64(value);
        else if (value is float)
            table[key] = System.Convert.ToDouble(value);
        else
            throw new BSUnsupportedTypeException(-1, $"타입 {value.GetType()}은(는) SetRaw로 설정할 수 없습니다.");
    }

    public void Remove(string key) {
        table.Remove(key);
    }

    public bool Contains(string key) {
        return table.ContainsKey(key);
    }

    public string[] GetKeys() {
        return table.Keys.ToArray();
    }

    public void Clear() {
        table.Clear();
    }
}
