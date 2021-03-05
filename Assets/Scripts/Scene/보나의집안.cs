﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 보나의집안 : MonoBehaviour
{
    ChatQueue chatQueue;
    Player player;

    void Start()
    {
        chatQueue = FindObjectOfType<ChatQueue>();
        player = FindObjectOfType<Player>();

        chatQueue.AddChat(new Chat("좋은 아침! 오늘도 고달픈 하루가 시작되는구나.", player.transform));
        chatQueue.AddChat(new Chat("배고파...", player.transform));
    }
}
