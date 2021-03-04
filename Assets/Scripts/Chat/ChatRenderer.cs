using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatRenderer : MonoBehaviour
{
    public Chat chat {get; private set;}

    RectTransform rectTransform;
    Text uiText;
    Vector2 chatboxSize;
    ChatboxCoordinator chatboxCoordinator;

    public bool AnimationFinished {get; private set;} = false;

    Coroutine animationCoroutine;

    public void Initialize(Chat chat, ChatboxCoordinator chatboxCoordinator) {
        this.chat = chat;
        this.chatboxCoordinator = chatboxCoordinator;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        uiText = GetComponent<Text>();
        if (uiText == null)
            uiText = GetComponentInChildren<Text>();
        
        uiText.text = chat.Message;
        chatboxSize = new Vector2(200, uiText.preferredHeight);

        animationCoroutine = StartCoroutine(Animate());

        UpdatePosition();
    }

    void Update() {
        UpdatePosition();
    }

    void UpdatePosition() {
        rectTransform.position = chatboxCoordinator.CalculateRectPosition(chat.Anchor.position, chatboxSize);
    }

    IEnumerator Animate() {
        for (int i = 0; i < chat.Message.Length; i++) {
            uiText.text = $"<b>{chat.Message.Substring(0, i + 1)}<color=#00000000>{chat.Message.Substring(i + 1, chat.Message.Length - (i + 1))}</color></b>";
            yield return new WaitForSeconds(0.07f);
        }

        AnimationFinished = true;
    }

    public void SkipAnimation() {
        StopCoroutine(animationCoroutine);

        uiText.text = $"<b>{chat.Message}</b>";

        AnimationFinished = true;
    }

    public void Finish() {
        Destroy(gameObject);
    }
}
