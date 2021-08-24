using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim2Command : IActionCommand
{
    string stateName;
    string movableName;
    Level level;

    public const string Keyword = "PLAYANIM2";
    public bool Blocking => false;
    public int LineNumber {get;}

    public PlayAnim2Command(Level level, string movableName, string stateName) {
        this.level = level;
        this.movableName = movableName;
        this.stateName = stateName;
    }

    public PlayAnim2Command(Level level, CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.level = level;
        this.movableName = lineParser.GetString(1);
        stateName = lineParser.GetString(2);
    }

    public IEnumerator GetCoroutine() {
        Movable movable = level.GetMovable(movableName);
        if (movable && movable.GetComponent<Animator>())
            movable.GetComponent<Animator>().Play(stateName);
        yield return null;
    }
}
