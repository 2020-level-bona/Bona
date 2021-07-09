using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCommand : IActionCommand
{
    Level level;
    CharacterType characterType;
    bool isFloating;
    
    public const string Keyword = "FLOAT";
    public bool Blocking => false;
    public int LineNumber {get;}

    public FloatCommand(Level level, CharacterType characterType, bool isFloating) {
        this.level = level;
        this.characterType = characterType;
        this.isFloating = isFloating;
    }

    public FloatCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.isFloating = lineParser.GetBool(2);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        character.GetComponent<Movable>().ignoreRoad = isFloating;

        yield return null;
    }
}
