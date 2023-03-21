using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    #region Public Fields
    [SerializeField]
    private GameObject throwable;

    [SerializeField]
    private float strength = 5f;

    [SerializeField]
    private float maxCharge = 2f;

    [SerializeField]
    private float chargeSpeed = 5000f;

    [SerializeField]
    public float kunaiCount = 1f;

    [SerializeField]
    public CameraFollow cameraFollow;

    [SerializeField]
    public bool InputEnabled = true;

    public List<GameObject> kunaiList;
    public AudioClip teleportSound;
    public AudioClip lastTeleportSound;
    public AudioClip throwSound;
    public AudioClip cancelTeleportSound;
    #endregion

    #region Delegates
    event Action onUpdate = () => { };
    #endregion

    #region Private Fields
    float chargeTime = 0f;
    float chargeStrength;
    Vector3 direction;
    Rigidbody playerRigidbody;
    Pointer pointer;
    Coroutine pointerCoroutine;
    AudioSource audioSource;
    Player player;
    ItemCollector collector;
    bool teleportEnabled = true;
    GameObject currentKunai;
    #endregion

    private void Awake()
    {
        player = GetComponent<Player>();
        playerRigidbody = this.GetComponent<Rigidbody>();
        pointer = GetComponent<Pointer>();
        kunaiList = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
        collector = GetComponent<ItemCollector>();

        // Physics.IgnoreLayerCollision(3, 6, true);
        this.onUpdate = CaptureInput;
    }   

    private void Update() => onUpdate();

    void CaptureInput()
    {
        if(InputEnabled & Input.GetKeyDown(KeyCode.Mouse1))
            ResetCharge();

        if (!InputEnabled & teleportEnabled & Input.GetKeyDown(KeyCode.Mouse1))
        {
            teleportEnabled = false;
            StartCoroutine(CancelTeleport(currentKunai));
        }

        if (InputEnabled & Input.GetMouseButtonDown(0))
        {
            if (pointerCoroutine != null)
                StopCoroutine(pointerCoroutine);
            
            if(kunaiCount > 0)
                pointerCoroutine = StartCoroutine(pointer.Charge());

            chargeTime = Time.time;
            onUpdate += Charged;
        }

        if(teleportEnabled & Input.GetKeyDown(KeyCode.Space))
        {
            if (kunaiList.Count == 0)
                return;
            
            GameObject kunaiLast = kunaiList.Last();

            if (!kunaiLast)
                return;

            Teleport(kunaiLast);
        }
    }

    void Regular()
    {
        if (pointer.radius <= pointer.chargedRadius)
        {
            if (pointerCoroutine != null)
                StopCoroutine(pointerCoroutine);


            pointerCoroutine = StartCoroutine(pointer.Discharge());
            Throw(strength);
            onUpdate -= Regular;
        }
    }

    void Charged()
    {
        // Cancel with RMB
        if(Input.GetKeyDown(KeyCode.Mouse1) ||  (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKeyDown(KeyCode.Mouse1)) )
        {
            onUpdate -= Charged;
            pointerCoroutine = StartCoroutine(pointer.Discharge());
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(pointerCoroutine != null)
                StopCoroutine(pointerCoroutine);

            pointerCoroutine = StartCoroutine(pointer.Discharge());
            chargeStrength = Time.time - chargeTime;
            chargeStrength *= chargeSpeed;
            
            if(chargeStrength >= maxCharge)
            {
                chargeStrength = maxCharge;
            }

            Throw(chargeStrength);

            onUpdate -= Charged;
        }
    }

    private void Throw(float throwStrength)
    {
        if (kunaiCount > 0)
        {
            audioSource.PlayOneShot(throwSound);
            // Decrease kunai count
            kunaiCount--;
            GameObject throwable = Spawn();
            currentKunai = throwable;

            // Get throwable's Rigidbody
            Rigidbody throwableRigidbody = throwable.GetComponent<Rigidbody>();

            // Direction of a kunai
            direction = pointer.Direction;

            throwable.transform.rotation = Quaternion.Euler(direction);

            // Perfrorm a throw
            throwableRigidbody.AddForce(direction * throwStrength);

            kunaiList.Add(throwable);
            cameraFollow.Target = kunaiList[kunaiList.Count - 1].transform;
            player.ToggleMovement(false);

            collector.UpdateKunaiUI();
        }

        // Replace with UI notifier
        Debug.Log("Out of kunai");
    }

    GameObject Spawn()
    {
        // Spawn a throwable
        GameObject throwable = Instantiate(this.throwable, transform.position, Quaternion.identity);

        // Mark thrown object as a Throwable
        throwable.gameObject.tag = "Throwable";
        return throwable;
    }

    void Teleport(GameObject kunai)
    {
        player.TeleportFX(kunai.transform.position);
        currentKunai = null;

        if (kunaiCount == 0)
            audioSource.PlayOneShot(lastTeleportSound);
        else 
            audioSource.PlayOneShot(teleportSound);

        cameraFollow.Target = transform;

        Destroy(kunai);
        player.ToggleMovement(true);

        // Change player's position to kunai
        transform.position = kunai.transform.position;

        // Reset player's velocity on teleport
        playerRigidbody.velocity = Vector3.zero;
    }

    IEnumerator CancelTeleport(GameObject kunai)
    {
        player.TeleportFX(kunai.transform.position);
        currentKunai = null;

        audioSource.PlayOneShot(cancelTeleportSound);
        kunai.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(cameraFollow.ChangeTargets(transform));
        player.ToggleMovement(true);
        teleportEnabled = true;
        Destroy(kunai);
    }

    public void ResetCharge()
    {
        if (pointerCoroutine != null)
            StopCoroutine(pointerCoroutine);

        pointerCoroutine = StartCoroutine(pointer.Discharge());
        onUpdate -= Charged;
    }

    public void KunaiDestroy()
    {
        if (cameraFollow.Target.gameObject.tag == "Player")
            return;

        GameObject kunai = cameraFollow.Target.gameObject;
        cameraFollow.Target = transform;
        player.ToggleMovement(true);
        Destroy(kunai);
    }
}
