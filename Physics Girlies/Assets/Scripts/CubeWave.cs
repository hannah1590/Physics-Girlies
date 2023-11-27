using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWave : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private MeshFilter meshFilter;
    private Bounds bounds;

    private Vector3[] oldVertices;
    private Vector3[] newVertices;

    private void Awake()
    {
        oldVertices = meshFilter.mesh.vertices;
        bounds = meshFilter.mesh.bounds;
    }

    private void Update()
    {
        newVertices = new Vector3[oldVertices.Length];
        
        for (int i = 0; i < oldVertices.Length; i++)
        {
            if (bounds.max.y <= oldVertices[i].y)
            {
                Vector3 vertice = oldVertices[i];
                vertice = transform.TransformPoint(vertice);
                vertice.y += getYPos(vertice.x, vertice.z);
                newVertices[i] = transform.InverseTransformPoint(vertice);
            }
            else
            {
                newVertices[i] = oldVertices[i];
            }
        }

        meshFilter.mesh.vertices = newVertices;
    }

    private float getYPos(float x, float z)
    {
        float y = 0;

        y += Mathf.Sin((Time.time * speed + z) / distance) * amplitude;
        return y;
    }
}
