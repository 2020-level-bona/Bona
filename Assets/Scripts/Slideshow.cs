using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    public string slideshowName;
    public Sprite[] images;
    public float timePerImage = 5f;
    Image[] imageInstances;

    void Awake() {
        imageInstances = new Image[images.Length];
        for (int i = images.Length - 1; i >= 0; i--) {
            GameObject instance = new GameObject(i.ToString());
            instance.transform.parent = transform;
            RectTransform rectTransform = instance.AddComponent<RectTransform>();
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
            imageInstances[i] = instance.AddComponent<Image>();
            imageInstances[i].sprite = images[i];
        }
    }

    void Start() {
        StartCoroutine(Show());
    }

    IEnumerator Show() {
        CameraFader cameraFader = FindObjectOfType<CameraFader>();
        ITweenEntry entry = cameraFader.FadeIn();
        yield return new WaitUntil(() => entry.HasDone());
        for (int i = 0; i < images.Length; i++) {
            yield return new WaitForSeconds(timePerImage);
            if (i < images.Length - 1) {
                entry = Tween.Add(gameObject, x => {
                    imageInstances[i].color = new Color(1, 1, 1, x);
                }, 1f, 0f, 1f);
                yield return new WaitUntil(() => entry.HasDone());
            }
        }
        entry = cameraFader.FadeOut();
        yield return new WaitUntil(() => entry.HasDone());
        Destroy(gameObject);
    }
}
