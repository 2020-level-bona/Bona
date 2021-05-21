using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElifCommand : IControlCommand
{
    public const string Keyword = "ELIF";
    public bool Blocking => true;
    public int LineNumber {get;}

    // 현재 라인에서의 조건식
    public string currentExpression;

    public ElifCommand(string currentExpression) {
        this.currentExpression = currentExpression;
    }

    public ElifCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        this.currentExpression = lineParser.GetAllStrings(1);

        // 구문 오류 확인용
        Expression.Eval(currentExpression);
    }

    public ICommandProvider GetCommandProvider() {
        return null;
    }
}
