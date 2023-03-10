using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Material inactiveSpawn;
    public Material activeSpawn;
    public AudioClip activeClip;

    private bool activePoint = false;
    private MeshRenderer spawnMeshRenderer;
    private AudioSource spawnAudioSource;

    private void Awake()
    {
        spawnMeshRenderer = GetComponent<MeshRenderer>();
        spawnAudioSource = GetComponent<AudioSource>();
    }

    public void ToggleActive(bool activePoint)
    {
        if (activePoint)
            spawnMeshRenderer.material = activeSpawn;
        else
            spawnMeshRenderer.material = inactiveSpawn;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            ToggleActive(true);

            if(!activePoint)
                spawnAudioSource.PlayOneShot(activeClip);
            
            activePoint = true;
        }
    }
}
