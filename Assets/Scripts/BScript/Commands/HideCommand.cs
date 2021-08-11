using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCommand : IActionCommand
{
    Level level;
    CharacterType characterType;
    float duration = 1f;

    public const string Keyword = "HIDE";
    public bool Blocking => false;
    public int LineNumber {get;}

    public HideCommand(Level level, CharacterType characterType, float duration = 1f) {
        this.level = level;
        this.characterType = characterType;
        this.duration = duration;
    }

    public HideCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        if (lineParser.HasArgument(2)) this.duration = lineParser.GetFloat(2);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character)
            character.gameObject.AddComponent<SpriteEffector>().Hide(duration);

        yield return null;
    }
}
