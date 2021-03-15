using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public DataManager datamanager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickNewGame(){
        SceneManager.LoadScene("보나의 집 안");
    }



    public void OnClickContinue(){
        Debug.Log("계속하기");
        //dataManager.GameLoad();
    }



    public void OnClickQuit(){
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif

    }
}
