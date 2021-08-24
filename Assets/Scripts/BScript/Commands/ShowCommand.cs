using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCommand : IActionCommand
{
    Level level;
    CharacterType characterType;
    Vector2Face target;
    public float duration = 1f;
    
    public const string Keyword = "SHOW";
    public bool Blocking => false;
    public int LineNumber {get;}

    public ShowCommand(Level level, CharacterType characterType, Vector2Face target, float duration = 1f) {
        this.level = level;
        this.characterType = characterType;
        this.target = target;
        this.duration = 1f;
    }

    public ShowCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.target = lineParser.GetVector2Face(level, 2);
        if (lineParser.HasArgument(3)) this.duration = lineParser.GetFloat(3);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (!character) {
            character = level.SpawnCharacter(characterType, target);
            character.gameObject.AddComponent<SpriteEffector>().Show(duration);
        } else if (character.GetComponent<SpriteEffector>() && character.GetComponent<SpriteEffector>().hide) {
            character.GetComponent<SpriteEffector>().Show(duration);
        }
        character.GetComponent<Movable>().MoveTo(target);
        character.GetComponent<Movable>().SetFace(target.face);

        yield return null;
    }
}
