using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferCommand : IScriptCommand
{
    Game game;
    string sceneName;

    public const string Keyword = "TRANSFER";
    public bool Blocking => true;
    public int LineNumber {get;}

    public TransferCommand(Game game, string sceneName) {
        this.game = game;
        this.sceneName = sceneName;
    }

    public TransferCommand(Game game, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.game = game;
        this.sceneName = lineParser.GetString(1);
    }

    public IEnumerator GetCoroutine() {
        game.TransferScene(sceneName);
        yield return null;
    }
}
