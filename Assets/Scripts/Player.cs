using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public new const CharacterType type = CharacterType.BONA;

    public Vector2 velocityScale = new Vector2(5f, 5f);

    AnimatorController animatorController;

    static readonly AnimatorState PUT = new AnimatorState("가방에 넣기");
    static readonly AnimatorState WALK_D = new AnimatorState("걷기(아래)");
    static readonly AnimatorState WALK_U = new AnimatorState("걷기(위)");
    static readonly AnimatorState WALK_L = new AnimatorState("걷기(좌)", false);
    static readonly AnimatorState WALK_R = new AnimatorState("걷기(좌)", true); // FLIP
    static readonly AnimatorState IDLE = new AnimatorState("기본(좌)");
    static readonly AnimatorState SURPRISE = new AnimatorState("놀란 표정");
    static readonly AnimatorState SURPRISE_TALK = new AnimatorState("놀란 표정+말");
    static readonly AnimatorState THROW = new AnimatorState("던지기");
    static readonly AnimatorState TALK = new AnimatorState("말");
    static readonly AnimatorState HUNGRY = new AnimatorState("배고픔");
    static readonly AnimatorState GET_BREAD = new AnimatorState("빵 받기");
    static readonly AnimatorState DEPRESS = new AnimatorState("시무룩한 표정");
    static readonly AnimatorState DEPRESS_TALK = new AnimatorState("시무룩한 표정+말");
    static readonly AnimatorState HAPPY = new AnimatorState("웃는표정");
    static readonly AnimatorState HAPPY_TALK = new AnimatorState("웃는표정+말");
    static readonly AnimatorState NO = new AnimatorState("절레절레");
    static readonly AnimatorState COLLECT = new AnimatorState("채집");
    static readonly AnimatorState ANGRY = new AnimatorState("화난 표정");
    static readonly AnimatorState ANGRY_TALK = new AnimatorState("화난 표정+말");

    public Inventory inventory;

    Game game;

    protected override void Awake() {
        base.Awake();
        
        animatorController = GetComponentInChildren<AnimatorController>();

        game = FindObjectOfType<Game>();

        inventory = new Inventory();

        transform.position = game.GetPlayerSpawnPoint(transform.position);
    }

    void Start() {

    }

    protected override void Update() {
        base.Update();

        Vector3 current = transform.position;

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal") * velocityScale.x, Input.GetAxis("Vertical") * velocityScale.y, 0);
        if (velocity.magnitude == 0)
            return;
        
        Vector3 delta = velocity * Time.deltaTime;
        MoveDelta(delta);

        UpdateAnimation();
    }

    void UpdateAnimation() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Mathf.Abs(x) <= 0.1f && Mathf.Abs(y) <= 0.1f) {
            animatorController.Play(IDLE);
        } else if (Mathf.Abs(x) >= Mathf.Abs(y)) {
            if (x > 0) {
                animatorController.Play(WALK_R);
            } else {
                animatorController.Play(WALK_L);
            }
        } else {
            if (y > 0) {
                animatorController.Play(WALK_U);
            } else {
                animatorController.Play(WALK_D);
            }
        }
    }
}