using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineParser : ICommandLineParser
{
    public int lineNumber {get;}
    List<string> args;

    public CommandLineParser(int lineNumber, string line) {
        this.lineNumber = lineNumber;
        args = CommandLineTokenizer.Tokenize(lineNumber, line);
    }

    void CheckIndex(int index) {
        if (index >= args.Count)
            throw new BSSyntaxException(lineNumber, $"{index}번째 파라미터가 필요하지만 전달되지 않았습니다.");
    }

    public string GetKeyword() {
        return args[0];
    }

    public bool ContainsFlag(string flag) {
        return args.Contains(flag);
    }

    public string GetString(int index) {
        CheckIndex(index);
        return args[index];
    }

    public int GetInt(int index) {
        CheckIndex(index);
        try {
            return int.Parse(args[index]);
        } catch (System.Exception) {
            throw new BSSyntaxException(lineNumber, $"{args[index]}를 정수 타입으로 변환할 수 없습니다.");
        }
    }

    public float GetFloat(int index) {
        CheckIndex(index);
        try {
            return float.Parse(args[index]);
        } catch (System.Exception) {
            throw new BSSyntaxException(lineNumber, $"{args[index]}를 실수 타입으로 변환할 수 없습니다.");
        }
    }

    public CharacterType GetCharacterType(int index) {
        string type = GetString(index);
        try {
            return (CharacterType) System.Enum.Parse(typeof(CharacterType), type);
        } catch (System.Exception) {
            throw new BSSyntaxException(lineNumber, $"{args[index]}은(는) 올바른 캐릭터 타입명이 아닙니다.");
        }
    }

    public Vector2Face GetVector2Face(Level level, int index) {
        string str = GetString(index);
        if (str.Contains(",")) {
            try {
                string[] spl = str.Split(',');
                float x = float.Parse(spl[0]);
                float y = float.Parse(spl[1]);
                return new Vector2Face(new Vector2(x, y));
            } catch (System.Exception) {
                throw new BSSyntaxException(lineNumber, $"{str}을(를) Vector2 타입으로 변환할 수 없습니다.");
            }
        } else {
            Marker marker = level.GetMarker(str);
            if (marker == null)
                throw new BSMarkerNotFoundException(lineNumber, $"마커[name={str}]를 찾을 수 없습니다.");
            return marker;
        }
    }
}
