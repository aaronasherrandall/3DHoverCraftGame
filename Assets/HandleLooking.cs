// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class HandleLooking : MonoBehaviour
// {
//     private HoverCraftController _input;
//     private FZeroMockUp _playerInput;
//     private const float _threshold = 0.01f;
//     public bool LockCameraPosition = false;
//     public GameObject CinemachineCameraTarget;

//     //Cinemachine
//     private float _cinemachineTargetYaw;

//     private float _cinemachineTargetPitch;

//     public float BottomClamp = -30.0f;
//     public float TopClamp = 70.0f;
//     public float CameraAngleOverride = 0.0f;



    

//     private bool IsCurrentDeviceMouse
//     {
//     get
//         {
//     #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
//             return _playerInput.currentControlScheme == "KeyboardMouse";
//     #else
//             return false;
//     #endif
//         }
//     }

//             private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
//         {
//             if (lfAngle < -360f) lfAngle += 360f;
//             if (lfAngle > 360f) lfAngle -= 360f;
//             return Mathf.Clamp(lfAngle, lfMin, lfMax);
//         }




//     // Start is called before the first frame update
//     void Start()
//     {
//         _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
//         _input = GetComponent<HoverCraftController>();
//         _playerInput = GetComponent<FZeroMockUp>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private void CameraRotation()
//         {
//             // if there is an input and camera position is not fixed
//             if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
//             {
//                 //Don't multiply mouse input by Time.deltaTime;
//                 float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

//                 _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
//                 _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
//             }

//             // clamp our rotations so our values are limited 360 degrees
//             _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
//             _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

//             // Cinemachine will follow this target
//             CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
//             _cinemachineTargetYaw, 0.0f);
//         }
// }
