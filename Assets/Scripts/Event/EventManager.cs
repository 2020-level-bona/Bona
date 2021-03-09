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

    public Action<CharacterType> OnCharacterClicked;
}
