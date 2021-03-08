using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTest : MonoBehaviour
{
    ChatQueue chatQueue;

    void Start() {
        chatQueue = FindObjectOfType<ChatQueue>();

        /*chatQueue.AddChat(new Chat("안녕, 세계!", transform));
        chatQueue.AddChat(new Chat("오늘도 좋은 하루!", transform));
        chatQueue.AddChat(new Chat("긴 문장을 말할 때에도 글자가 불안정하게 다음줄로 넘어가지 않아요. 이렇게띄어쓰기가없어도그럭저럭잘출력해줄겁니다아마도요.", transform));
        chatQueue.AddChat(new Chat("솔직히 정말 맘에 안들긴 하네요.", transform));*/
    }
}
