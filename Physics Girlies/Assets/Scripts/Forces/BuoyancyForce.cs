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
    private float drag;

    public override void UpdateForce(Particle2D particle)
    {

    }
    public override void UpdateForce(Particle3D particle)
    {
        getWaterHeight(particle);
        if (waterHeight > 0)//if it collides with the water
        {
            //liquid density is the density of the water
            //water height is how much the object is in the water

            // volume of sphere:  V = (1/3)πh2(3R - h) where h is height of current spherical section and r is new radius
            Vector3 force = ((0.333f) * Mathf.PI * waterHeight * 2 * (3 * waterHeight - waterHeight)) * liquidDensity * -particle.gravity;//waterHeight * liquidDensity * -particle.gravity;
            particle.AddForce(force);
            //particle.velocity.y += force.magnitude;
        }
        
    }

    private void getWaterHeight(Particle3D particle)
    {
        // have no idea if this is right or not; also only did it for sphere
        Vector3 s2ToS1 = particle.transform.position - ocean.Center;
        float dist = s2ToS1.magnitude;
        float sumOfRadii = (particle.GetComponent<Sphere>().Radius + ocean.Height / 2);
        waterHeight = sumOfRadii - dist;
    }
}
