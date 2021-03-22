using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCommand : IScriptCommand
{
    ChatQueue chatQueue;
    Level level;
    CharacterType characterType;
    string message;

    public bool Blocking => true;

    public MessageCommand(ChatQueue chatQueue, Level level, CharacterType characterType, string message) {
        this.chatQueue = chatQueue;
        this.level = level;
        this.characterType = characterType;
        this.message = message;
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character == null)
            throw new System.Exception($"캐릭터[{characterType}]가 존재하지 않습니다.");
        chatQueue.AddChat(new Chat(message, character));
        
        yield return new WaitWhile(() => chatQueue.IsDisplaying);
    }
}
