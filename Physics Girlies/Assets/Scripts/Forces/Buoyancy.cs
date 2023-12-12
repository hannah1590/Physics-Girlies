using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : ForceGenerator3D
{
    [SerializeField] float liquidDensity = 1000.0f; // Density of the liquid (water)
    [SerializeField] float gravityScale = 1.0f;     // Adjust this to control gravity influence on the object
    [SerializeField] float buoyancyCoefficient = 0.5f; // Adjust this to control buoyancy force
    [SerializeField] float buoyancyDamping = 0.1f; // Adjust this to control buoyancy damping
    [SerializeField] SphereWave oceanWave;


    public override void UpdateForce(Particle2D particle)
    {
        // Not implemented for 2D particles
    }
    public override void UpdateForce(Particle3D particle)
    { 
        Vector3 center = particle.GetComponent<Sphere>().Center;

        float waveHeight = oceanWave.GetWaveHeightAtPosition(center.x, center.z);
        float waterSurface = oceanWave.transform.position.y + waveHeight;

        if (center.y + particle.GetComponent<Sphere>().Radius < waterSurface)
        {
            float volume = (4.0f / 3.0f) * Mathf.PI * Mathf.Pow(particle.GetComponent<Sphere>().Radius, 3);
            Vector3 buoyancyForce = Vector3.up * liquidDensity * Mathf.Abs(Physics.gravity.y) * volume * buoyancyCoefficient;
            buoyancyForce -= buoyancyDamping * particle.velocity;
            particle.AddForce(buoyancyForce - gravityScale * Physics.gravity);
        }
    }
}
