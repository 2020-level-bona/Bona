﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    Player player;
    CameraController cameraController;

    bool showZIndexThreshold = false;
    bool showColliders = false;

    List<GameObject> zIndexThresholdObjects;

    public Material colliderDebugMaterial;

    List<GameObject> colliderDebugObjects;

    Text debugText;

    void Start()
    {
        player = FindObjectOfType<Player>();
        cameraController = FindObjectOfType<CameraController>();

        debugText = GetComponent<Text>();
        debugText.text += $"\n배경: {cameraController.backgroundSize.x}x{cameraController.backgroundSize.y}\n카메라: {cameraController.cameraSize.x}x{cameraController.cameraSize.y}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("AllScenes");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (showZIndexThreshold) {
                foreach (GameObject gameObject in zIndexThresholdObjects) {
                    Destroy(gameObject);
                }

                showZIndexThreshold = false;
            } else {
                zIndexThresholdObjects = new List<GameObject>();
                foreach (ZIndex zIndex in FindObjectsOfType<ZIndex>()) {
                    GameObject gameObject = new GameObject("Z Index Threshold");
                    gameObject.transform.position = new Vector3(0, 0, 0);

                    LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                    lineRenderer.startWidth = lineRenderer.endWidth = 0.03f;

                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, new Vector3(-100, zIndex.y - 100 * Mathf.Tan(zIndex.angle * Mathf.Deg2Rad), -9.5f));
                    lineRenderer.SetPosition(1, new Vector3(100, zIndex.y + 100 * Mathf.Tan(zIndex.angle * Mathf.Deg2Rad), -9.5f));

                    zIndexThresholdObjects.Add(gameObject);
                }

                showZIndexThreshold = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (showColliders) {
                foreach (GameObject gameObject in colliderDebugObjects) {
                    Destroy(gameObject);
                }

                showColliders = false;
            } else {
                colliderDebugObjects = new List<GameObject>();
                foreach (PolygonCollider2D floor in player.floorPolygons) {
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

        if (showColliders) {
            UpdateColliderDebugMaterialColors();
        }
    }

    void UpdateColliderDebugMaterialColors() {
        if (colliderDebugObjects == null)
            return;
        
        for(int i = 0; i < colliderDebugObjects.Count; i++) {
            MeshRenderer renderer = colliderDebugObjects[i].GetComponent<MeshRenderer>();
            Color color = player.currentFloor == (i + 1) ? Color.red : Color.green;
            color.a = 0.5f;
            renderer.sharedMaterial.color = color;
        }
    }
}