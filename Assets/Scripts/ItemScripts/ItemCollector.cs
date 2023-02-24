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
    
    public int CoinCount { get; set; }
    public TMP_Text coinCountText;

    private ThrowManager throwManager;
    private GameManager gameManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        throwManager = GetComponent<ThrowManager>();
    }

    private void Start()
    {
        CoinCount = gameManager.coins;
        itemCountText.text = "kunai:" + throwManager.kunaiCount;
        coinCountText.text = "x" + CoinCount + "/" + gameManager.coinsTotal;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "Collectible")
        {
            throwManager.kunaiCount++;
            itemCountText.text = "kunai:" + throwManager.kunaiCount;
            Debug.Log("Now have " + throwManager.kunaiCount + " kunai");

            GameObject throwableParent = collided.transform.parent.gameObject;

            int throwableIndex = throwManager.kunaiList.IndexOf(throwableParent);
            throwManager.kunaiList.RemoveAt(throwableIndex);
            Debug.Log("Kunai List:" + throwManager.kunaiList.Count);
            throwManager.cameraFollow.Target = transform;
            Destroy(throwableParent);
        }

        if (collided.tag == "Coin")
        {
            audioSource.PlayOneShot(itemPickupSound);
            CoinCount++;
            coinCountText.text = "x" + CoinCount + "/" + gameManager.coinsTotal;
            Destroy(collided);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            other.gameObject.tag = "Collectible";
            itemCountText.text = "kunai:" + throwManager.kunaiCount;
            Debug.Log("Now have " + throwManager.kunaiCount + " kunai");
        }
    }
}
