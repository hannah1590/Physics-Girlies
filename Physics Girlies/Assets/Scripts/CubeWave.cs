using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CubeWave : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float distance = 1f;
    //[SerializeField] private MeshFilter meshFilter;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float depth;
    private Bounds bounds;

    private Vector3[] oldVertices;
    private Vector3[] newVertices;

    private void Awake()
    {
        //oldVertices = meshFilter.mesh.vertices;
        //bounds = meshFilter.mesh.bounds;
    }

    void OnEnable()
    {
        //var mesh = new Mesh
        //{
            //name = "Procedural Mesh"
        //};

        /*
        float halfWidth = width / 2;
        float halfHeight = height / 2; 
        float halfDepth = depth / 2;

        mesh.vertices = new Vector3[] {
            new Vector3(-halfWidth, -halfHeight, -halfDepth), // 0 now 0
            new Vector3(-halfWidth, halfHeight, -halfDepth), // 1 now 1
            new Vector3(0, 0, -halfDepth),
            new Vector3(halfWidth, -halfHeight, -halfDepth), // 3 now 3
            new Vector3(halfWidth, halfHeight, -halfDepth), // 2 now 4
            new Vector3(-halfWidth, -halfHeight, halfDepth), // 4 now 5
            new Vector3(-halfWidth, halfHeight, halfDepth), // 5 now 6
            new Vector3(0, 0, halfDepth),
            new Vector3(halfWidth, -halfHeight, halfDepth), // 7 now 8
            new Vector3(halfWidth, halfHeight, halfDepth), // 6 now 9
            new Vector3(-halfWidth, 0, 0),
            new Vector3(halfWidth, 0, 0),
            new Vector3(0, halfHeight, 0),
            new Vector3(0, -halfHeight, 0)
        };

        mesh.normals = new Vector3[] {
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back
        };

        mesh.tangents = new Vector4[] {
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f)
        };

        mesh.uv = new Vector2[] {
            new Vector3(-halfWidth, -halfHeight, -halfDepth), // 0 now 0
            new Vector3(-halfWidth, halfHeight, -halfDepth), // 1 now 1
            new Vector3(0, 0, -halfDepth),
            new Vector3(halfWidth, -halfHeight, -halfDepth), // 3 now 3
            new Vector3(halfWidth, halfHeight, -halfDepth), // 2 now 4
            new Vector3(-halfWidth, -halfHeight, halfDepth), // 4 now 5
            new Vector3(-halfWidth, halfHeight, halfDepth), // 5 now 6
            new Vector3(0, 0, halfDepth),
            new Vector3(halfWidth, -halfHeight, halfDepth), // 7 now 8
            new Vector3(halfWidth, halfHeight, halfDepth), // 6 now 9
            new Vector3(-halfWidth, 0, 0),
            new Vector3(halfWidth, 0, 0),
            new Vector3(0, halfHeight, 0),
            new Vector3(0, -halfHeight, 0)
        };

        mesh.triangles = new int[] {
            // front face
            0, 1, 2,
            0, 2, 3,
            3, 2, 4,
            2, 1, 4,

            // back face
            5, 6, 7,
            5, 7, 8,
            8, 7, 9,
            7, 6, 9,

            // left face
            5, 6, 10,
            5, 10, 0,
            0, 10, 1,
            10, 6, 1,

            // right face
            3, 4, 11,
            3, 11, 8,
            8, 11, 9,
            11, 4, 9,

            // top face
            1, 6, 12,
            1, 12, 4,
            4, 12, 9,
            12, 6, 9,

            // bottom face
            5, 0, 13,
            5, 13, 8,
            8, 13, 3,
            13, 0, 3
        };
        */
        //GetComponent<MeshFilter>().mesh = mesh;

        //oldVertices = meshFilter.mesh.vertices;
        //bounds = meshFilter.mesh.bounds;
    }

    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        transform.gameObject.SetActive(true);

        oldVertices = mesh.vertices;
        bounds = mesh.bounds;
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

        GetComponent<MeshFilter>().mesh.vertices = newVertices;
    }
    
    private float getYPos(float x, float z)
    {
        float y = 0;

        y += Mathf.Sin((Time.time * speed + z) / distance) * amplitude;
        return y;
    }
}
