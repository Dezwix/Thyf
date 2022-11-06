using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    #region Public Fields
    public GameObject pointer;
    public float radius = 5f;
    #endregion

    #region Private Fields
    float full;
    float multiplier;
    float xDelta;
    float yDelta;
    float distance;
    Vector3 delta;
    Vector3 localZero;
    Vector3 position;
    Vector3 worldPosition;
    Plane plane = new Plane(Vector3.back, 0);
    Ray ray;
    #endregion

    private void Update()
    {
        if (CursorController.isFrozen)
            return;

        // Get mouse position
        position = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Get intersection position
        if(plane.Raycast(ray, out distance))
            worldPosition = ray.GetPoint(distance);

        SnapOnCircle();
        Direction();
        
    }

    void SnapOnCircle()
    {
        // Full distance between the player and the pointer
        full = Mathf.Sqrt(Mathf.Pow(transform.position.y - worldPosition.y, 2) + Mathf.Pow(transform.position.x - worldPosition.x, 2));

        // How much radius triangle is smaller than the full triangle
        multiplier = radius / full;

        // Position of the point on the circle
        xDelta = (worldPosition.x - transform.position.x) * multiplier;
        yDelta = (worldPosition.y - transform.position.y) * multiplier;

        // Contains x and y values of the point on the circle
        delta = new Vector3(xDelta, yDelta, 0);

        // Player's position
        localZero = transform.position;

        // Assign pointer relatively to player's position and offset
        pointer.transform.position = delta + localZero;
    }

    void Direction()
    {
        // I and IV quarters
        if (xDelta >= 0)
           pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(yDelta / radius) * Mathf.Rad2Deg - 225f));

        // II and III quarters
        if(xDelta < 00)
            pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f - Mathf.Asin(yDelta / radius) * Mathf.Rad2Deg - 225f));
    }
}
