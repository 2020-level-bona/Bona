using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTest : MonoBehaviour
{
    Game game;
    Level level;
    ChatQueue chatQueue;

    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatQueue = FindObjectOfType<ChatQueue>();
    }

    void Start() {
        EventManager.Instance.OnCharacterClicked += OnCharacterClicked;
    }

    void OnCharacterClicked(CharacterType type) {
        if (type == CharacterType.PRIEST) {
            chatQueue.AddChat(new Chat("안녕", level.GetSpawnedCharacter(type)));
        }
    }
}
