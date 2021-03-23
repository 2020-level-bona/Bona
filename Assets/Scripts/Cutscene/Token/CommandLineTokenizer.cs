using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandLineTokenizer
{
    public static List<Token> Tokenize(int lineNumber, string line) {
        List<Token> args = new List<Token>();

        int index = 0;
        while(index < line.Length) {
            EatSpaces(line, ref index);
            if (index >= line.Length)
                break;
            if (line[index] == '"')
                args.Add(EatString(lineNumber, line, ref index));
            else
                args.Add(EatToken(lineNumber, line, ref index));
        }

        return args;
    }

    static void EatSpaces(string line, ref int index) {
        while(index < line.Length && line[index] == ' ') {
            index++;
        }
    }

    static Token EatToken(int lineNumber, string line, ref int index) {
        int start = index;
        while(index < line.Length && line[index] != ' ') {
            index++;
        }
        return new Token(lineNumber, line.Substring(start, index - start), start);
    }

    // 따옴표를 포함하여 가져옴
    static Token EatString(int lineNumber, string line, ref int index) {
        int start = index;
        index++; // 첫번째 따옴표를 넘겨야 함
        while(index < line.Length && line[index] != '"') {
            index++;
        }
        
        // 따옴표가 정상적으로 닫혔다면 그 따옴표까지 포함
        if (index < line.Length)
            index++;

        Token token = new Token(lineNumber, line.Substring(start, index - start), start);
        return token;
    }
}
