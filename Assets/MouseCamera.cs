using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private float cursorSpeed = 500f;
    [SerializeField] private float padding = 50f;

    public Vector2 currentPosition;
    public Vector2 newPosition;

    private bool previousMouseState;
    private Mouse virtualMouse;
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        if(virtualMouse == null)
        {   
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if(!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        //Pair the device to the user to use the PlayerInput component with the Event System & Virtual Mouse
        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if(cursorTransform != null)
        {
            //anchoredPosition takes into account the anchoredPosition of a RectTransform
            Vector2 position = cursorTransform.anchoredPosition;
            //Changing state of virtualmouse.position to where cursor is at
            InputState.Change(virtualMouse.position, position);  
        }
        
        //Similar to LateUpdate() function
        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable() 
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    //Moves virtual mouse in accordance with our gamepad input
    private void UpdateMotion()
    {
        if(virtualMouse == null || Gamepad.current == null) 
        {
            return;
        }
        //Joystick value
        //Can do rightStick as well
        //Delta - change in position between previous and current frame
        Vector2 deltaValue = Gamepad.current.rightStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        //With delta, take current position and add delta to it; y = mx + b (slope)
        currentPosition = virtualMouse.position.ReadValue();
        //print(newpo);
        newPosition = currentPosition + deltaValue;

        //print(currentPosition);

        //Make sure we don't go out of bounds from out screen
        //0 clamps it right at edge of screen
        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding); //TODO - add padding
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        //Change virtual mouse to take in new position and new delta value
        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        //We only want to click a button when state has changed
        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if(previousMouseState != aButtonIsPressed)
        {
            //

            //Output variable called mouseState that we can use within if statement scope
            //It copies state of current virtual mouse
            virtualMouse.CopyState<MouseState>(out var mouseState);
            //Mapping A button to Mouse Left Button
            mouseState.WithButton(MouseButton.Left, Gamepad.current.aButton.IsPressed());
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }
        AnchorCursor(newPosition);

    }

    //Change cursor on screen
    //Due to scaling issues, we must map a screen cordinate position to a RectTransform cordinate
    public void AnchorCursor(Vector2 position){
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode 
        == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }

    public Vector2 AnchorCursorToRead(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode 
        == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
        return anchoredPosition;
    }
    
    //public Vector2 aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // aim.x += Input.GetAxis("Mouse X");
        // aim.y += Input.GetAxis("Mouse Y");
        // transform.localRotation = Quaternion.Euler(-aim.y, aim.x, 0);

    }
}
