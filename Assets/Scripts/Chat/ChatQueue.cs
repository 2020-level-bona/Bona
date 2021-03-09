using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatQueue : MonoBehaviour
{
    public GameObject chatRendererPrefab;
    public GameObject canvasObject;
    
    List<Chat> queue = new List<Chat>();

    ChatRenderer currentRenderer;

    ChatboxCoordinator chatboxCoordinator;

    public bool IsDisplaying {
        get {
            return currentRenderer != null;
        }
    }

    public bool Skipable = true;

    void Awake() {
        chatboxCoordinator = new ChatboxCoordinator(canvasObject.GetComponent<RectTransform>(), Camera.main);
    }

    void Update()
    {
        if (currentRenderer != null && Input.GetMouseButtonDown(0) && Skipable) {
            if (currentRenderer.AnimationFinished)
                Next();
            else
                currentRenderer.SkipAnimation();
        }
    }

    public void Next() {
        if (currentRenderer != null) {
            currentRenderer.Finish();
            currentRenderer = null;
        }

        if (queue.Count > 0) {
            GameObject gameObject = Instantiate(chatRendererPrefab);
            gameObject.transform.SetParent(canvasObject.transform);
            currentRenderer = gameObject.GetComponent<ChatRenderer>();
            currentRenderer.Initialize(queue[0], chatboxCoordinator);

            queue.RemoveAt(0);
        }
    }

    public void AddChat(Chat chat) {
        queue.Add(chat);

        if (currentRenderer == null)
            Next();
    }
}
