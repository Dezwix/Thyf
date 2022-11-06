using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public AudioSource audioSource;
    public float Volume;
    public AudioClip coinPickupSound;

    private GameManager gameManager;
    private int keyCount = 0;

    public TMP_Text keyCountText;

    private void Awake()
    {
        // this is probably risky since we don't know which object will be created first
        gameManager = FindObjectOfType<GameManager>();
        keyCountText.text = keyCount + " / " + gameManager.winScore;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            keyCount++;
            keyCountText.text = keyCount + " / " + gameManager.winScore;
            if(keyCount == gameManager.winScore)
            {
                gameManager.WinGame();
            }

            audioSource.PlayOneShot(coinPickupSound, Volume);
        }
    }
}
