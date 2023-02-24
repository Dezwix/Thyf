//using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    #region Public Fields
    public GameObject pointer;
    public float radius = 2f;
    public float chargedRadius = 0.5f;
    public MeshRenderer pointerRenderer;
    #endregion

    #region Private Fields
    float dischargedRadius;
    float distance;
    Vector3 position;
    Vector3 worldPosition;
    Plane plane;
    Ray ray;
    #endregion

    public Vector3 Direction { get; private set; }

    private void Awake()
    {
        plane = new Plane(Vector3.back, 0);
        dischargedRadius = radius;
    }

    private void FixedUpdate()
    {
        if (CursorController.isFrozen)
            return;

        // Get mouse position
        position = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(position);

        // Get intersection position
        if(plane.Raycast(ray, out distance))
            worldPosition = ray.GetPoint(distance);

        this.Direction = (worldPosition - transform.position).normalized;
        pointer.transform.forward = Direction;

        pointer.transform.position = transform.position + Direction * radius;

        float angle = 0f;
        if (Direction.y < 0)
            angle = -Vector3.Angle(this.Direction, Vector3.right);

        if(Direction.y >= 0)
            angle = Vector3.Angle(this.Direction, Vector3.right);

        pointer.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    public IEnumerator Charge()
    {
        while(radius > chargedRadius)
        {
            radius -= 0.1f;
            yield return new WaitForSeconds(0.001f * Time.deltaTime);
        }
    }

    public IEnumerator Discharge()
    {
        while (radius < dischargedRadius)
        {
            radius += 0.1f;
            yield return new WaitForSeconds(0.0001f * Time.deltaTime);
        }
    }

    public void MakeVisible(bool visible)
    {
        pointerRenderer.enabled = visible;
    }
}