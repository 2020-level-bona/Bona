using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    static EventManager instance;

    public static EventManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<EventManager>();
            }
            return instance;
        }
    }

    // 캐릭터가 클릭되었을 때 발생하는 이벤트
    public Action<CharacterType> OnCharacterClicked;
    // 저장이 시작되기 전에 발생하는 이벤트
    public Action OnPreSave;
}
