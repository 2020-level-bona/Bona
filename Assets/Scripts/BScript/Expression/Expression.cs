using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expression
{
    public static object Eval(string str) {
        List<object> expression = ExpressionTokenizer.Tokenize(str);
        return EvalSub(expression, 0, expression.Count);
    }

    public static bool CastAsBool(object value) {
        if (value is null)
            return false;
        if (value is bool)
            return (bool) value;
        if (value is int || value is long)
            return System.Convert.ToInt64(value) != 0;
        if (value is float || value is double)
            return System.Convert.ToDouble(value) != 0;
        return false;
    }

    static object EvalSub(List<object> expression, int begin, int end) {
        if (begin == end)
            return null;
        
        Op op = null;
        int opIndex = -1;
        int level = 0;
        for (int i = end - 1; i >= begin; i--) {
            if (expression[i] is Parenthesis) {
                if ((expression[i] as Parenthesis).open)
                    level--;
                else
                    level++;
                
                if (level < 0)
                    throw new BSSyntaxException(-1, "괄호가 맞지 않습니다.");
            } else if(expression[i] is Op) {
                if (level > 0)
                    continue;
                
                if (op == null || op.Priority > (expression[i] as Op).Priority) {
                    op = expression[i] as Op;
                    opIndex = i;
                }
            }
        }

        if (op == null) {
            // 괄호를 포함한다면 최소 3개의 토큰이 있을 것
            if (begin < end - 2 && expression[begin] is Parenthesis
                && (expression[begin] as Parenthesis).open && expression[end - 1] is Parenthesis
                && !(expression[end - 1] as Parenthesis).open)
                return EvalSub(expression, begin + 1, end - 1);
            if (begin + 1 != end)
                throw new BSSyntaxException(-1, "잘못된 구문입니다.");
            return expression[begin];
        }

        if (op is BinaryOp) {
            if (opIndex == begin || opIndex == end - 1)
                throw new BSSyntaxException(-1, "잘못된 구문입니다.");
            return (op as BinaryOp).Do(EvalSub(expression, begin, opIndex), EvalSub(expression, opIndex + 1, end));
        }

        // UnaryOp은 BinaryOp보다 우선순위가 높기때문에
        // UnaryOp가 선택되었다면 BinaryOp가 남아있으면 안된다.
        // 또한 항상 맨 앞에 위치하여야 한다.
        if (op is UnaryOp) {
            if (opIndex != begin)
                throw new BSSyntaxException(-1, "잘못된 구문입니다.");
            return (op as UnaryOp).Do(EvalSub(expression, opIndex + 1, end));
        }

        throw new BSSyntaxException(-1, "지원하지 않는 연산자가 포함되어 있습니다.");
    }
}
