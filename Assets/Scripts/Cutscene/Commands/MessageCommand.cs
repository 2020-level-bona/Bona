using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCommand : IScriptCommand
{
    ChatManager chatManager;
    Level level;
    CharacterType characterType;
    string message;

    public bool Blocking => true;

    public MessageCommand(ChatManager chatQueue, Level level, CharacterType characterType, string message) {
        this.chatManager = chatQueue;
        this.level = level;
        this.characterType = characterType;
        this.message = message;
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character == null)
            throw new System.Exception($"캐릭터[{characterType}]가 존재하지 않습니다.");
        Chat chat = new Chat(message);
        character.ShowMessage(chat);
        
        yield return new WaitWhile(() => chat.Displaying);
    }
}
