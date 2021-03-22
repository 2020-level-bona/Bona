using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCommand : IScriptCommand
{
    Level level;
    CharacterType characterType;

    public string Keyword => "HIDE";
    public bool Blocking => false;

    public HideCommand(Level level, CharacterType characterType) {
        this.level = level;
        this.characterType = characterType;
    }

    public IEnumerator GetCoroutine() {
        Character character = level.GetSpawnedCharacter(characterType);
        if (character)
            character.Hide();

        yield return null;
    }
}
