using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuoyancyForce : ForceGenerator3D
{
    [SerializeField] SphereWave ocean;

    Vector3 centerBuoyancy;
    float maxDepth;
    float volume; // current volume submerged
    float waterHeight;
    public float liquidDensity = 1000;
    private float buoyant = 0;
    public float drag = 0.47f; // drag of water

    private Vector3[] vertices;
    private Bounds bounds;

    private float halfLength;

    public override void UpdateForce(Particle2D particle)
    {

    }

    public override void UpdateForce(Particle3D particle)
    {
        // verticies and bounds of object
        vertices = particle.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        bounds = particle.gameObject.GetComponent<MeshFilter>().mesh.bounds;

        halfLength = transform.localScale.y / 2;

        volume = Mathf.PI * Mathf.Pow(particle.GetComponent<Sphere>().Radius, 3) * (4.0f / 3.0f); // volume of sphere
        float mass;
        if(particle.inverseMass == 0) // multiply volume by mass to get density
        {
            mass = particle.inverseMass;
            volume *= mass; 
        }
        else
        {
            mass = (1 / particle.inverseMass);
            volume *= mass;
        }
        float forceMagnitude = liquidDensity * Mathf.Abs(particle.gravity.y) * volume; // archimedes force
        Vector3 localForce = new Vector3(0, forceMagnitude, 0) / vertices.Length;

        Vector3 center = particle.GetComponent<Sphere>().Center;
        GetOceanHeight(center, particle);
        if(center.y - halfLength < waterHeight) // if in the ocean
        {
            float k = (waterHeight - center.y) / (2 * halfLength) + 0.5f; // adjustment of drag based on depth in water
                
            if (k > 1)
            {
                k = 1f;
            }
            else if (k < 0)
            {
                k = 0f;
            }
                
            Vector3 localDrag = -particle.velocity * drag * mass;
            Vector3 force = localDrag + Mathf.Sqrt(k) * localForce;
            particle.AddForce(force + particle.gravity);
        }
    }

    private void GetOceanHeight(Vector3 particle, Particle3D p)
    {
        Vector3[] vertices = ocean.gameObject.GetComponent<MeshFilter>().mesh.vertices;
        Matrix4x4 transformation = ocean.transform.worldToLocalMatrix;
        Matrix4x4 t = ocean.transform.localToWorldMatrix;
        Bounds bounds = ocean.GetComponent<MeshFilter>().mesh.bounds;
        Vector3 pos = transformation.MultiplyPoint(particle);

        float minX = 100; // just using an absurd number
        float minZ = 100;
        foreach (Vector3 v in vertices)
        {
            if (bounds.max.y / 2 <= v.y)
            {
                // finds the current height of the ocean according to the closest ocean vertex to the sphere
                if (pos.x - v.x < minX && pos.z - v.z < minZ && bounds.max.y / 2 <= v.y)
                {
                    minX = pos.x - v.x;
                    minZ = pos.z - v.z;
                    waterHeight = v.y;
                }
            }
        }
    }
}
