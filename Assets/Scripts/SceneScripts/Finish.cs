using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]
    public string nextLevel = "Level 0";

    [SerializeField]
    public Material activeMaterial;

    [SerializeField]
    public Material inactiveMaterial;

    [SerializeField]
    public AudioClip activateSound;

    [SerializeField]
    public AudioClip accessDenied;

    [SerializeField]
    public GameObject portalFx;

    [System.NonSerialized] public bool finishActive = false;

    private GameManager gameManager;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3 && finishActive)
            gameManager.LoadLevel(nextLevel);
        
        if (other.gameObject.layer == 3 && !finishActive)
            audioSource.PlayOneShot(accessDenied);

    }

    public void ToggleFinish(bool active)
    {
        // If already activated - return
        if (finishActive == active && active == true)
            return;

        finishActive = active;

        if (active)
        {
            meshRenderer.material = activeMaterial;
            audioSource.PlayOneShot(activateSound);
            portalFx.SetActive(active);
        }
        else
        {
            meshRenderer.material = inactiveMaterial;
        }
    }
}
