using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandLineParser
{
    string GetKeyword();
    bool ContainsFlag(string flag);
    string GetString(int index);
    int GetInt(int index);
    float GetFloat(int index);
    
}
