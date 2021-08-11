using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferCommand : IActionCommand
{
    Game game;
    string sceneName;
    bool save = true;

    public const string Keyword = "TRANSFER";
    public bool Blocking => true;
    public int LineNumber {get;}

    public TransferCommand(Game game, string sceneName, bool save = true) {
        this.game = game;
        this.sceneName = sceneName;
        this.save = save;
    }

    public TransferCommand(Game game, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.game = game;
        this.sceneName = lineParser.GetString(1);
        if (lineParser.HasArgument(2)) this.save = lineParser.GetBool(2);
    }

    public IEnumerator GetCoroutine() {
        game.TransferScene(sceneName, save);
        yield return null;
    }
}
