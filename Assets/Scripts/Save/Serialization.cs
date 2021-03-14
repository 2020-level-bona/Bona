using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

  public class Serialization : MonoBehaviour {
     void Start () {
         var jsonString = "{ \"array\": [1.44,2,3], " +
                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
                           "\"int\": 65536, " +
                           "\"float\": 3.1415926, " +
                          "\"bool\": true, " +
                         "\"null\": null }";
 
          var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

          Debug.Log("deserialized: " + dict.GetType());
          Debug.Log("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
          Debug.Log("dict['string']: " + (string) dict["string"]);
          Debug.Log("dict['float']: " + (double) dict["float"]); // floats come out as doubles
          Debug.Log("dict['int']: " + (long) dict["int"]); // ints come out as longs
          Debug.Log("dict['unicode']: " + (string) dict["unicode"]);
 
           var str = Json.Serialize(dict);

          Debug.Log("serialized: " + str);
      }
  }


/* List <T>
[Serializable]
public  class Serialization <T>
{
    [SerializeField]
    List <T> target;
    public List <T> ToList () { return target;}

    public Serialization (List <T> target)
    {
        this .target = target;
    }
}

// Dictionary <TKey, TValue>
[Serializable]
public  class Serialization <TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List <TKey> keys;
    [SerializeField]
    List <TValue> values;

    Dictionary <TKey, TValue> target;
    public Dictionary <TKey, TValue> ToDictionary () { return target;}

    public Serialization (Dictionary <TKey, TValue> target)
    {
        this .target = target;
    }

    public  void OnBeforeSerialize ()
    {
        keys = new List <TKey> (target.Keys);
        values = new List <TValue> (target.Values);
    }

    public  void OnAfterDeserialize ()
    {
        var count = Math.Min (keys.Count, values.Count);
        target = new Dictionary <TKey, TValue> (count);
         for (var i = 0 ; i <count; ++ i)
        {
            target.Add (keys[i], values​​[i]);
        }
    }
}*/