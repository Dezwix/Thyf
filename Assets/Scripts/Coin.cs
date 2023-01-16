using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    private float floatTime = 1f;

    [SerializeField]
    [Range(0f, 2f)]
    private float floatRange = 0.5f;

    private float lerpPrct;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool up = true;
    private float elapsedTime;


    private void Awake()
    {
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(0f, floatRange, 0f);
    }

    private void Update()
    { 
        elapsedTime += Time.deltaTime;
        lerpPrct = elapsedTime / floatTime;

        if(up)
            transform.position = Vector3.Lerp(startPosition, endPosition, lerpPrct);

        if(!up)
            transform.position = Vector3.Lerp(endPosition, startPosition, lerpPrct);

        if (transform.position == startPosition)
        {
            elapsedTime = 0f;
            up = true;
        }

        if (transform.position == endPosition)
        {
            elapsedTime = 0f;
            up = false;
        }
    }

}
