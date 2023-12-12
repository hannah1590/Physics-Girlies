using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWave : MonoBehaviour
{
   public int numWaves = 4;
    public float amplitude = 0.1f;
    public float waveLength = 1.0f;
    public float speed = 1.0f;

    private Vector3[] originalVertices;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[originalVertices.Length];

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];

            // Check if the vertex is in the top hemisphere of the sphere
            if (vertex.y > 0)
            {
                // Apply Gerstner waves
                float waveHeight = 0;

                for (int j = 0; j < numWaves; j++)
                {
                    float waveNumber = j + 1;
                    float dotProduct = Vector3.Dot(new Vector2(vertex.x, vertex.z).normalized, new Vector2(Mathf.Cos(waveNumber), Mathf.Sin(waveNumber)));
                    float wavePhase = speed * Time.time * waveNumber;

                    waveHeight += amplitude * Mathf.Sin(dotProduct * 2 * Mathf.PI / waveLength - wavePhase);
                }

                vertex.y += waveHeight;
            }

            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public float GetWaveHeightAtPosition(float x, float z)
    {
        float waveHeight = 0;

        for (int j = 0; j < numWaves; j++)
        {
            float waveNumber = j + 1;
            float dotProduct = Vector2.Dot(new Vector2(x, z).normalized, new Vector2(Mathf.Cos(waveNumber), Mathf.Sin(waveNumber)));
            float wavePhase = speed * Time.time * waveNumber;

            waveHeight += amplitude * Mathf.Sin(dotProduct * 2 * Mathf.PI / waveLength - wavePhase);
        }

        return waveHeight;
    }

}