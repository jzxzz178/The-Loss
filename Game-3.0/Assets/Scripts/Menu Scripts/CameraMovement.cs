using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float deltaX;
    private bool goLeft = true;
    public float limit;

    void Update()
    {
        if (transform.position.x <= -limit)
            goLeft = false;


        else if (transform.position.x >= limit)
            goLeft = true;


        if (goLeft)
        {
            transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
        }

        if (!goLeft)
        {
            transform.position = new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z);
        }
    }
}