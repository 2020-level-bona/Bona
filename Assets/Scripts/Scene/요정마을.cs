using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 요정마을 : MonoBehaviour
{
    public GameObject fairy1, fairy2, fairy3, fairy4, fairy5;
    public Transform fairy1Target, fairy2Target, fairy3Target, fairy4Target, fairy5Target;

    public GameObject berryFairy;
    public Transform berryFairyTarget;

    public GameObject droppedBerry;

    public Transform bonaStanding;
    public Transform bonaExitTarget;
    public Transform waterFairyExitTarget;

    Game game;
    Level level;

    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
    }

    void Start() {
        berryFairy.SetActive(false);
        droppedBerry.SetActive(false);

        game.StartCutscene(PlayWithFairy());
    }

    IEnumerator PlayWithFairy() {
        Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
        WaterFairy waterFairy = level.GetSpawnedCharacter(CharacterType.WATERFAIRY) as WaterFairy;

        ITweenEntry tween = Tween.Add(bona, bonaStanding.position, 3f);
        yield return new WaitForTween(tween);

        Tween.Add(fairy1, x => fairy1.transform.position = x, fairy1.transform.position, fairy1Target.transform.position, 3f);
        Tween.Add(fairy2, x => fairy2.transform.position = x, fairy2.transform.position, fairy2Target.transform.position, 3f);
        Tween.Add(fairy3, x => fairy3.transform.position = x, fairy3.transform.position, fairy3Target.transform.position, 3f);
        Tween.Add(fairy4, x => fairy1.transform.position = x, fairy4.transform.position, fairy4Target.transform.position, 3f);
        tween = Tween.Add(fairy5, x => fairy5.transform.position = x, fairy5.transform.position, fairy5Target.transform.position, 3f);
        yield return new WaitForTween(tween);

        bona.PlayAnimation(Player.PLAY_HAPPY);

        yield return new WaitForSeconds(2f);

        CameraFader fader = FindObjectOfType<CameraFader>();
        fader.FadeOut();

        yield return new WaitForSeconds(3f);

        fairy1.SetActive(false);
        fairy2.SetActive(false);
        fairy3.SetActive(false);
        fairy4.SetActive(false);
        fairy5.SetActive(false);

        berryFairy.SetActive(true);
        tween = Tween.Add(berryFairy, x => berryFairy.transform.position = x, berryFairy.transform.position, berryFairyTarget.transform.position, 5f);

        fader.FadeIn();

        yield return new WaitForTween(tween);

        bona.hasBerries = true;

        tween = Tween.Add(bona, bonaExitTarget.transform.position, 2f);

        yield return new WaitForSeconds(3f);
        droppedBerry.SetActive(true);

        ITweenEntry fairyGetBerry = Tween.Add(waterFairy, droppedBerry.transform.position, 1.5f);
        yield return new WaitForTween(fairyGetBerry);
        droppedBerry.SetActive(false);

        ITweenEntry fairyExit = Tween.Add(waterFairy, waterFairyExitTarget.position, 3f);

        yield return new WaitForTween(fairyExit);
    }
}
