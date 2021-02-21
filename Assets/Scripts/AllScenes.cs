using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject btnTemplate = GameObject.Find("SceneBtn");

        foreach (string path in GetScenePaths()) {
            if (path.Contains("AllScene"))
                continue;
            
            GameObject gameObject = Instantiate(btnTemplate);
            gameObject.transform.SetParent(btnTemplate.transform.parent, false);

            Button button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(() => {
                LoadScene(path);
            });

            Text text = gameObject.transform.GetChild(0).GetComponent<Text>();
            text.text = path;
        }

        Destroy(btnTemplate);
    }

    string[] GetScenePaths() {
        string[] result = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < result.Length; i++) {
            result[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }
        return result;
    }

    void LoadScene(string path) {
        SceneManager.LoadScene(path);
        Debug.Log("Load Scene: " + path);
    }
}
