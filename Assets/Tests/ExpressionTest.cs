using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ExpressionTest
    {
        [Test]
        public void Tokenizer테스트()
        {
            List<object> expression = ExpressionTokenizer.Tokenize("1+2 - 4 !true");
            Assert.AreEqual(expression.Count, 7);
            Assert.AreEqual(expression[0], 1);
            Assert.IsTrue(expression[1] is AddOp);
            Assert.AreEqual(expression[2], 2);
            Assert.IsTrue(expression[3] is SubOp);
            Assert.AreEqual(expression[4], 4);
            Assert.IsTrue(expression[5] is NegOp);
            Assert.AreEqual(expression[6], true);

            expression = ExpressionTokenizer.Tokenize("1 == 2 !=3");
            Assert.AreEqual(expression.Count, 5);
            Assert.AreEqual(expression[0], 1);
            Assert.IsTrue(expression[1] is EqualOp);
            Assert.AreEqual(expression[2], 2);
            Assert.IsTrue(expression[3] is NotEqualOp);
            Assert.AreEqual(expression[4], 3);
        }

        [Test]
        public void 정수덧셈테스트()
        {
            Assert.AreEqual(Expression.Eval("1+2"), 3);
            Assert.AreEqual(Expression.Eval("4+23"), 27);
            Assert.AreEqual(Expression.Eval(" 22 +  11  "), 33);
        }

        [Test]
        public void 실수덧셈테스트()
        {
            Assert.AreEqual(Expression.Eval("1.1+2"), 3.1f);
            Assert.AreEqual(Expression.Eval("5+ 0.5"), 5.5f);
            Assert.AreEqual(Expression.Eval("0.2 + 0.3"), 0.5f);
        }

        [Test]
        public void 뺄셈테스트()
        {
            Assert.AreEqual(Expression.Eval("3-1"), 2);
            Assert.AreEqual(Expression.Eval("4-6"), -2);
            Assert.AreEqual(Expression.Eval("  1.2 - 0.2 "), 1.0f);
        }

        [Test]
        public void 덧셈뺄셈테스트()
        {
            Assert.AreEqual(Expression.Eval("1+2+3+4+5"), 15);
            Assert.AreEqual(Expression.Eval("1+2-3"), 0);
            Assert.AreEqual(Expression.Eval("4+8-3.0"), 9f);
        }

        [Test]
        public void 곱셈테스트()
        {
            Assert.AreEqual(Expression.Eval("2*4"), 8);
            Assert.AreEqual(Expression.Eval("3*1*2"), 6);
            Assert.AreEqual(Expression.Eval("0.2*0.25"), 0.2f * 0.25f);
        }

        [Test]
        public void 사칙연산테스트()
        {
            Assert.AreEqual(Expression.Eval("2*4+3"), 11);
            Assert.AreEqual(Expression.Eval("2+4*3"), 14);
            Assert.AreEqual(Expression.Eval("2*4+6*3"), 8+18);
            Assert.AreEqual(Expression.Eval("0.5 * 4 + 0.25 * 4"), 3);
            Assert.AreEqual(Expression.Eval("0.5 - 4 * 0.25 - 4"), -4.5f);
        }

        [Test]
        public void Equal테스트()
        {
            Assert.AreEqual(Expression.Eval("2==2"), true);
            Assert.AreEqual(Expression.Eval("3==2"), false);
            Assert.AreEqual(Expression.Eval("true == false"), false);
            Assert.AreEqual(Expression.Eval("false == false"), true);
            Assert.AreEqual(Expression.Eval("2 == 2.0"), true);
            Assert.AreEqual(Expression.Eval("2.1 == 2"), false);
            Assert.AreEqual(Expression.Eval("2.1 == 2.1"), true);
        }

        [Test]
        public void NullEqual테스트()
        {
            Assert.AreEqual(Expression.Eval("null == null"), true);
            Assert.AreEqual(Expression.Eval("2 == null"), false);
            Assert.AreEqual(Expression.Eval("null == 0"), true);
            Assert.AreEqual(Expression.Eval("0.0 == null"), true);
            Assert.AreEqual(Expression.Eval("0.1 == null"), false);
            Assert.AreEqual(Expression.Eval("true == null"), false);
            Assert.AreEqual(Expression.Eval("null == false"), true);
        }

        [Test]
        public void 복잡한Equal테스트()
        {
            Assert.AreEqual(Expression.Eval("2 == 1 + 1"), true);
            Assert.AreEqual(Expression.Eval("3 * 3 == 1 + 2 * 4"), true);
            Assert.AreEqual(Expression.Eval("3 == 3 == true"), true);
            Assert.AreEqual(Expression.Eval("true == false == 0"), false);
            Assert.AreEqual(Expression.Eval("2 == 2 == 2 == 2"), false);
            Assert.AreEqual(Expression.Eval("0.5 + 0.5 == 0.5 * 4 - 1"), true);
            Assert.AreEqual(Expression.Eval("null == 3 * 4 - 2 * 6"), true);
            Assert.AreEqual(Expression.Eval("null == false == null"), false);
            Assert.AreEqual(Expression.Eval("1 != 2 + 2 == false"), false);
            Assert.AreEqual(Expression.Eval("1 != 2 != 3"), true);
        }

        [Test]
        public void 괄호토큰테스트() {
            List<object> expression = ExpressionTokenizer.Tokenize("(12) ((true) hello)");
            Assert.AreEqual(expression.Count, 9);
            Assert.True(expression[0] is Parenthesis && (expression[0] as Parenthesis).open);
            Assert.AreEqual(expression[1], 12);
            Assert.True(expression[2] is Parenthesis && !(expression[2] as Parenthesis).open);
            Assert.True(expression[3] is Parenthesis && (expression[3] as Parenthesis).open);
            Assert.True(expression[4] is Parenthesis && (expression[4] as Parenthesis).open);
            Assert.AreEqual(expression[5], true);
            Assert.True(expression[6] is Parenthesis && !(expression[6] as Parenthesis).open);
            Assert.True(expression[8] is Parenthesis && !(expression[8] as Parenthesis).open);

            expression = ExpressionTokenizer.Tokenize("(((1)+2)+3)+4");
            Assert.AreEqual(expression.Count, 13);
            Assert.True(expression[0] is Parenthesis && (expression[0] as Parenthesis).open);
            Assert.True(expression[1] is Parenthesis && (expression[1] as Parenthesis).open);
            Assert.True(expression[2] is Parenthesis && (expression[2] as Parenthesis).open);
            Assert.AreEqual(expression[3], 1);
            Assert.True(expression[4] is Parenthesis && !(expression[4] as Parenthesis).open);
            Assert.True(expression[5] is AddOp);
            Assert.AreEqual(expression[6], 2);
            Assert.True(expression[7] is Parenthesis && !(expression[7] as Parenthesis).open);
            Assert.True(expression[8] is AddOp);
            Assert.AreEqual(expression[9], 3);
            Assert.True(expression[10] is Parenthesis && !(expression[10] as Parenthesis).open);
            Assert.True(expression[11] is AddOp);
            Assert.AreEqual(expression[12], 4);
        }

        [Test]
        public void 괄호테스트() {
            Assert.AreEqual(Expression.Eval("2 + 3 * 4"), 14);
            Assert.AreEqual(Expression.Eval("(2 + 3) * 4"), 20);
            Assert.AreEqual(Expression.Eval("(1 + 5) * (2 + 3)"), 30);
            Assert.AreEqual(Expression.Eval("(((1)))"), 1);
            Assert.AreEqual(Expression.Eval("(1)+2"), 3);
            Assert.AreEqual(Expression.Eval("(((1)+2)+3)+4"), 10);
            Assert.AreEqual(Expression.Eval("(((1)+2)+3)+4+(5)"), 15);
            Assert.AreEqual(Expression.Eval("true == (1 == 1)"), true);
        }

        [Test]
        public void 부정테스트() {
            Assert.AreEqual(Expression.Eval("!true"), false);
            Assert.AreEqual(Expression.Eval("!false"), true);
            Assert.AreEqual(Expression.Eval("!(1 == 2)"), true);
            // Assert.AreEqual(Expression.Eval("!!!!true"), true); Invalid Syntax (현재까지는)
            Assert.AreEqual(Expression.Eval("!false == true"), true);
            Assert.AreEqual(Expression.Eval("!false == !(true == false)"), true);
            Assert.AreEqual(Expression.Eval("!(!(!false)) == (1 == 2)"), false);
        }
    }
}
