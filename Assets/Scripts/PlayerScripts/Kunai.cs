using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public bool teleportable = true;

    private bool touchedAnything = false;
    private Rigidbody rigidbody;
    private ItemCollector collector;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collector = FindObjectOfType<ItemCollector>();
    }

    void Update()
    {
        if (!touchedAnything)
            transform.forward = rigidbody.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchedAnything = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchedAnything = false;
    }

    private void OnTriggerEnter(Collider collider)
     {
        if (collider.gameObject.tag == "Coin")
            collector.CollectCoin(collider.gameObject);

        if (collider.gameObject.tag == "CollectibleKunai")
            collector.CollectKunai(collider.gameObject);
    }
}
