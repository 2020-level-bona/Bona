using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class SessionTest
    {
        [Test]
        public void 씬에세션이존재하는가()
        {
            Session session = ReadySession();
            Assert.NotNull(session);
        }

        [Test]
        public void Bool저장테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test");

            Assert.False(ns.GetBool("myBool", false));
            
            ns.Set("myBool", true);

            Assert.True(ns.GetBool("myBool", true));
        }

        [Test]
        public void Int저장테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test");

            Assert.AreEqual(ns.GetInt("myInt", -1), -1);
            
            ns.Set("myInt", 3);

            Assert.AreEqual(ns.GetInt("myInt", -1), 3);
        }

        [Test]
        public void String저장테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test");

            Assert.Null(ns.GetString("myStr", null));
            
            ns.Set("myStr", "Hello World");

            Assert.AreEqual(ns.GetString("myStr", null), "Hello World");
        }

        [Test]
        public void Float저장테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test");

            Assert.AreEqual(ns.GetFloat("myFloat", 0f), 0f);
            
            ns.Set("myFloat", Mathf.PI);

            Assert.AreEqual(ns.GetFloat("myFloat", 0f), Mathf.PI);
        }

        [Test]
        public void Vector2저장테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test");

            Assert.AreEqual(ns.GetVector2("myVec", Vector2.zero), Vector2.zero);
            
            ns.Set("myVec", new Vector2(1.23f, 4.56f));

            Assert.AreEqual(ns.GetVector2("myVec", Vector2.zero), new Vector2(1.23f, 4.56f));
        }

        [Test]
        public void List저장테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test");

            Assert.AreEqual(ns.GetList<int>("myList", new List<int>()).Count, 0);
            
            ns.Set("myList", new List<int>(new int[]{1, 2, 3}));

            Assert.AreEqual(ns.GetList<int>("myList", new List<int>()), new List<int>(new int[]{1, 2, 3})); 
        }

        [Test]
        public void 이중네임스페이스_저장테스트() {
            Session session = ReadySession();
            Namespace ns1 = session.GetNamespace("Test1");
            Namespace ns2 = ns1.GetNamespace("Test2");

            ns1.Set("myInt", 1);
            ns2.Set("myInt", 2);

            Assert.AreEqual(session.GetNamespace("Test1").GetInt("myInt", 0), 1);
            Assert.AreEqual(session.GetNamespace("Test1").GetNamespace("Test2").GetInt("myInt", 0), 2);
        }

        [Test]
        public void 직렬화_테스트() {
            Session session = ReadySession();
            Namespace ns1 = session.GetNamespace("Test1");
            Namespace ns2 = session.GetNamespace("Test2");
            Namespace ns3 = ns1.GetNamespace("Test3");

            ns1.Set("myBool", true);
            ns2.Set("myInt", 3);
            ns3.Set("myStr", "Hello");

            string data = session.Serialize();
            Debug.Log(data);
            session.Clear();
            session.Deserialize(data);

            Assert.AreEqual(session.GetNamespace("Test1").GetBool("myBool", false), true);
            Assert.AreEqual(session.GetNamespace("Test2").GetInt("myInt", 0), 3);
            Assert.AreEqual(session.GetNamespace("Test1").GetNamespace("Test3").GetString("myStr", null), "Hello");
        }

        [Test]
        public void 딕셔너리_공유_테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test1");
            Namespace ns_ = session.GetNamespace("Test1");

            ns.Set("myInt", 5);

            Assert.AreEqual(ns.GetInt("myInt", 0), 5);
            Assert.AreEqual(ns_.GetInt("myInt", 0), 5);
        }

        [Test]
        public void 지원하지_않는_오브젝트_테스트() {
            Session session = ReadySession();
            Namespace ns = session.GetNamespace("Test1");
            
            ns.Set("strangeList", new List<Vector3>(){Vector3.zero, Vector3.one, Vector3.forward});
            try {
                string data = session.Serialize();
                Assert.Fail("예외가 발생되지 않았습니다.");
            } catch (System.Exception) {
                // PASS
            }
        }
        
        [Test]
        public void Swap_테스트() {
            Session session = ReadySession();
            
            session.GetNamespace("Test").Set("myStr", "first");
            string first = session.Serialize();
            session.Clear();

            session.GetNamespace("Test").Set("myStr", "second");
            string second = session.Serialize();
            session.Clear();

            session.Deserialize(first);
            Assert.AreEqual(session.GetNamespace("Test").GetString("myStr", null), "first");
            
            session.Deserialize(second);
            Assert.AreEqual(session.GetNamespace("Test").GetString("myStr", null), "second");
        }

        Session ReadySession() {
            Session session = GameObject.FindObjectOfType<Session>();
            if (session == null)
                return null;
            session.Clear();
            return session;
        }
    }
}
