using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CommandLineTokenizerTest
    {
        [Test]
        public void 토큰테스트()
        {
            List<Token> args = CommandLineTokenizer.Tokenize(-1, "HELLO WORLD !!");
            Assert.AreEqual(args.Count, 3);
            Assert.AreEqual(args[0].str, "HELLO");
            Assert.AreEqual(args[1].str, "WORLD");
            Assert.AreEqual(args[2].str, "!!");
        }

        [Test]
        public void 두개이상_공백을가진_토큰테스트()
        {
            List<Token> args = CommandLineTokenizer.Tokenize(-1, "  HELLO      WORLD   !!    ");
            Assert.AreEqual(args.Count, 3);
            Assert.AreEqual(args[0].str, "HELLO");
            Assert.AreEqual(args[1].str, "WORLD");
            Assert.AreEqual(args[2].str, "!!");
        }

        [Test]
        public void 문자열테스트()
        {
            List<Token> args = CommandLineTokenizer.Tokenize(-1, "\"HELLO\" WORLD \"MY NAME IS\"");
            Assert.AreEqual(args.Count, 3);
            Assert.AreEqual(args[0].str, "HELLO");
            Assert.AreEqual(args[1].str, "WORLD");
            Assert.AreEqual(args[2].str, "MY NAME IS");
        }

        [Test]
        public void 문자열_두개이상_공백테스트() {
            List<Token> args = CommandLineTokenizer.Tokenize(-1, "\"HELLO  \"    WORLD   \"  MY   NAME IS \"  ");
            Assert.AreEqual(args.Count, 3);
            Assert.AreEqual(args[0].str, "HELLO  ");
            Assert.AreEqual(args[1].str, "WORLD");
            Assert.AreEqual(args[2].str, "  MY   NAME IS ");
        }

        [Test]
        public void 전체_공백테스트() {
            List<Token> args = CommandLineTokenizer.Tokenize(-1, "       ");
            Assert.AreEqual(args.Count, 0);
        }

        [Test]
        public void 예외_테스트() {
            try {
                List<Token> args = CommandLineTokenizer.Tokenize(-1, "\"HELLO\" WORLD \"MY NAME IS");
            } catch (BSException) {

            }
        }
    }
}
