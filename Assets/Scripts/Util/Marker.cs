using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Marker : MonoBehaviour
{
    public Face face = Face.UNKNOWN;
    public Vector2 position => transform.position;

    public static implicit operator Vector2Face(Marker marker) => new Vector2Face(marker.position, marker.face);
}
