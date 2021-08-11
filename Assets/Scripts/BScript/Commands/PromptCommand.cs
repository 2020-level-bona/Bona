using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptCommand : IActionCommand
{
    ChatManager chatManager;
    Level level;
    CharacterType characterType;
    string message;
    string variablePath;

    public const string Keyword = "PROMPT";
    public bool Blocking => true;
    public int LineNumber {get;}

    public PromptCommand(int lineNumber, ChatManager chatManager, Level level, CharacterType characterType, string variablePath, string message) {
        LineNumber = lineNumber;
        this.chatManager = chatManager;
        this.level = level;
        this.characterType = characterType;
        this.variablePath = variablePath;
        this.message = message;
    }

    public PromptCommand(ChatManager chatManager, Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.chatManager = chatManager;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.variablePath = lineParser.GetString(2);
        this.message = lineParser.GetString(3);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character == null)
            throw new BSCharacterNotSpawnedException(LineNumber, $"캐릭터[type={characterType}]가 존재하지 않습니다.");
        Chat chat = new Chat(message, variablePath);
        character.ShowMessage(chat);
        
        yield return new WaitWhile(() => chat.Displaying);
    }
}
