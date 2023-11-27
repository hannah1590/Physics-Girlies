using System.Collections;
using System.Collections.Generic;
using static CollisionDetection;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{

    private void StandardCollisionResolution()
    {
        OBB[] oBBs = FindObjectsOfType<OBB>();
        Sphere[] spheres = FindObjectsOfType<Sphere>();
        PlaneCollider[] planes = FindObjectsOfType<PlaneCollider>();
        for (int i = 0; i < oBBs.Length; i++)
        {
            OBB s1 = oBBs[i];
            for (int j = i + 1; j < oBBs.Length; j++)
            {
                OBB s2 = oBBs[j];
                ApplyCollisionResolution(s1, s2);
            }
            foreach (PlaneCollider plane in planes)
            {
                ApplyCollisionResolution(s1, plane);
            }
        }

        for (int i = 0; i < spheres.Length; i++)
        {
            Sphere s1 = spheres[i];
            for (int j = i + 1; j < spheres.Length; j++)
            {
                Sphere s2 = spheres[j];
                ApplyCollisionResolution(s1, s2);
            }
            foreach (PlaneCollider plane in planes)
            {
                ApplyCollisionResolution(s1, plane);
            }
            foreach(OBB obb in oBBs)
            {
                ApplyCollisionResolution(s1, obb);
            }
        }
    }

    private void FixedUpdate()
    {
        CollisionChecks = 0;

        StandardCollisionResolution();
    }
}
