using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public SceneContext sceneContext;

    CameraController cameraController;

    void Awake() {
        cameraController = FindObjectOfType<CameraController>();
    }

    void Start() {
        Level level = FindObjectOfType<Level>();
        cameraController.AddCameraOperator(new FollowTransform(level.GetSpawnedCharacter(CharacterType.BONA).transform, cameraController));
    }

    void Update() {
        Tween.Instance.Update();
    }

    public void TransferScene(SceneReference sceneReference) {
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

    public void StartCutscene(IEnumerator coroutine) {
        StartCoroutine(new CutsceneEnumerator(coroutine));
    }
}
