using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCommand : IScriptCommand
{
    ChatManager chatManager;
    Level level;
    CharacterType characterType;
    string message;

    public string Keyword => "MSG";
    public bool Blocking => true;

    public MessageCommand(ChatManager chatManager, Level level, CharacterType characterType, string message) {
        this.chatManager = chatManager;
        this.level = level;
        this.characterType = characterType;
        this.message = message;
    }

    public MessageCommand(ChatManager chatManager, Level level, CommandLineParser lineParser) {
        this.chatManager = chatManager;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.message = lineParser.GetString(2);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character == null)
            throw new BSCharacterNotSpawnedException($"캐릭터[type={characterType}]가 존재하지 않습니다.");
        Chat chat = new Chat(message);
        character.ShowMessage(chat);
        
        yield return new WaitWhile(() => chat.Displaying);
    }
}
