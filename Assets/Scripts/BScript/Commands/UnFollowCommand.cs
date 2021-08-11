using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnFollowCommand : IActionCommand
{
    Game game;
    Level level;
    CharacterType followerCharacterType;
    
    public const string Keyword = "UNFOLLOW";
    public bool Blocking => false;
    public int LineNumber {get;}

    public UnFollowCommand(Game game, Level level, CharacterType followerCharacterType) {
        this.game = game;
        this.level = level;
        this.followerCharacterType = followerCharacterType;
    }

    public UnFollowCommand(Game game, Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.game = game;
        this.level = level;
        this.followerCharacterType = lineParser.GetCharacterType(1);
    }

    public IEnumerator GetCoroutine() {
        Character follower = level.GetSpawnedCharacter(followerCharacterType);
        if (follower) {
            game.characterFollowManager.UnFollow(follower);
        }
        yield return null;
    }
}
