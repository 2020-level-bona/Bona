using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    Coroutine coroutine;

    void Start() {
        Session.Instance.Load();
        continueButton.SetActive(Session.General.Contains("currentScene"));
        coroutine = null;
    }

    void Update() {
        Tween.Instance.Update();
    }

    public void OnClickNewGame(){
        if (coroutine != null) return;
        coroutine = StartCoroutine(NewGame());
    }

    IEnumerator NewGame() {
        FindObjectOfType<CameraFader>().FadeOut();
        yield return new WaitForSeconds(1.5f);
        Session.Instance.Clear();
        Session.General.Set("currentScene", "보나의 집 안");
        Session.Instance.Save();
        SceneManager.LoadScene("보나의 집 안");
    }

    public void OnClickContinue(){
        if (coroutine != null) return;
        coroutine = StartCoroutine(ContinueGame());
    }

    IEnumerator ContinueGame() {
        FindObjectOfType<CameraFader>().FadeOut();
        yield return new WaitForSeconds(1.5f);

        string currentScene = Session.General.GetString("currentScene");
        if (currentScene != null) SceneManager.LoadScene(currentScene);
    }

    public void OnClickQuit(){
        if (coroutine != null) return;
        coroutine = StartCoroutine(QuitGame());
    }

    IEnumerator QuitGame() {
        FindObjectOfType<CameraFader>().FadeOut();
        yield return new WaitForSeconds(1.5f);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
