using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCommand : IScriptCommand
{
    Level level;
    CharacterType characterType;
    Vector2 target;

    public bool Blocking => false;

    public ShowCommand(Level level, CharacterType characterType, Vector2 target) {
        this.level = level;
        this.characterType = characterType;
        this.target = target;
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (!character) {
            character = level.SpawnCharacter(characterType, target);
            character.Show();
        }
        character.MoveTo(target);

        yield return null;
    }
}
