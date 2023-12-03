using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CubeWave : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private MeshFilter meshFilter;
    private Bounds bounds;

    private Vector3[] oldVertices;
    private Vector3[] newVertices;

    [SerializeField] private TextMeshProUGUI ampText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI distText;
    [SerializeField] private TextMeshProUGUI dirText;

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
            // only makes the wave on the upper half of the shape
            if (bounds.max.y / 2 <= oldVertices[i].y)
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
    
    // gets new y position
    private float getYPos(float x, float z)
    {
        float y = 0;

        y += Mathf.Sin((Time.time * speed + x) / distance) * amplitude;
        return y;
    }

    // changes values based on the UI scrollbars
    public void changeAmplitude(float n)
    {
        amplitude = n;
        ampText.text = String.Format("{0:0.00}", n);
    }

    public void changeDistance(float n)
    {
        distance = n;
        distText.text = String.Format("{0:0.00}", n);
    }

    public void changeSpeed(float n)
    {
        speed = n;
        speedText.text = String.Format("{0:0.00}", n);
    }

    public void changeDirection(float n)
    {
        Quaternion target = Quaternion.Euler(0, n, 0);
        transform.rotation = target;
        dirText.text = String.Format("{0:0}", n);
    }
}
