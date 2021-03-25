using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayCommand : IActionCommand
{
    ChatManager chatManager;
    Level level;
    CharacterType characterType;
    string message;

    public const string Keyword = "SAY";
    public bool Blocking => true;
    public int LineNumber {get;}

    public SayCommand(ChatManager chatManager, Level level, CharacterType characterType, string message) {
        this.chatManager = chatManager;
        this.level = level;
        this.characterType = characterType;
        this.message = message;
    }

    public SayCommand(ChatManager chatManager, Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.chatManager = chatManager;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.message = lineParser.GetString(2);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character == null)
            throw new BSCharacterNotSpawnedException(LineNumber, $"캐릭터[type={characterType}]가 존재하지 않습니다.");
        Chat chat = new Chat(message);
        character.ShowMessage(chat);
        
        yield return new WaitWhile(() => chat.Displaying);
    }
}
