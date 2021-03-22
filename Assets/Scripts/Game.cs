using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public SceneContext sceneContext;

    CameraController cameraController;

    public bool IsPlayingCutscene {get; private set;} = false;

    void Awake() {
        cameraController = FindObjectOfType<CameraController>();
    }

    void Start() {
        Level level = FindObjectOfType<Level>();
        cameraController.AddCameraOperator(new FollowCharacters(cameraController, level.GetSpawnedCharacter(CharacterType.BONA)));
    }

    void Update() {
        Tween.Instance.Update();
    }

    public void TransferScene(SceneReference sceneReference) {
        Session.Instance.Save();

        sceneContext.LastScenePath = SceneManager.GetActiveScene().path;
        SceneManager.LoadScene(sceneReference);
    }

    public Vector2 GetPlayerSpawnPoint(Vector2 defaultSpawnPoint) {
        foreach (TransferMap transferMap in FindObjectsOfType<TransferMap>()) {
            if (transferMap.targetScene != null && transferMap.targetScene.ScenePath == sceneContext.LastScenePath)
                return transferMap.transform.position;
        }
        return defaultSpawnPoint;
    }
}
