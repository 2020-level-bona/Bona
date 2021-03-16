using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISession
{
    Namespace GetNamespace(string name);
    string Serialize();
    void Deserialize(string data);
    void Clear();
}
