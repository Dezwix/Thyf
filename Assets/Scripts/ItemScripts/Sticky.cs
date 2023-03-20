using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Throwable")
        {
            Rigidbody playerRigidbody = collider.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.drag = 45f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Throwable")
        {
            Rigidbody playerRigidbody = collider.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.drag = 0f;
        }
    }
}
