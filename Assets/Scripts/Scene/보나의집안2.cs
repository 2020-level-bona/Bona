/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

ublic class 보나의집안2 : MonoBehaviour
{
    ChatQueue chatQueue;
    Player player;

    void Start()
    {
        
       chatQueue = FindObjectOfType<ChatQueue>();
        player = FindObjectOfType<Player>();

        chatQueue.AddChat(new Chat("저게 뭐지?", player.transform));
         chatQueue.AddChat(new Chat("믿을 수 없어!", player.transform));

    }


} */