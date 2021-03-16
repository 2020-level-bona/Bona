using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTest : MonoBehaviour
{
    Game game;
    Level level;
    ChatQueue chatQueue;

    public Transform bonaStandingPoint;
    public Transform moleSpawnPoint;
    public PathController waterFairyPath;

    void Awake() {
        game = FindObjectOfType<Game>();
        level = FindObjectOfType<Level>();
        chatQueue = FindObjectOfType<ChatQueue>();
    }

    void Start() {
        EventManager.Instance.OnCharacterClicked += OnCharacterClicked;
    }

    void OnCharacterClicked(CharacterType type) {
        if (type == CharacterType.PRIEST) {
            if (Session.CurrentScene.GetBool("talkedToPriest")) {
                Priest priest = level.GetSpawnedCharacter(CharacterType.PRIEST) as Priest;
                chatQueue.AddChat(new Chat("음? 이미 내가 다 이야기하지 않았니?", priest));
            }
            else
                game.StartCutscene(Cutscene1());
        }
    }

    IEnumerator Cutscene1() {
        Player bona = level.GetSpawnedCharacter(CharacterType.BONA) as Player;
        Priest priest = level.GetSpawnedCharacter(CharacterType.PRIEST) as Priest;

        CameraController cameraController = FindObjectOfType<CameraController>();
        FollowCharacters cameraOperator = cameraController.FindCameraOperator<FollowCharacters>();
        // 사제를 카메라에 잡아준다.
        cameraOperator.AddTarget(priest);

        // 보나가 서있어야 할 장소로 옮긴다.
        ITweenEntry tweenEntry = Tween.Add(bona, bonaStandingPoint.position, 5f);

        // 보나가 bonaStandingPoint에 도달할 때까지 대기한다.
        yield return new WaitForTween(tweenEntry);

        chatQueue.AddChat(new Chat("안녕...", priest));
        chatQueue.AddChat(new Chat("안녕하세요!", bona));
        chatQueue.AddChat(new Chat("여긴 무슨 일이니?", priest));
        chatQueue.AddChat(new Chat("집으로 가는 길을 잃은 것 같아요.", bona));
        chatQueue.AddChat(new Chat("그런데 여긴 처음 보는 곳인데, 기분이 이상하네요.", bona));
        // 위에서 추가한 대화들이 모두 넘어갈 때까지 대기한다.
        yield return new WaitForSkippingChat();

        // 대화를 스킵 불가능하게 만든다. 이 경우에는 반드시 chatQueue의 Next 메서드를 수동으로 호출해야 한다.
        chatQueue.Skipable = false;

        // 아래 3줄은 해당 대화를 5초 뒤에 자동으로 넘어가도록 만든다.
        chatQueue.AddChat(new Chat("보나야. 내가 지금부터 하는 이야기를 잘 듣거라.", priest));
        yield return new WaitForSeconds(5f);
        chatQueue.Next();

        chatQueue.AddChat(new Chat("이건 정말 중요한 이야기라 반드시 집중해야 해.", priest));
        yield return new WaitForSeconds(5f);
        chatQueue.Next();

        chatQueue.AddChat(new Chat("이제 곧 저 위에서 두더지 한 마리가 나타날거야.", priest));
        yield return new WaitForSeconds(3f);

        // 두더지가 씬에 없으므로 스폰시킨다.
        Mole mole = level.SpawnCharacter(CharacterType.MOLE, moleSpawnPoint.position) as Mole;
        mole.Hide(0f, false); // 기본이 보여지는 상태이므로 즉시(duration=0f) 숨긴다.

        // 두더지만 화면에 잡히도록 만든다.
        cameraOperator.ClearTargets();
        cameraOperator.AddTarget(mole);

        yield return new WaitForSeconds(2f);
        
        chatQueue.Next();

        mole.Show();

        yield return new WaitForSeconds(1f);

        chatQueue.AddChat(new Chat("그리고 몇 초 뒤에 죽는다는거야.", priest));

        yield return new WaitForSeconds(2f);

        chatQueue.Next();
        // 두더지 애니메이션을 재생한다.
        mole.PlayAnimation(Mole.DIE);

        yield return new WaitForSeconds(1f);

        // 두더지를 디스폰한다.
        mole.Hide();

        yield return new WaitForSeconds(1f);

        // 두더지는 삭제되었으므로 자동으로 카메라에서 제외된다.
        // 다시 나머지 둘을 카메라에 잡아준다.
        cameraOperator.AddTarget(bona);
        cameraOperator.AddTarget(priest);

        chatQueue.Skipable = true;
        chatQueue.AddChat(new Chat("나도 이유를 모르겠지만 매일 이런 일이 일어나더라고.", priest));
        chatQueue.AddChat(new Chat("이상하네요...", bona));
        yield return new WaitForSkippingChat();

        WaterFairy waterFairy = level.SpawnCharacter(CharacterType.WATERFAIRY, Vector2.zero) as WaterFairy;
        waterFairy.Show();

        ITweenEntry tweenEntry2 = Tween.Add(waterFairy, waterFairyPath.GetPath(), 7f);
        yield return new WaitForTween(tweenEntry2);

        waterFairy.Hide();

        chatQueue.AddChat(new Chat("방금 요정이 날아가는걸 봤니?", priest));
        chatQueue.AddChat(new Chat("네, 봤어요.", bona));
        chatQueue.AddChat(new Chat("흠... 잘됐군. 그럼 시간이 된 것 같으니...", priest));
        chatQueue.AddChat(new Chat("난 가보도록 할게.", priest));
        yield return new WaitForSkippingChat();

        // priest.Hide();

        // yield return new WaitForSeconds(1f);
        Session.CurrentScene.Set("talkedToPriest", true);
    }
}
