using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public AudioSource audioSource;
    public float Volume;
    public AudioClip itemPickupSound;
    public TMP_Text itemCountText;

    private ThrowManager throwManager;


    private void Awake()
    {
        throwManager = GetComponent<ThrowManager>();
        itemCountText.text = "kunai: " + throwManager.kunaiCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject throwable = other.gameObject;
        if (throwable.tag == "Collectible")
        {
            throwManager.kunaiCount++;
            itemCountText.text = "kunai: " + throwManager.kunaiCount;
            Debug.Log("Now have " + throwManager.kunaiCount + " kunai");

            GameObject throwableParent = throwable.transform.parent.gameObject;

            int throwableIndex = throwManager.kunaiList.IndexOf(throwableParent);
            throwManager.kunaiList.RemoveAt(throwableIndex);
            throwManager.cameraFollow.Target = transform;
            Destroy(throwableParent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            other.gameObject.tag = "Collectible";
            itemCountText.text = "kunai: " + throwManager.kunaiCount;
            Debug.Log("Now have " + throwManager.kunaiCount + " kunai");
        }
    }
}
