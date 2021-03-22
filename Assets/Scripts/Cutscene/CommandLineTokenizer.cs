using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandLineTokenizer
{
    public static List<string> Tokenize(int lineNumber, string line) {
        List<string> args = new List<string>();

        int shit = 0;
        while(line.Length > 0) {
            shit++;
            if (shit > 10)
                break;
            EatSpaces(ref line);
            if (line.Length == 0)
                break;
            if (line[0] == '"')
                args.Add(EatString(lineNumber, ref line));
            else
                args.Add(EatToken(ref line));
        }

        return args;
    }

    static void EatSpaces(ref string lineLeft) {
        int index = 0;
        while(index < lineLeft.Length && lineLeft[index] == ' ') {
            index++;
        }
        lineLeft = lineLeft.Substring(index);
    }

    static string EatToken(ref string lineLeft) {
        int index = 0;
        while(index < lineLeft.Length && lineLeft[index] != ' ') {
            index++;
        }
        string token = lineLeft.Substring(0, index);
        lineLeft = lineLeft.Substring(index);
        return token;
    }

    static string EatString(int lineNumber, ref string lineLeft) {
        int index = 1;
        while(index < lineLeft.Length && lineLeft[index] != '"') {
            index++;
        }
        
        if (index >= lineLeft.Length)
            throw new BSSyntaxException(lineNumber, "문자열이 닫히지 않았습니다.");
        
        string token = lineLeft.Substring(1, index - 1);
        lineLeft = lineLeft.Substring(index + 1);
        return token;
    }
}
