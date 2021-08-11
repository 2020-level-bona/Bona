using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    Game game;

    void Awake() {
        game = FindObjectOfType<Game>();
    }

    void Update () {
          if(Input.GetKeyDown(KeyCode.Escape)){
              if (GameIsPaused){
                  Resume();
              } else {
                  Pause();
              }
          }
    }

    public void Resume(){
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        game.overlayCanvas++;
    }
    void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        game.overlayCanvas--;
    }
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}