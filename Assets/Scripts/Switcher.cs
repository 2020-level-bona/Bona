using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Switcher : MonoBehaviour
{
    [SerializeField]
    PageConfig[] pageConfigs;

    Level level;
    Movable movable;

    int index = -1;

    void OnValidate() {
        if (pageConfigs == null)
            return;
        
        // Play Mode가 아닐 경우 선택됨 표시를 없앤다.
        if (!EditorApplication.isPlaying) {
            foreach (PageConfig pageConfig in pageConfigs)
                pageConfig.selected = false;
        }
    }

    void Awake() {
        level = FindObjectOfType<Level>();
        movable = GetComponent<Movable>();
    }

    void Start() {
        Trigger trigger = GetComponent<Trigger>();
        if (trigger)
            trigger.AddListener(OnTriggered);
        SwitchPage(CalcIndex());
    }

    void Update() {
        int newIndex = CalcIndex();
        if (index != newIndex)
            SwitchPage(newIndex);
        
        if (index >= 0 && pageConfigs[index].autoExecutor)
            pageConfigs[index].autoExecutor.Run();
    }

    int CalcIndex() {
        if (pageConfigs == null)
            return -1;

        for (int i = 0; i < pageConfigs.Length; i++) {
            if (pageConfigs[i].condition == null || pageConfigs[i].condition.Length == 0
                || Expression.CastAsBool(Expression.Eval(pageConfigs[i].condition))) {
                return i;
            }
        }
        return -1;
    }

    void SwitchPage(int index) {
        this.index = index;
        UpdateSelectedMarker();

        if (index < 0)
            return;
        
        SetHide(pageConfigs[index].hide);
        SetPosition(pageConfigs[index].position);
        SetSprite(pageConfigs[index].sprite);
    }

    void OnTriggered() {
        if (index < 0)
            return;
        
        if (pageConfigs[index].triggerExecutor)
            pageConfigs[index].triggerExecutor.Run();
    }

    void UpdateSelectedMarker() {
        if (pageConfigs == null)
            return;

        for (int i = 0; i < pageConfigs.Length; i++) {
            pageConfigs[i].selected = (i == index);
        }
    }

    void SetHide(bool hide) {
        foreach (SpriteRenderer spriteRenderer in GetComponents<SpriteRenderer>())
            spriteRenderer.enabled = !hide;
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            spriteRenderer.enabled = !hide;
        foreach (Trigger trigger in GetComponents<Trigger>())
            trigger.enabled = !hide;
        foreach (Trigger trigger in GetComponentsInChildren<Trigger>())
            trigger.enabled = !hide;
    }

    void SetPosition(string position) {
        // TODO: CommandLineParser.GetVector2Face?
        if (position == null || position.Length == 0)
            return;
        
        if (movable == null) {
            Debug.LogError("Movable 컴포넌트를 찾을 수 없지만 Switcher에서 위치를 변경하려고 시도했습니다.");
            return;
        }
        
        if (position.Contains(",")) {
            try {
                string[] spl = position.Split(',');
                float x = float.Parse(spl[0]);
                float y = float.Parse(spl[1]);
                movable.MoveTo(new Vector2(x, y));
            } catch {
                Debug.LogError("올바른 Vector2 표현식이 아닙니다.");
            }
        } else {
            Marker marker = level.GetMarker(position);
            if (marker == null)
                Debug.LogError($"마커[name={position}]를 찾을 수 없습니다.");
            else
                movable.MoveTo(marker.position);
        }
    }

    void SetSprite(Sprite sprite) {
        if (sprite == null)
            return;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        if (!spriteRenderer) {
            Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없지만 Switcher에서 스프라이트를 변경하려고 시도했습니다.");
            return;
        }

        spriteRenderer.sprite = sprite;
    }
}
