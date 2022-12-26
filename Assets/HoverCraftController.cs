using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;
using Cinemachine;



public class HoverCraftController : MonoBehaviour
{   
    public Shooter shooter;

    public CinemachineVirtualCamera cinemachineVirtualCamera;

    //Handle Looking
    // [Header("Mouse Cursor Settings")]
    // public bool cursorLocked = true;
    // public bool cursorInputForLook = true;
    // public Vector2 look;

    // public Vector2 MouseDelta;


    // public void OnLook(InputAction.CallbackContext context)
    // {
    //     MouseDelta = context.ReadValue<Vector2>();
    // }


    public FZeroMockUp playerControls;
    //Reference to Cinemachine

    private InputAction move;
    private InputAction fire;
    private InputAction look;

    
    private void Awake() 
    {
        playerControls = new FZeroMockUp();
        shooter = FindObjectOfType<Shooter>();
        
    }

    
    private void OnEnable() 
    {
        move = playerControls.Player.Move;
        move.Enable();

        look = playerControls.Player.Look;
        look.Enable();
        look.performed += Look;

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;


    }

    private void OnDisable() 
    {
        move.Disable();
        fire.Disable();
        look.Disable();
    }

    //Vector2 inputs = Vector2.zero
    Vector2 inputs = Vector2.zero;
    Vector2 lookinputs = Vector2.zero;

    public Vector2 MouseDelta;



    //For rotating RigidBody
    Rigidbody theRB;
    //Vector3 m_EulerAngleVelocity;
    
    public Vector3 turnTorque = new Vector3(0,50,0);
    public Vector3 bankingForce = new Vector3(0,0,-50);

    Vector2 inputDirection;


    public float multiplier;
    public float moveForce;
    //public float bankingForce;
    public float bankOffest;

    public float bounceAmount;

    //Use Parallel Array system
    public Transform[] anchors = new Transform[4];
    public RaycastHit[] hits = new RaycastHit[4];

    


    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
    }

    private void Update() 
    {
        //Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputs = move.ReadValue<Vector2>();
        inputDirection = inputs.normalized;
        //lookinputs = look.ReadValue<Vector2>();
        

    }


    private void FixedUpdate() 
    {
        for(int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }

        theRB.AddForce(inputDirection.y * moveForce * transform.forward);
        if(inputDirection.y <= 0)
        {

        }
        
        //Apply bounce based on Horizontal input
        //theRB.AddForce(Input.GetAxis("Horizontal") * turnTorque * transform.up);
        
        //Apply constant bounce 
        //theRB.AddForce(bounceAmount * transform.up);
        //float horizontalInput = Input.GetAxis("Horizontal");
        //m_EulerAngleVelocity = new Vector3(0, horizontalInput, 0);
        
        //Rotate using transform - ORIGINAL SOLUTION
        //This rotates the vehicle left and right depending on horizontal input
        //transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * turnTorque);
        //transform.Rotate(-Vector3.forward * (horizontalInput * bankOffest) * Time.deltaTime * bankingForce);
        
        //Try to actually rotate theRB instead of the transform - RB SOLUTION
        //theRB..Rotate()
        //theRB.Rotate(Vector3.up * horizontalInput * Time.deltaTime * turnTorque);
        //theRB.Rotate(-Vector3.forward * (horizontalInput * bankOffest) * Time.deltaTime * bankingForce);
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime );
        Quaternion deltaRotation = Quaternion.Euler(inputDirection.x * turnTorque * Time.deltaTime);
        theRB.MoveRotation(theRB.rotation * deltaRotation);

        //Attempt to create banking effect by rotating the RB
        Quaternion deltaRotationBank = Quaternion.Euler(inputDirection.x * bankingForce * Time.deltaTime);
        theRB.MoveRotation(theRB.rotation * deltaRotationBank);

        //theRB.MoveRotation(theRB.rotation * deltaRotation);


        //Need to rotate Z rotation based off of horizontal input

        //Apply the banking effect
        //theRB.AddTorque(-Vector3.right * bankingForce * horizontalInput * Time.deltaTime);   


    }

    


    //For each 4 points on Hover Craft, we shoot a raycast down
    //On the y-axis, depending on how far we are from the ground the more force gets added to balance it out
    void ApplyForce(Transform anchor, RaycastHit hit)
    {
        //-anchor.up makes it point down
        if(Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            //Upward force exerted on these points in the force that keeps Hover Craft hovering
            float force = 0;
            //Set the force variable as the reciprocal of distance between floor and anchor
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            theRB.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);

        }

    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire Bullet");
        //shooter.FireBullet();
        shooter.Shoot3();
    }

    public void Look(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }
}
