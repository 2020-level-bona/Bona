using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 보나의집밖_3_9 : MonoBehaviour
{

    //Scene 3-9.
    // 보나 요정 클릭 -> (입모양, 말풍선)으로 집 초대

    Game game;
    Level level;
    ChatQueue chatQueue;

    public Transform bonaStandingPoint;

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
            if (Session.CurrentScene.GetBool("talkedToWaterFairy")) {
                Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
                chatQueue.AddChat(new Chat("뭐해? 따라와!", bona));
            }
            else
                game.StartCutscene(Cutscene3_9());
        }
    }


        IEnumerator Cutscene3_9() {
            //보나 이동
            Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
            ITweenEntry tweenEntry = Tween.Add(bona, bonaStandingPoint.position, 5f);
            yield return new WaitForTween(tweenEntry);

            //집으로 초대하는 대화
            chatQueue.AddChat(new Chat("우리집으로 놀러올래?" , bona));
            yield return new WaitForSkippingChat();


             Session.CurrentScene.Set("talkedToWaterFairy", true); 
        }

}
