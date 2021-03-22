using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Marker : MonoBehaviour
{
    public Face face;
    public Vector2 position => transform.position;

    public void OnDrawGizmos() {
        Handles.color = Color.red;

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 16;

        Handles.Label(transform.position + new Vector3(0f, 0.7f, 0f), name, style);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
