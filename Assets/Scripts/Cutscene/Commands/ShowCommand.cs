﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCommand : IScriptCommand
{
    Level level;
    CharacterType characterType;
    Vector2Face target;
    
    public const string Keyword = "SHOW";
    public bool Blocking => false;

    public ShowCommand(Level level, CharacterType characterType, Vector2Face target) {
        this.level = level;
        this.characterType = characterType;
        this.target = target;
    }

    public ShowCommand(Level level, CommandLineParser lineParser) {
        this.level = level;
        this.characterType = lineParser.GetCharacterType(1);
        this.target = lineParser.GetVector2Face(level, 2);
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
