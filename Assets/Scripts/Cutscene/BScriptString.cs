using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PropertyDrawer를 사용하기 위한 클래스
[System.Serializable]
public class BScriptString 
{
    public string code = "";
    public List<Token> tokens;
    public int tokenCount;
    public List<BSExceptionAsSerializedProperty> exceptions;
    public int exceptionCount; // SerializedProperty에서 리스트를 복구할 때 필요함
    public int linePointer = -1;
}

// Message 프로퍼티가 Serialize되지 않음
[System.Serializable]
public class BSExceptionAsSerializedProperty {
    public int line;
    public string message;

    BSExceptionAsSerializedProperty(int line, string message) {
        this.line = line;
        this.message = message;
    }

    public static implicit operator BSExceptionAsSerializedProperty(BSException exception) {
        return new BSExceptionAsSerializedProperty(exception.line, exception.Message);
    }
}
