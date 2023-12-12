using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSphere : MonoBehaviour
{
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject ocean;
    [SerializeField] private float height;
    [SerializeField] private float min;
    [SerializeField] private float max;

    public void CreateSphere()
    {
        Vector3 position = new Vector3(Random.Range(min, max), height, ocean.transform.position.z);
        Instantiate(spherePrefab, position, spherePrefab.transform.rotation);
    }
}
