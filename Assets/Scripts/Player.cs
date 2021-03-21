﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public override CharacterType type {
        get => CharacterType.BONA;
    }

    public Vector2 velocityScale = new Vector2(5f, 5f);

    public static readonly AnimatorState PUT = new AnimatorState("가방에 넣기");
    public static readonly AnimatorState WALK_D = new AnimatorState("걷기(아래)");
    public static readonly AnimatorState WALK_U = new AnimatorState("걷기(위)");
    public static readonly AnimatorState WALK_L = new AnimatorState("걷기(좌)", false);
    public static readonly AnimatorState WALK_R = new AnimatorState("걷기(좌)", true); // FLIP
    public static readonly AnimatorState IDLE = new AnimatorState("기본(좌)");
    public static readonly AnimatorState WALK_BERRIES_D = new AnimatorState("걷기(아래) + 베리 바구니");
    public static readonly AnimatorState WALK_BERRIES_U = new AnimatorState("걷기(위) + 베리 바구니");
    public static readonly AnimatorState WALK_BERRIES_L = new AnimatorState("걷기(좌) + 베리 바구니", false);
    public static readonly AnimatorState WALK_BERRIES_R = new AnimatorState("걷기(좌) + 베리 바구니", true); // FLIP
    public static readonly AnimatorState BERRIES_IDLE = new AnimatorState("스탠드 + 베리 바구니");
    public static readonly AnimatorState SURPRISE = new AnimatorState("놀란 표정");
    public static readonly AnimatorState SURPRISE_TALK = new AnimatorState("놀란 표정+말");
    public static readonly AnimatorState THROW = new AnimatorState("던지기");
    public static readonly AnimatorState TALK = new AnimatorState("말");
    public static readonly AnimatorState HUNGRY = new AnimatorState("배고픔");
    public static readonly AnimatorState GET_BREAD = new AnimatorState("빵 받기");
    public static readonly AnimatorState DEPRESS = new AnimatorState("시무룩한 표정");
    public static readonly AnimatorState DEPRESS_TALK = new AnimatorState("시무룩한 표정+말");
    public static readonly AnimatorState HAPPY = new AnimatorState("웃는표정");
    public static readonly AnimatorState HAPPY_TALK = new AnimatorState("웃는표정+말");
    public static readonly AnimatorState NO = new AnimatorState("절레절레");
    public static readonly AnimatorState COLLECT = new AnimatorState("채집");
    public static readonly AnimatorState ANGRY = new AnimatorState("화난 표정");
    public static readonly AnimatorState ANGRY_TALK = new AnimatorState("화난 표정+말");
    public static readonly AnimatorState STAB_WITH_FORK = new AnimatorState("포크 들고 찌르기");
    public static readonly AnimatorState PLAY_HAPPY = new AnimatorState("즐겁게 논다");

    public Inventory inventory;

    public bool hasBerries = false;

    Game game;

    protected override void Awake() {
        base.Awake();

        game = FindObjectOfType<Game>();

        inventory = new Inventory();

        transform.position = game.GetPlayerSpawnPoint(transform.position);
    }

    protected override void Update() {
        base.Update();

        Vector3 current = transform.position;

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal") * velocityScale.x, Input.GetAxis("Vertical") * velocityScale.y, 0);
        if (game.IsPlayingCutscene)
            velocity = Vector2.zero;
        Vector3 delta = velocity * Time.deltaTime;
        MoveDelta(delta);

        UpdateAnimation();
    }

    void UpdateAnimation() {
        float x = velocity.x;
        float y = velocity.y;

        if (Mathf.Abs(x) <= 0.1f && Mathf.Abs(y) <= 0.1f) {
            PlayAnimation(hasBerries ? BERRIES_IDLE : IDLE);
        } else if (Mathf.Abs(x) >= Mathf.Abs(y)) {
            if (x > 0) {
                PlayAnimation(hasBerries ? WALK_BERRIES_R : WALK_R); // @Temporary 더 나은 구현
            } else {
                PlayAnimation(hasBerries ? WALK_BERRIES_L : WALK_L);
            }
        } else {
            if (y > 0) {
                PlayAnimation(hasBerries ? WALK_BERRIES_U : WALK_U);
            } else {
                PlayAnimation(hasBerries ? WALK_BERRIES_D : WALK_D);
            }
        }
    }
}