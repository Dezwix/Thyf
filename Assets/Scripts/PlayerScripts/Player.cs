using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject SpawnPoint;
    public AudioClip deathClip;
    public GameObject deathEffect;
    public GameObject teleportEffect;
    public float secondsToRespawn = 3f;

    private Rigidbody playerRigidbody;
    private PlayerMovement movement;
    private MeshRenderer meshRender;
    private ThrowManager throwManager;
    private Pointer pointer;
    private BoxCollider playerCollider;

    [System.NonSerialized] public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
        meshRender = GetComponent<MeshRenderer>();
        throwManager= GetComponent<ThrowManager>();
        pointer = GetComponent<Pointer>();
        playerCollider = GetComponent<BoxCollider>();
    }

    public void TeleportFX(Vector3 position)
    {
        teleportEffect.transform.position = position;
        teleportEffect.SetActive(false);
        teleportEffect.SetActive(true);
    }

    public void Die()
    {
        audioSource.PlayOneShot(deathClip);
        deathEffect.SetActive(false);
        deathEffect.SetActive(true);
        throwManager.ResetCharge();

        StartCoroutine(DelayedRespawn(secondsToRespawn));
    }

    IEnumerator DelayedRespawn(float seconds)
    {
        playerRigidbody.velocity = Vector3.zero;
        movement.enabled = false;
        meshRender.enabled = false;
        throwManager.enabled = false;
        playerCollider.enabled = false;
        pointer.MakeVisible(false);
        yield return new WaitForSeconds(seconds);
        transform.position = SpawnPoint.transform.position;
        movement.enabled = true;
        meshRender.enabled = true;
        throwManager.enabled = true;
        playerCollider.enabled = true;
        pointer.MakeVisible(true);
    }

    public void ToggleMovement(bool active)
    {
        movement.InputEnabled = active;
        throwManager.InputEnabled = active;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Checkpoint")
        {
            SpawnPoint = collider.gameObject;
            Debug.Log("Make this yout check point");
        }
    }
}
