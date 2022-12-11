using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public Transform Target 
    {
        get { return target; }
        set { target = value; }
    }

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = Target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
