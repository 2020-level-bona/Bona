using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    Level level;
    Movable playerMovable;
    CameraController cameraController;

    bool showZIndexThreshold = false;
    bool showColliders = false;

    List<GameObject> zIndexThresholdObjects;

    public Material colliderDebugMaterial;

    List<GameObject> colliderDebugObjects;

    Text debugText;
    string defaultDebugInfoString;

    public string currentSceneName;

    void Awake() {
        level = FindObjectOfType<Level>();
        cameraController = FindObjectOfType<CameraController>();

        debugText = GetComponent<Text>();
    }

    void Start()
    {
        playerMovable = level.GetSpawnedCharacter(CharacterType.BONA).GetComponent<Movable>();

        if (cameraController != null)
            debugText.text += $"\n배경: {cameraController.backgroundSize.x}x{cameraController.backgroundSize.y}\n카메라: {cameraController.cameraSize.x}x{cameraController.cameraSize.y}";

        defaultDebugInfoString = debugText.text;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) {
            SceneManager.LoadScene("AllScenes");
        }
        if (Input.GetKeyDown(KeyCode.F5)) {
            // if (showZIndexThreshold) {
            //     foreach (GameObject gameObject in zIndexThresholdObjects) {
            //         Destroy(gameObject);
            //     }

            //     showZIndexThreshold = false;
            // } else {
            //     zIndexThresholdObjects = new List<GameObject>();
            //     foreach (ZIndex zIndex in FindObjectsOfType<ZIndex>()) {
            //         GameObject gameObject = new GameObject("Z Index Threshold");
            //         gameObject.transform.position = new Vector3(0, 0, 0);

            //         LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            //         lineRenderer.startWidth = lineRenderer.endWidth = 0.03f;

            //         lineRenderer.positionCount = 2;
            //         lineRenderer.SetPosition(0, new Vector3(-100, zIndex.y - 100 * Mathf.Tan(zIndex.angle * Mathf.Deg2Rad), -9.5f));
            //         lineRenderer.SetPosition(1, new Vector3(100, zIndex.y + 100 * Mathf.Tan(zIndex.angle * Mathf.Deg2Rad), -9.5f));

            //         zIndexThresholdObjects.Add(gameObject);
            //     }

            //     showZIndexThreshold = true;
            // }
        }
        if (Input.GetKeyDown(KeyCode.F6)) {
            if (showColliders) {
                foreach (GameObject gameObject in colliderDebugObjects) {
                    Destroy(gameObject);
                }

                showColliders = false;
            } else {
                if (playerMovable) {
                    colliderDebugObjects = new List<GameObject>();
                    foreach (PolygonCollider2D floor in level.floorPolygons) {
                        GameObject gameObject = new GameObject("Floor Collider Mesh");
                        gameObject.transform.position = new Vector3(0, 0, -9);
                        
                        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
                        renderer.sharedMaterial = Instantiate(colliderDebugMaterial);

                        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
                        meshFilter.mesh = floor.CreateMesh(false, false);

                        colliderDebugObjects.Add(gameObject);
                    }

                    showColliders = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F9)) {
            Session.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.F10)) {
            Session.Instance.Load();
        }
        if (Input.GetKeyDown(KeyCode.F11)) {
            Session.Instance.Clear();
        }

        if (showColliders) {
            UpdateColliderDebugMaterialColors();
        }

        debugText.text = defaultDebugInfoString + $"\n{(int) (1f / Time.unscaledDeltaTime)} FPS";
    }

    void UpdateColliderDebugMaterialColors() {
        if (colliderDebugObjects == null)
            return;
        
        for(int i = 0; i < colliderDebugObjects.Count; i++) {
            MeshRenderer renderer = colliderDebugObjects[i].GetComponent<MeshRenderer>();
            Color color = playerMovable.currentFloor == (i + 1) ? Color.red : Color.green;
            color.a = 0.5f;
            renderer.sharedMaterial.color = color;
        }
    }
}
