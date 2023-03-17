using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleArea : MonoBehaviour
{
    public GameObject madeBy;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            madeBy.SetActive(true);
        }
    }
}
