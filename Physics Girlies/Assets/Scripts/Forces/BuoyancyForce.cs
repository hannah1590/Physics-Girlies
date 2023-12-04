using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyForce : ForceGenerator3D
{
    [SerializeField] CubeWave ocean;

    Vector3 centerBuoyancy;
    float maxDepth;
    float volume = 1; // current volume submerged
    float waterHeight;
    float liquidDensity = 1000;
    private float buoyant = 0;
    public float drag = 0.47f;

    public override void UpdateForce(Particle2D particle)
    {

    }
    public override void UpdateForce(Particle3D particle)
    {
        float radius = particle.GetComponent<Sphere>().Radius; // radius of sphere
        float area = Mathf.PI * Mathf.Pow(radius, 2); // area of sphere
        waterHeight = ocean.Height - transform.position.y; // height of ocean minus current sphere pos

        if (waterHeight > 0.0f)
        {
            volume = Mathf.PI * Mathf.Pow(radius, 3) * (4.0f / 3.0f); // volume of sphere
            float submergedVolume = Mathf.PI * Mathf.Pow(waterHeight, 2) * (radius - waterHeight / 3.0f); // submerged volume of sphere
            Vector3 force = (submergedVolume / volume) * particle.gravity;
            force -= drag * 0.5f * area * Vector3.up; // buoyancy force minus drag force
            particle.AddForce(force);
        }
    }
}
