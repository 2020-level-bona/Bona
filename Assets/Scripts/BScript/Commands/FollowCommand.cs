using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCommand : IActionCommand
{
    Game game;
    Level level;
    CharacterType followerCharacterType;
    CharacterType targetCharacterType;
    
    public const string Keyword = "FOLLOW";
    public bool Blocking => false;
    public int LineNumber {get;}

    public FollowCommand(Game game, Level level, CharacterType followerCharacterType, CharacterType targetCharacterType) {
        this.game = game;
        this.level = level;
        this.followerCharacterType = followerCharacterType;
        this.targetCharacterType = targetCharacterType;
    }

    public FollowCommand(Game game, Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.game = game;
        this.level = level;
        this.followerCharacterType = lineParser.GetCharacterType(1);
        this.targetCharacterType = lineParser.GetCharacterType(2);
    }

    public IEnumerator GetCoroutine() {
        Character follower = level.GetSpawnedCharacter(followerCharacterType);
        Character target = level.GetSpawnedCharacter(targetCharacterType);
        if (follower && target) {
            game.characterFollowManager.Follow(follower, target);
        }
        yield return null;
    }
}
