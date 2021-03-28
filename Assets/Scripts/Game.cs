using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public SceneContext sceneContext;

    Level level;
    ChatManager chatManager;
    CameraController cameraController;

    public Inventory inventory;

    void Awake() {
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();
        cameraController = FindObjectOfType<CameraController>();

        inventory = new Inventory();
    }

    void Start() {
        Character bona = level.GetSpawnedCharacter(CharacterType.BONA);
        cameraController.MoveInstantly(bona.movable.GetCenter());
        cameraController.AddCameraOperator(new FollowCharacters(cameraController, CameraController.DEFAULT_SPEED, bona));
    }

    void Update() {
        Tween.Instance.Update();
    }

    public void TransferScene(string sceneName) {
        // Session.Instance.Save();

        sceneContext.LastScenePath = SceneManager.GetActiveScene().path;
        SceneManager.LoadScene(sceneName);
    }

    public Vector2 GetPlayerSpawnPoint(Vector2 defaultSpawnPoint) {
        foreach (TransferMap transferMap in FindObjectsOfType<TransferMap>()) {
            if (transferMap.targetScene != null && transferMap.targetScene.ScenePath == sceneContext.LastScenePath)
                return transferMap.transform.position;
        }
        return defaultSpawnPoint;
    }

    public ScriptSession CreateScriptSession(ICommandProvider commandProvider) {
        return new ScriptSession(this, level, chatManager, commandProvider, this);
    }
}
