using System.Linq.Expressions;
using System;
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
            
            if (EatItem(line, expression, ref index))
                continue;
            
            if (EatDist(line, expression, ref index))
                continue;
            
            if (EatHand(line, expression, ref index))
                continue;
            
            string token = EatToken(line, ref index);
            if (token.ToLower() == "true")
                expression.Add(true);
            else if (token.ToLower() == "false")
                expression.Add(false);
            else {
                object value = Session.Instance.Get(token);
                if (value is null || value is bool || value is long || value is double)
                    expression.Add(value);
                else
                    throw new BSUnsupportedTypeException(-1, $"변수 {token}에 대한 타입 {value.GetType()}은(는) 지원되지 않습니다.");
            }
        }

        return expression;
    }

    static void EatSpaces(string line, ref int index) {
        while(index < line.Length && line[index] == ' ') {
            index++;
        }
    }

    static Regex doubleRegex = new Regex(@"^\d+\.\d+");
    static Regex longRegex = new Regex(@"^\d+");
    static bool EatNumber(string line, List<object> expression, ref int index) {
        if (char.IsDigit(line[index])) {
            Match match = doubleRegex.Match(line.Substring(index));
            if (match.Success) {
                expression.Add(double.Parse(match.Value));
                index += match.Value.Length;
                return true;
            }
            match = longRegex.Match(line.Substring(index));
            if (match.Success) {
                expression.Add(long.Parse(match.Value));
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
        } else if (line.Substring(index).StartsWith("||") || line.Substring(index).ToLower().StartsWith("or")) {
            expression.Add(new OrOp());
            index += 2;
            return true;
        } else if (line.Substring(index).StartsWith("&&")) {
            expression.Add(new AndOp());
            index += 2;
            return true;
        } else if (line.Substring(index).ToLower().StartsWith("and")) {
            expression.Add(new AndOp());
            index += 3;
            return true;
        } else if (line.Substring(index).StartsWith("<=")) {
            expression.Add(new GreaterEqualOp(false));
            index += 2;
            return true;
        } else if (line.Substring(index).StartsWith(">=")) {
            expression.Add(new GreaterEqualOp(true));
            index += 2;
            return true;
        } else if (line[index] == '<') {
            expression.Add(new GreaterOp(false));
            index++;
            return true;
        } else if (line[index] == '>') {
            expression.Add(new GreaterOp(true));
            index++;
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

    static Regex itemContainsRegex = new Regex(@"contains\(([^,]*),([^,]*)\)");
    static bool EatItem(string line, List<object> expression, ref int index) {
        Match match = itemContainsRegex.Match(line.Substring(index));
        if (match.Success && match.Index == 0) {
            string itemTypeStr = match.Groups[1].Value;
            string itemCountStr = match.Groups[2].Value;

            ItemType itemType;
            try {
                itemType = (ItemType) System.Enum.Parse(typeof(ItemType), itemTypeStr, true);
            } catch {
                throw new BSSyntaxException(-1, $"{itemTypeStr}은(는) 올바른 아이템 타입 열거형이 아닙니다.");
            }

            int itemCount;
            try {
                itemCount = int.Parse(itemCountStr);
            } catch {
                throw new BSSyntaxException(-1, $"{itemCountStr}을(를) 정수 타입으로 변환할 수 없습니다.");
            }

            Inventory inventory = Game.Instance.inventory;

            index += match.Length;
            expression.Add(inventory?.ContainsItem(new Item(itemType, itemCount)) ?? false);
            return true;
        }
        return false;
    }

    static Regex eatDistRegex = new Regex(@"dist\(([^,]*),([^,]*)\)");
    static bool EatDist(string line, List<object> expression, ref int index) {
        Match match = eatDistRegex.Match(line.Substring(index));
        if (match.Success && match.Index == 0) {
            string characterATypeStr = match.Groups[1].Value;
            string characterBTypeStr = match.Groups[2].Value;

            CharacterType characterAType;
            try {
                characterAType = (CharacterType) System.Enum.Parse(typeof(CharacterType), characterATypeStr, true);
            } catch {
                throw new BSSyntaxException(-1, $"{characterATypeStr}은(는) 올바른 캐릭터 타입명이 아닙니다.");
            }

            CharacterType characterBType;
            try {
                characterBType = (CharacterType) System.Enum.Parse(typeof(CharacterType), characterBTypeStr, true);
            } catch {
                throw new BSSyntaxException(-1, $"{characterBTypeStr}은(는) 올바른 캐릭터 타입명이 아닙니다.");
            }

            index += match.Length;

            Level level = Game.Instance.level;
            if (level == null) {
                expression.Add(float.PositiveInfinity);
            } else {
                Character characterA = level.GetSpawnedCharacter(characterAType);
                Character characterB = level.GetSpawnedCharacter(characterBType);
                if (characterA == null || characterB == null) {
                    expression.Add(float.PositiveInfinity);
                } else {
                    expression.Add(Vector2.Distance(characterA.transform.position, characterB.transform.position));
                }
            }

            return true;
        }
        return false;
    }

    static Regex eatHandRegex = new Regex(@"hand\((.+)\)");
    static bool EatHand(string line, List<object> expression, ref int index) {
        Match match = eatHandRegex.Match(line.Substring(index));
        if (match.Success && match.Index == 0) {
            string itemTypeStr = match.Groups[1].Value;
            ItemType itemType;
            try {
                itemType = (ItemType) System.Enum.Parse(typeof(ItemType), itemTypeStr, true);
            } catch {
                throw new BSSyntaxException(-1, $"{itemTypeStr}은(는) 올바른 아이템 타입명이 아닙니다.");
            }
            index += match.Length;

            Inventory inventory = Game.Instance.inventory;
            Item itemSelected = inventory.GetSelectedItem();
            if (itemSelected == null || itemSelected.type != itemType) expression.Add(false);
            else expression.Add(true);

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
