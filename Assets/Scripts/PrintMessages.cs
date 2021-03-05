using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public class PrintMessages : MonoBehaviour
{
    public string[] messages;

    Player player;
    ChatQueue chatQueue;
    Trigger trigger;

    void Awake() {
        player = FindObjectOfType<Player>();
        chatQueue = FindObjectOfType<ChatQueue>();

        trigger = GetComponent<Trigger>();
    }

    void Start() {
        trigger.AddListener(Chat);
    }

    void Chat() {
        if (!chatQueue.IsDisplaying) {
            foreach (string message in messages) {
                chatQueue.AddChat(new Chat(message, player.transform));
            }
        }
    }
}
