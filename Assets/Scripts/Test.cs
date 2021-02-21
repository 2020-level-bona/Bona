using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            PolygonCollider2D collider2D = FindObjectOfType<PolygonCollider2D>();
            Mesh mesh = collider2D.CreateMesh(false, false);

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        }
    }
}
