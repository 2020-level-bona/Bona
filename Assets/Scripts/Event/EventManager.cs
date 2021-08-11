using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // 플레이어가 움직였을 때 발생하는 이벤트
    public Action<Vector2> OnPlayerMove;

    // 캐릭터가 클릭되었을 때 발생하는 이벤트
    public Action<CharacterType> OnCharacterClicked;
    // 저장이 시작되기 전에 발생하는 이벤트
    public Action OnPreSave;
    // 컷씬이 시작되었을 때 발생하는 이벤트
    public Action OnCutsceneStart;
    // 컷씬이 종료되었을 때 발생하는 이벤트
    public Action OnCutsceneFinish;
}
