using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLetterbox : MonoBehaviour
{
    public const float LETTERBOX_HEIGHT = 128f;

    public Image lower, upper;

    public float letterboxIntensity {
        set {
            value = Mathf.Clamp01(value);
            lower.rectTransform.sizeDelta = new Vector2(lower.rectTransform.sizeDelta.x, value * LETTERBOX_HEIGHT);
            upper.rectTransform.sizeDelta = new Vector2(upper.rectTransform.sizeDelta.x, value * LETTERBOX_HEIGHT);
        }
    }

    void Awake() {
        EventManager.Instance.OnCutsceneStart += ShowLetterbox;
        EventManager.Instance.OnCutsceneFinish += HideLetterbox;
    }

    public void ShowLetterbox() {
        Tween.Add(gameObject, x => letterboxIntensity = x, 0f, 1f, 0.7f);
    }

    public void HideLetterbox() {
        Tween.Add(gameObject, x => letterboxIntensity = x, 1f, 0f, 0.7f);
    }
}
