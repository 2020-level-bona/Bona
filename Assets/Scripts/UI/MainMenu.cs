using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickNewGame(){
        Debug.Log("시작하기");
    }

    public void OnClickQuit(){
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif

    }
}
