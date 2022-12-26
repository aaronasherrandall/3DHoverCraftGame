using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    // The speed at which the vehicle should move
    public float speed = 10.0f;

    void Update()
    {
        // Get the input axes for the horizontal and vertical movement of the vehicle
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the direction the vehicle should move in based on the input
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        // Move the vehicle in the direction specified by the input
        transform.position += direction * speed * Time.deltaTime;
    }
}

