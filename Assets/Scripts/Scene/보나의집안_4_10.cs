using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 보나의집안_4_10 : MonoBehaviour
{

    //Scene 4-10.
    // 요정 클릭하면 요정과 장난치는 이벤트 시작 -> 모르의 등장 및 대사 -> 모르 퇴장 -> 요정과 보나 놀람(!!띄우기)


    Game game;
    Level level;
    ChatQueue chatQueue;

    public Transform bonaStandingPoint;
    public Transform moreuComeInPoint;
    public Transform moreuStandingPoint;
    public Transform moreuOutPoint;
    public PathController waterFairyPath;


    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatQueue = FindObjectOfType<ChatQueue>();
    }


    void Start() {
        EventManager.Instance.OnCharacterClicked += OnCharacterClicked;
    } //요정 클릭 이벤트

    void OnCharacterClicked(CharacterType type) {
        if (type == CharacterType.WATERFAIRY) {
            game.StartCutscene(Cutscene4_10());
        }
    }

    IEnumerator Cutscene4_10() {
        Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
        WaterFairy waterFairy = level.GetSpawnedCharacter(CharacterType.WATERFAIRY) as WaterFairy;

        //tween으로 보나와 장난치는 요정 표현
        /* ITweenEntry tweenEntry = Tween.Add(waterFairy, waterFairyPath.GetPath(), 7f);
        yield return new WaitForTween(tweenEntry); */


        // 모르 문에서 등장
        Moreu moreu = level.SpawnCharacter(CharacterType.MOREU, moreuComeInPoint.position) as Moreu;

        // 모르를 보나 앞으로 움직임
        ITweenEntry tweenEntry2 = Tween.Add(moreu, moreuStandingPoint.position, 5f);
        yield return new WaitForTween(tweenEntry2);

        chatQueue.AddChat(new Chat("딱 걸렸어, 이 마녀!" , moreu));
        chatQueue.AddChat(new Chat("어쩐지 요즘 수상했어.", moreu));
        chatQueue.AddChat(new Chat("그 이상한 괴물로 우리를 다 죽일 셈이지?", moreu));
        chatQueue.AddChat(new Chat("전부 이를거야!", moreu));

        //모르를 다시 문앞으로
        ITweenEntry tweenEntry3 = Tween.Add(moreu, moreuOutPoint.position, 5f);

        //모르 퇴장
        moreu.Hide();


        //요정과 보나 놀란다(!!띄우기)

    }



}

