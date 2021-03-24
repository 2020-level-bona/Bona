using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ExpressionTokenizer
{
    public static List<object> Tokenize(string line) {
        List<object> expression = new List<object>();

        int index = 0;
        while (index < line.Length) {
            EatSpaces(line, ref index);
            if (index >= line.Length)
                break;
            
            if (EatNumber(line, expression, ref index))
                continue;
            
            if (EatOperator(line, expression, ref index))
                continue;
            
            string token = EatToken(line, ref index);
            if (token.ToLower() == "true")
                expression.Add(true);
            else if (token.ToLower() == "false")
                expression.Add(false);
            else
                expression.Add(null); // TODO : 저장 세션에서 불러오기
        }

        return expression;
    }

    static void EatSpaces(string line, ref int index) {
        while(index < line.Length && line[index] == ' ') {
            index++;
        }
    }

    static bool EatNumber(string line, List<object> expression, ref int index) {
        if (char.IsDigit(line[index])) {
            Regex floatRegex = new Regex(@"^\d+\.\d+");
            Match match = floatRegex.Match(line.Substring(index));
            if (match.Success) {
                expression.Add(float.Parse(match.Value));
                index += match.Value.Length;
                return true;
            }
            Regex intRegex = new Regex(@"^\d+");
            match = intRegex.Match(line.Substring(index));
            if (match.Success) {
                expression.Add(int.Parse(match.Value));
                index += match.Value.Length;
                return true;
            }
        }
        return false;
    }

    static bool EatOperator(string line, List<object> expression, ref int index) {
        if (line.Substring(index).StartsWith("==")) {
            expression.Add(new EqualOp());
            index += 2;
            return true;
        } else if (line.Substring(index).StartsWith("!=")) {
            expression.Add(new NotEqualOp());
            index += 2;
            return true;
        } else if (line[index] == '(') {
            expression.Add(new Parenthesis(true));
            index++;
            return true;
        } else if (line[index] == ')') {
            expression.Add(new Parenthesis(false));
            index++;
            return true;
        } else if (line[index] == '!') {
            expression.Add(new NegOp());
            index++;
            return true;
        } else if (line[index] == '+') {
            expression.Add(new AddOp());
            index++;
            return true;
        } else if (line[index] == '-') {
            expression.Add(new SubOp());
            index++;
            return true;
        } else if (line[index] == '*') {
            expression.Add(new MultOp());
            index++;
            return true;
        }

        return false;
    }

    static string EatToken(string line, ref int index) {
        int start = index;
        while(index < line.Length && line[index] != ' ' && line[index] != '('
                && line[index] != ')' && line[index] != '!' && line[index] != '+'
                && line[index] != '-' && line[index] != '*') {
            index++;
        }
        return line.Substring(start, index - start);
    }
}
