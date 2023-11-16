using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wave : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private float noiseStrength = 1f;
    [SerializeField] private float noiseWalk = 1f;
    [SerializeField] private MeshFilter meshFilter;

    private Vector3[] oldVertices;
    private Vector3[] newVertices;

    private void Awake()
    {
        oldVertices = meshFilter.mesh.vertices;
    }

    private void Update()
    {
        newVertices = new Vector3[oldVertices.Length];

        for (int i = 0; i < oldVertices.Length; i++)
        {
            Vector3 vertice = oldVertices[i];
            vertice = transform.TransformPoint(vertice);
            vertice.y += getYPos(vertice.x, vertice.z);
            newVertices[i] = transform.InverseTransformPoint(vertice);
        }

        meshFilter.mesh.vertices = newVertices;
    }

    private float getYPos(float x, float z)
    {
        float y = 0;

        y += Mathf.Sin((Time.time * speed + z) / distance) * amplitude;
        y += Mathf.PerlinNoise(x + noiseWalk, z + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
        return y;
    }
}
