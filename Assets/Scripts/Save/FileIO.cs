using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 비동기 처리
public class FileIO
{
    public const string PATH = @"/save.json";

    public void Write(string content) {
        System.IO.File.WriteAllText(Application.persistentDataPath + PATH, content, System.Text.Encoding.UTF8);
    }

    public string Read() {
        if (!System.IO.File.Exists(Application.persistentDataPath + PATH))
            return null;
        return System.IO.File.ReadAllText(Application.persistentDataPath + PATH, System.Text.Encoding.UTF8);
    }
}
