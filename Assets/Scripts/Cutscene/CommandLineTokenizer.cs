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
                args.Add(EatToken(line, ref index));
        }

        return args;
    }

    static void EatSpaces(string line, ref int index) {
        while(index < line.Length && line[index] == ' ') {
            index++;
        }
    }

    static Token EatToken(string line, ref int index) {
        int start = index;
        while(index < line.Length && line[index] != ' ') {
            index++;
        }
        return new Token(line.Substring(start, index - start), start);
    }

    static Token EatString(int lineNumber, string line, ref int index) {
        index += 1;
        int start = index;
        while(index < line.Length && line[index] != '"') {
            index++;
        }
        
        if (index >= line.Length)
            throw new BSSyntaxException(lineNumber, "문자열이 닫히지 않았습니다.");
        
        Token token = new Token(line.Substring(start, index - start), start);
        index++; // 마지막 따옴표 스킵
        return token;
    }
}
