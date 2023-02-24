using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Material inactiveSpawn;
    public Material activeSpawn;

    private MeshRenderer spawnMeshRenderer;

    private void Awake()
    {
        spawnMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleActive(bool active)
    {
        if (active)
            spawnMeshRenderer.material = activeSpawn;
        else
            spawnMeshRenderer.material = inactiveSpawn;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            ToggleActive(true);
        }
    }
}
