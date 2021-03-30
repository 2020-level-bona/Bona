using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimCommand : IActionCommand
{
    string stateName;
    float duration;
    CharacterType characterType;
    Level level;

    public const string Keyword = "PLAYANIM";
    public bool Blocking => false;
    public int LineNumber {get;}

    public PlayAnimCommand(Level level, CharacterType characterType, string stateName, float duration = -1) {
        this.level = level;
        this.characterType = characterType;
        this.stateName = stateName;
        this.duration = duration;
    }

    public PlayAnimCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        stateName = lineParser.GetString(2);
        if (lineParser.HasArgument(3))
            duration = lineParser.GetFloat(3);
        else
            duration = -1;
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character)
            character.AddAnimationController(new SingleAnimationController(stateName, duration));
        yield return null;
    }
}
