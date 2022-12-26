using System.Collections;
using UnityEngine;
using Cinemachine;
//using System.Numerics;

public class CameraLook : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachine;
    private FZeroMockUp playerControls;

    private void Awake() {
        playerControls = new FZeroMockUp();
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //Read in value
        //Gives us delta or difference between on pointer down and current position
        Vector2 delta = playerControls.Player.Look.ReadValue<Vector2>();
    }
}
