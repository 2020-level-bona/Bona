using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬이 전환되어도 세션 데이터를 유지해줍니다.
[CreateAssetMenu(menuName = "SessionHolder")]
public class SessionHolder : ScriptableObject
{
    public Dictionary<string, object> table;
}
