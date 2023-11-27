using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyForce : ForceGenerator3D
{
    Vector3 centerBuoyancy;
    float maxDepth;
    float volume;
    float waterHeight;
    float liquidDensity;

    public override void UpdateForce(Particle2D particle)
    {

    }
    public override void UpdateForce(Particle3D particle)
    {
       
    }
}
