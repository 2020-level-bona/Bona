using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCommand : IScriptCommand
{
    Level level;
    CharacterType characterType;

    public const string Keyword = "HIDE";
    public bool Blocking => false;
    public int LineNumber {get;}

    public HideCommand(Level level, CharacterType characterType) {
        this.level = level;
        this.characterType = characterType;
    }

    public HideCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character)
            character.gameObject.AddComponent<SpriteEffector>().Hide();

        yield return null;
    }
}
