using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatRenderer : MonoBehaviour
{
    // 캐릭터의 머리 위에서 얼마만큼의 높이를 더한 지점에 대화를 띄울지 결정한다. 0인 경우 머리 바로 위에 대화를 띄운다.
    public const float CHATBOX_ADDITIONAL_HEIGHT = 0.3f;

    public Chat chat {get; private set;}
    public Movable movable {get; private set;}

    RectTransform rectTransform;
    Text uiText;
    Vector2 chatboxSize;
    ChatboxCoordinator chatboxCoordinator;

    public bool AnimationFinished {get; private set;} = false;

    Coroutine animationCoroutine;

    public void Initialize(Chat chat, Movable movable, ChatboxCoordinator chatboxCoordinator) {
        this.chat = chat;
        this.movable = movable;
        this.chatboxCoordinator = chatboxCoordinator;
    }

    void Awake() {
        rectTransform = GetComponent<RectTransform>();

        uiText = GetComponent<Text>();
        if (uiText == null)
            uiText = GetComponentInChildren<Text>();
    }

    void Start()
    {
        uiText.text = chat.Message;
        chatboxSize = new Vector2(200, uiText.preferredHeight);

        if (!chat.Global)
            EventManager.Instance.OnCharacterClicked += OnCharacterClicked;

        animationCoroutine = StartCoroutine(Animate());

        UpdatePosition();
    }

    void Update() {
        if (!movable)
            Finish();
        else if (chat.Global && Input.GetMouseButtonDown(0)) {
            if (!AnimationFinished)
                SkipAnimation();
            else
                Finish();
        }

        UpdatePosition();
    }

    void OnCharacterClicked(CharacterType type) {
        if (!AnimationFinished)
            SkipAnimation();
        else
            Finish();
    }

    void UpdatePosition() {
        rectTransform.position = chatboxCoordinator.CalculateRectPosition(GetAnchorPosition(), chatboxSize);
    }

    Vector2 GetAnchorPosition() {
        Bounds bounds = movable.GetBounds();
        return new Vector2((bounds.min.x + bounds.max.x) / 2f, bounds.max.y + CHATBOX_ADDITIONAL_HEIGHT);
    }

    IEnumerator Animate() {
        for (int i = 0; i < chat.Message.Length; i++) {
            uiText.text = $"<b>{chat.Message.Substring(0, i + 1)}<color=#00000000>{chat.Message.Substring(i + 1, chat.Message.Length - (i + 1))}</color></b>";
            yield return new WaitForSeconds(0.07f);
        }

        AnimationFinished = true;
    }

    public void SkipAnimation() {
        if (animationCoroutine != null) {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;

            uiText.text = $"<b>{chat.Message}</b>";

            AnimationFinished = true;
        }
    }

    public void Finish() {
        Destroy(gameObject);
        chat.Displaying = false;
    }

    void OnDestroy() {

    }
}
