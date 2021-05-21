using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : IControlCommand
{
    List<string> expressions = new List<string>();
    List<Queue<ICommand>> branchCommands = new List<Queue<ICommand>>();

    public const string Keyword = "IF";
    public bool Blocking => true;
    public int LineNumber {get;}

    // 현재 라인에서의 조건식
    public string currentExpression;

    public IfCommand(string currentExpression) {
        this.currentExpression = currentExpression;
    }

    public IfCommand(CommandLineParser lineParser) {
        LineNumber = lineParser.lineNumber;
        currentExpression = lineParser.GetAllStrings(1);

        // 구문 오류 확인용
        Expression.Eval(currentExpression);
    }

    public void AddBranch(string expression, Queue<ICommand> commands) {
        expressions.Add(expression);
        branchCommands.Add(commands);
    }

    public ICommandProvider GetCommandProvider() {
        for (int i = expressions.Count - 1; i >= 0; i--) {
            if (Expression.CastAsBool(Expression.Eval(expressions[i])))
                return new QueueCommandProvider(branchCommands[i]);
        }
        return null;
    }
}
