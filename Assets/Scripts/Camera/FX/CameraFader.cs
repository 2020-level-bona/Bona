using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
    Image blackImage;

    public float fadeIntensity {
        set {
            Color color = blackImage.color;
            color.a = Mathf.Clamp01(value);
            blackImage.color = color;
        }
    }

    void Awake() {
        blackImage = GetComponent<Image>();
    }

    // 어두워진 화면이 다시 돌아옴
    public ITweenEntry FadeIn(float duration = 1.5f) {
        return Tween.Add(gameObject, x => fadeIntensity = x, 1f, 0f, duration);
    }

    // 화면이 어두워짐
    public ITweenEntry FadeOut(float duration = 1.5f) {
        return Tween.Add(gameObject, x => fadeIntensity = x, 0f, 1f, duration);
    }
}
