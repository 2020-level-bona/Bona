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

    public bool IsPlayingCutscene {get; private set;} = false;

    void Awake() {
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();
        cameraController = FindObjectOfType<CameraController>();

        inventory = new Inventory();
    }

    void Start() {
        cameraController.AddCameraOperator(new FollowCharacters(cameraController, level.GetSpawnedCharacter(CharacterType.BONA)));
    }

    void Update() {
        Tween.Instance.Update();
    }

    public void TransferScene(string sceneName) {
        Session.Instance.Save();

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
        return new ScriptSession(level, chatManager, commandProvider, this);
    }
}
