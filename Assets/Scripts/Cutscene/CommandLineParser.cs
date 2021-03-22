using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineParser : ICommandLineParser
{
    List<string> args;

    public CommandLineParser(string line) {
        args = CommandLineTokenizer.Tokenize(line);
    }

    public string GetKeyword() {
        return args[0];
    }

    public bool ContainsFlag(string flag) {
        return args.Contains(flag);
    }

    public string GetString(int index) {
        return args[index];
    }

    public int GetInt(int index) {
        return int.Parse(args[index]);
    }

    public float GetFloat(int index) {
        return float.Parse(args[index]);
    }
}
