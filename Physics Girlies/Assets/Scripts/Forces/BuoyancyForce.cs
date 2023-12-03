using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyForce : ForceGenerator3D
{
    [SerializeField] Wave ocean;

    Vector3 centerBuoyancy;
    float maxDepth;
    float volume;
    float waterHeight;
    float liquidDensity;
    private float buoyant = 0;
    private float drag;

    public override void UpdateForce(Particle2D particle)
    {

    }
    public override void UpdateForce(Particle3D particle)
    {
        if(waterHeight > 0)//if it collides with the water
        {
            //liquid density is the density of the water
            //water height is how much the object is in the water

            Vector3 force = waterHeight * liquidDensity * -particle.gravity;
            particle.AddForce(force);
            particle.velocity.y += force.magnitude;
        }
    }
}
