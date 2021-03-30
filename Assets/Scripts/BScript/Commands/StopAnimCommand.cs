using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimCommand : IActionCommand
{
    CharacterType characterType;
    Level level;

    public const string Keyword = "STOPANIM";
    public bool Blocking => false;
    public int LineNumber {get;}

    public StopAnimCommand(Level level, CharacterType characterType) {
        this.level = level;
        this.characterType = characterType;
    }

    public StopAnimCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character)
            character.ClearAnimationControllers();
        yield return null;
    }
}
