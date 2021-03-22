using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField] GameObject chatRendererPrefab;
    [SerializeField] GameObject canvasObject;

    ChatboxCoordinator chatboxCoordinator;

    void Awake() {
        chatboxCoordinator = new ChatboxCoordinator(canvasObject.GetComponent<RectTransform>(), Camera.main);
    }

    public ChatRenderer Render(Chat chat, Character character, bool global = true) {
        GameObject gameObject = Instantiate(chatRendererPrefab);
        gameObject.transform.SetParent(canvasObject.transform);

        ChatRenderer chatRenderer = gameObject.GetComponent<ChatRenderer>();
        chatRenderer.Initialize(chat, character, chatboxCoordinator);

        return chatRenderer;
    }
}
