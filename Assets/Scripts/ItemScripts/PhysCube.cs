using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysCube : MonoBehaviour
{
    Rigidbody cubeRigidbody;

    private void Awake()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        cubeRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }
}
