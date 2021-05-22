using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCommand : IActionCommand
{
    Level level;
    CharacterType characterType;
    Vector2Face target;
    
    public const string Keyword = "SHOW";
    public bool Blocking => false;
    public int LineNumber {get;}

    public ShowCommand(Level level, CharacterType characterType, Vector2Face target) {
        this.level = level;
        this.characterType = characterType;
        this.target = target;
    }

    public ShowCommand(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.target = lineParser.GetVector2Face(level, 2);
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (!character) {
            character = level.SpawnCharacter(characterType, target);
            character.gameObject.AddComponent<SpriteEffector>().Show();
        }
        character.GetComponent<Movable>().MoveTo(target);
        character.GetComponent<Movable>().SetFace(target.face);

        yield return null;
    }
}
