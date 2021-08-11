using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    static Game instance;
    public static Game Instance {
        get {
            if (!instance) {
                instance = FindObjectOfType<Game>();
            }
            return instance;
        }
    }

    public Level level;
    ChatManager chatManager;
    CameraController cameraController;

    public Inventory inventory;

    public CharacterFollowManager characterFollowManager;

    public int overlayCanvas = 0;

    void Awake() {
        level = FindObjectOfType<Level>();
        chatManager = FindObjectOfType<ChatManager>();
        cameraController = FindObjectOfType<CameraController>();

        inventory = new Inventory();

        characterFollowManager = FindObjectOfType<CharacterFollowManager>();

        ScriptSession.cutsceneSessionSemaphore = 0; // Dirty @Hack
    }

    void Start() {
        Character controlledCharacter = level.GetControlledCharacter();
        controlledCharacter.movable.MoveTo(GetPlayerSpawnPoint(controlledCharacter.movable.position));

        cameraController.MoveInstantly(controlledCharacter.movable.GetCenter());
        cameraController.AddCameraOperator(new FollowCharacters(cameraController, CameraController.DEFAULT_SPEED, controlledCharacter));
    }

    void Update() {
        Tween.Instance.Update();
    }

    public void TransferScene(string sceneName, bool save = true) {
        // @Hardcoded
        if (sceneName != "MainMenu") {
            Session.General.Set("lastScenePath", SceneManager.GetActiveScene().path);
            Session.General.Set("currentScene", sceneName);
            Session.Instance.Save(save);
        }
        SceneManager.LoadScene(sceneName);
    }

    public Vector2 GetPlayerSpawnPoint(Vector2 defaultSpawnPoint) {
        foreach (TransferMap transferMap in FindObjectsOfType<TransferMap>()) {
            if (transferMap.targetScene != null && transferMap.targetScene.ScenePath == Session.General.GetString("lastScenePath"))
                return transferMap.transform.position;
        }
        return defaultSpawnPoint;
    }

    public ScriptSession CreateScriptSession(ICommandProvider commandProvider) {
        return new ScriptSession(this, level, chatManager, commandProvider, this);
    }
}
