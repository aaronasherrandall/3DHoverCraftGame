using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverVehicle : MonoBehaviour {

//Reference our RigidBody
Rigidbody theRB;

LayerMask groundMask;


private void Awake() {
    theRB = GetComponent<Rigidbody>();  
    LayerMask groundMask = LayerMask.GetMask("Ground");
}
// Set layermask for ground


public float oscillatingForce;
public float forwardForce;

public float turningForce;


// Raycast from the vehicle to check if it's hitting the ground
RaycastHit hit;

private void Update() {

    if (Physics.Raycast(transform.position, Vector3.down, out hit, 10, groundMask))
    {
    // Apply oscillating force to Rigidbody
    theRB.AddForce(transform.up * oscillatingForce);
    }

    // Move vehicle forward
    if (Input.GetAxis("Vertical") > 0)
    {
        theRB.AddForce(transform.forward * forwardForce);
    }

    // Turn vehicle left/right
    if (Input.GetAxis("Horizontal") > 0)
    {
        theRB.AddTorque(transform.up * turningForce);
    }
    else if (Input.GetAxis("Horizontal") < 0)
    {
        theRB.AddTorque(transform.up * -turningForce);
    }
}
}




