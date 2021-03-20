using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 저주받은숲 : MonoBehaviour
{
    /*#2

(놀란 표정)

숲에 들어서면 보나가 위험에 처한 물의 요정을 발견한다. (함정1, 물의 요정 5-8)

(용감한 표정)
‘포크’ 사용해서 두더지 찌르기

두더지 X-X (4-5)

물의 요정이 파닥거리며 보나 주위를 빙빙 돌다가 (1-4)
요정 마을로 안내한다.

>>요정 마을 해금

 */
    Game game;
    Level level;
    ChatQueue chatQueue;

    public Transform bonaStandingPoint;

    public Transform DigDogStandingPoint;

    public Transform waterFairyStandingPoint;

    public PathController waterFairyPath;

    void Awake() 
    {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatQueue = FindObjectOfType<ChatQueue>();
    }

     IEnumerator CutScene2() {
        Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
        WaterFairy waterFairy = level.GetSpawnedCharacter(CharacterType.WATERFAIRY) as WaterFairy;

        // 놀란 표정
        // 숲에 들어서면 보나가 위험에 처한 물의 요정을 발견한다.
        ITweenEntry tweenEntry = Tween.Add(bona, bonaStandingPoint.position, 5f);
        yield return new WaitForTween(tweenEntry);
    }


}
