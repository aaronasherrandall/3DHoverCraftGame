using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class AimingControls : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirtualCamera;

    private void Update() 
    {
        
    }

    // Set up a variable to store the horizontal and vertical movement of the right stick 
    Vector2 rightStickInput; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // Read the input from the right stick 
    //     rightStickInput = new Vector2 (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //     // Rotate the character according to the right stick input 
    //     transform.Rotate(Vector3.up * rightStickInput.x, Space.World); 
    //     transform.Rotate(Vector3.right * rightStickInput.y, Space.Self); 

    // }
}
