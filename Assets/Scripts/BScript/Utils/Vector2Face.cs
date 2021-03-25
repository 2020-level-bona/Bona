using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector2Face
{
    public Vector2 position;
    public Face face = Face.UNKNOWN;

    public Vector2Face(Vector2 position, Face face = Face.UNKNOWN) {
        this.position = position;
        this.face = face;
    }

    public static implicit operator Vector2Face(Vector2 vector2) => new Vector2Face(vector2);
    public static implicit operator Vector2(Vector2Face vector2Face) => vector2Face.position;
}
