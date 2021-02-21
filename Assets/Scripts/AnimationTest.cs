using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    const float MAX_X = 20;
    const float X_STEP = 4f;
    const float Y_STEP = 5f;

    public GameObject template;

    public RuntimeAnimatorController[] runtimeAnimatorControllers;

    Camera cam;

    Vector2 startMousePosition;
    Vector3 startCameraPosition;

    Dictionary<GameObject, string> gameObjectAnimationNameDictionary;

    void Start() {
        cam = Camera.main;
        
        gameObjectAnimationNameDictionary = new Dictionary<GameObject, string>();

        float x = 0, y = 0;
        foreach (RuntimeAnimatorController runtimeAnimatorController in runtimeAnimatorControllers) {
            foreach (AnimationClip animationClip in runtimeAnimatorController.animationClips) {
                GameObject gameObject = Instantiate(template);
                gameObject.transform.position = new Vector3(x, y, 0);
                
                Animator animator = gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = runtimeAnimatorController;
                animator.Play(animationClip.name);

                TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
                textMesh.text = animationClip.name;

                gameObjectAnimationNameDictionary.Add(gameObject, animationClip.name);

                x += X_STEP;
                if (x >= MAX_X) {
                    x = 0;
                    y += Y_STEP;
                }
            }
        }
    }

    void Replay() {
        foreach (KeyValuePair<GameObject, string> pair in gameObjectAnimationNameDictionary) {
            Animator animator = pair.Key.GetComponent<Animator>();
            animator.Play(pair.Value, 0, 0f);
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            startMousePosition = Input.mousePosition;
            startCameraPosition = cam.transform.position;
        } else if (Input.GetMouseButton(0)) {
            cam.transform.position = cam.ScreenToWorldPoint(startMousePosition) - cam.ScreenToWorldPoint(Input.mousePosition) + startCameraPosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel") * 5f;
        cam.orthographicSize = Mathf.Max(cam.orthographicSize - scroll, 1f);

        if (Input.GetKeyDown(KeyCode.R)) {
            Replay();
        }
    }

}
