using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public AudioSource audioSource;
    public float Volume;
    public AudioClip itemPickupSound;
    public AudioClip kunaiPickupSound;
    public GameObject kunaiCollectEffect;
    public TMP_Text itemCountText;
    
    public int CoinCount { get; set; }
    public TMP_Text coinCountText;

    private ThrowManager throwManager;
    private GameManager gameManager;
    private Player player;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        throwManager = GetComponent<ThrowManager>();
        player = GetComponent<Player>();
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
        if (collided.tag == "Thrown")
        {
            throwManager.kunaiCount++;
            itemCountText.text = "kunai:" + throwManager.kunaiCount;
            Debug.Log("Now have " + throwManager.kunaiCount + " kunai");

            GameObject throwableParent = collided.transform.parent.gameObject;

            int throwableIndex = throwManager.kunaiList.IndexOf(throwableParent);
            throwManager.kunaiList.RemoveAt(throwableIndex);
            Debug.Log("Kunai List:" + throwManager.kunaiList.Count);
            throwManager.cameraFollow.Target = transform;
            player.ToggleMovement(true);

            Destroy(throwableParent);
        }

        if (collided.tag == "Coin")
        {
            audioSource.PlayOneShot(itemPickupSound);
            CoinCount++;
            coinCountText.text = "x" + CoinCount + "/" + gameManager.coinsTotal;
            Destroy(collided);
            gameManager.checkFinish(CoinCount);
        }

        if (collided.tag == "CollectibleKunai")
        {
            audioSource.PlayOneShot(kunaiPickupSound);
            KunaiCollectFX(collided.transform.position);
            
            throwManager.kunaiCount++;
            itemCountText.text = "kunai:" + throwManager.kunaiCount;
            Destroy(collided.transform.parent.gameObject);
        }
    }

    private void KunaiCollectFX(Vector3 position)
    {
        kunaiCollectEffect.transform.position = position;
        kunaiCollectEffect.SetActive(false);
        kunaiCollectEffect.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            other.gameObject.tag = "Thrown";
            itemCountText.text = "kunai:" + throwManager.kunaiCount;
            Debug.Log("Now have " + throwManager.kunaiCount + " kunai");
        }
    }
}
