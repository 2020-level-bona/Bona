using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 숲 : MonoBehaviour
{
    Game game;
    Level level;

    public Transform bonaStandingPoint;
    public PathController fairyPath;

    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
    }

    void Start()
    {
        game.StartCutscene(ShowMoleAndFairy());

        EventManager.Instance.OnCharacterClicked += OnCharacterClicked;
    }

    void OnCharacterClicked(CharacterType type) {
        if (type == CharacterType.MOLE) {
            game.StartCutscene(KillMole());
        }
    }

    IEnumerator ShowMoleAndFairy() {
        Player player = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
        Mole mole = level.GetSpawnedCharacter(CharacterType.MOLE) as Mole;
        WaterFairy waterFairy = level.GetSpawnedCharacter(CharacterType.WATERFAIRY) as WaterFairy;

        CameraController cameraController = FindObjectOfType<CameraController>();
        FollowCharacters cameraOperator = cameraController.FindCameraOperator<FollowCharacters>();
        cameraOperator.AddTarget(mole);
        cameraOperator.AddTarget(waterFairy);
        cameraOperator.RemoveTarget(player);

        yield return new WaitForSeconds(3f);

        cameraOperator.AddTarget(player);
        cameraOperator.RemoveTarget(mole);
        cameraOperator.RemoveTarget(waterFairy);
    }

    IEnumerator KillMole() {
        Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
        Mole mole = level.GetSpawnedCharacter(CharacterType.MOLE) as Mole;
        WaterFairy waterFairy = level.GetSpawnedCharacter(CharacterType.WATERFAIRY) as WaterFairy;

        CameraController cameraController = FindObjectOfType<CameraController>();
        FollowCharacters cameraOperator = cameraController.FindCameraOperator<FollowCharacters>();
        cameraOperator.AddTarget(mole);

        ITweenEntry tween = Tween.Add(bona, bonaStandingPoint.position, 5f);
        yield return new WaitForTween(tween);

        bona.PlayAnimation(Player.STAB_WITH_FORK); // FIXME: Player측에서 IDLE 모션을 계속 재생하려고 하기 때문에 동작하지 않음!

        yield return new WaitForSeconds(0.5f);

        mole.PlayAnimation(Mole.DIE);
        yield return new WaitForSeconds(1f);
        mole.Hide();
        yield return new WaitForSeconds(1f);
        cameraOperator.RemoveTarget(mole);

        bona.PlayAnimation(Player.IDLE);

        tween = Tween.Add(waterFairy, fairyPath.GetPath(), 8f);
        yield return new WaitForTween(tween);

        waterFairy.Hide();

        yield return new WaitForSeconds(1f);
    }
}
