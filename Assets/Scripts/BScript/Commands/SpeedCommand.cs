using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCommand : IActionCommand
{
    Level level;
    CharacterType characterType;
    float speedMultiplier;
    
    public const string Keyword = "SPEED";
    public bool Blocking => false;
    public int LineNumber {get;}

    public SpeedCommand(Level level, CharacterType characterType, float speedMultiplier) {
        this.level = level;
        this.characterType = characterType;
        this.speedMultiplier = speedMultiplier;
    }

    public SpeedCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        speedMultiplier = lineParser.GetFloat(2);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character)
            character.movable.speedMultiplier = speedMultiplier;
        yield return null;
    }
}
