using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public bool teleportable = true;

    bool touchedAnything = false;
    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!touchedAnything)
            transform.forward = rigidbody.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "Ground")
            touchedAnything = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.tag == "Ground")
            touchedAnything = false;
    }
}
