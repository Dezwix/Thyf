using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public Transform Target 
    {
        get { return target; }
        set { target = value; }
    }

    [SerializeField]
    private float smoothSizeSpeed = 2.0f;

    [SerializeField]
    private float smoothPositionSpeed = 0.125f;

    [SerializeField]
    public Vector3 offset;

    [SerializeField]
    private float mapSize = 30f;

    [SerializeField]
    private float playerSize = 12f;

    private GameObject mapAnchor;
    private Camera cameraComponent;
    private Vector3 desiredPosition;
    private bool anchored = false;
    private float startSize;
    private float smoothSizeStep;
    private float smoothPositionStep;

    private void Awake()
    {
        mapAnchor = GameObject.FindGameObjectWithTag("MapAnchor");
        cameraComponent = GetComponent<Camera>();
        startSize = cameraComponent.orthographicSize;
    }

    private void Update()
    {
        smoothSizeStep += smoothSizeSpeed * Time.deltaTime;
        smoothPositionStep += smoothPositionSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Reset smooth step and size when swap
            smoothSizeStep = 0f;
            startSize = cameraComponent.orthographicSize;
            
            // Reset smooth step and size when swap
            smoothPositionStep = 0f;

            if (anchored)
                anchored = false;
            else
                anchored = true;
        }

        if (!anchored)
        {
            desiredPosition = Target.position + offset;
            transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothPositionStep);
            cameraComponent.orthographicSize = Mathf.SmoothStep(startSize, playerSize, smoothSizeStep);
        }

        if (anchored)
        {
            desiredPosition = mapAnchor.transform.position + offset;
            transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothPositionStep);
            cameraComponent.orthographicSize = Mathf.SmoothStep(startSize, mapSize, smoothSizeStep);
        }
    }

    public IEnumerator ChangeTargets(Transform newTarget)
    {
        smoothPositionStep = 0f;
        Target = newTarget;
        desiredPosition = Target.position + offset;

        while(transform.position != desiredPosition)
        {
            smoothPositionStep += smoothPositionSpeed * Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothPositionStep);
            yield return new WaitForSeconds(smoothPositionStep);
        }
    }
}
