using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineParser : ICommandLineParser
{
    public int lineNumber {get;}
    public List<Token> args {get;}

    public CommandLineParser(int lineNumber, string line) {
        this.lineNumber = lineNumber;
        args = CommandLineTokenizer.Tokenize(lineNumber, line);
        ProcessComments();
    }

    void ProcessComments() {
        bool isComment = false;
        for (int i = 0; i < args.Count; i++) {
            if (args[i].str.StartsWith("//"))
                isComment = true;
            
            if (isComment)
                args[i].type = TokenType.COMMENT;
        }
    }

    void CheckIndex(int index) {
        if (index >= args.Count || args[index].type == TokenType.COMMENT)
            throw new BSSyntaxException(lineNumber, $"{index}번째 파라미터가 필요하지만 전달되지 않았습니다.");
    }

    public bool HasKeyword() {
        return args.Count > 0 && args[0].type != TokenType.COMMENT;
    }

    public string GetKeyword() {
        args[0].type = TokenType.KEYWORD;
        return args[0];
    }

    public bool ContainsFlag(string flag) {
        foreach (Token token in args) {
            if (token.str == flag) {
                token.type = TokenType.FLAG;
                return true;
            }
        }
        return false;
    }

    public string GetString(int index) {
        CheckIndex(index);
        args[index].type = TokenType.STR;
        if (args[index].str.StartsWith("\"")) {
            if (!args[index].str.EndsWith("\""))
                throw new BSSyntaxException(lineNumber, "문자열이 닫히지 않았습니다.");
            return args[index].str.Substring(1, Mathf.Max(args[index].str.Length - 2, 0));
        }
        return args[index];
    }

    public int GetInt(int index) {
        CheckIndex(index);
        try {
            int val = int.Parse(args[index]);
            args[index].type = TokenType.NUM;
            return val;
        } catch (System.Exception) {
            throw new BSSyntaxException(lineNumber, $"{args[index]}를 정수 타입으로 변환할 수 없습니다.");
        }
    }

    public float GetFloat(int index) {
        CheckIndex(index);
        try {
            float val = float.Parse(args[index]);
            args[index].type = TokenType.NUM;
            return val;
        } catch (System.Exception) {
            throw new BSSyntaxException(lineNumber, $"{args[index]}를 실수 타입으로 변환할 수 없습니다.");
        }
    }

    public CharacterType GetCharacterType(int index) {
        string type = GetString(index);
        try {
            CharacterType characterType = (CharacterType) System.Enum.Parse(typeof(CharacterType), type, true);
            args[index].type = TokenType.NAME;
            return characterType;
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
                args[index].type = TokenType.NUM;
                return new Vector2Face(new Vector2(x, y));
            } catch (System.Exception) {
                throw new BSSyntaxException(lineNumber, $"{str}을(를) Vector2 타입으로 변환할 수 없습니다.");
            }
        } else {
            Marker marker = level.GetMarker(str);
            if (marker == null)
                throw new BSMarkerNotFoundException(lineNumber, $"마커[name={str}]를 찾을 수 없습니다.");
            args[index].type = TokenType.NAME;
            return marker;
        }
    }
}
