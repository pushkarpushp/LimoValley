using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class CarController : MonoBehaviourPunCallbacks, IPunObservable
{
    public InputActionAsset starterAsset;
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    SpawnPlayers spawner;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;


    public UIVirtualJoystick moveJoystick;

    Boat boat;
    private void Start()
    {
        spawner = FindObjectOfType<SpawnPlayers>();
        boat = GetComponent<Boat>();
        moveJoystick = GameObject.Find("UI_Virtual_Joystick_Move").GetComponent<UIVirtualJoystick>();
        if(moveJoystick != null)
            moveJoystick.joystickOutputEvent.AddListener(GetJoystickInput);
    }

    private void FixedUpdate()
    {
        //if(boat.driverPhotonView != null && boat.driverPhotonView.IsMine) starterAsset = spawner.starterAsset;
        if (boat.driverPhotonView == null || !boat.driverPhotonView.IsMine) return;
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            if (boat.driverPhotonView == null || !boat.driverPhotonView.IsMine) return;

            stream.SendNext(horizontalInput);
            stream.SendNext(verticalInput);
        }
        else
        {
            horizontalInput = (float)stream.ReceiveNext();
            verticalInput = (float)stream.ReceiveNext();
        }
    }
    private void GetJoystickInput(Vector2 input)
    {
        if (!boat.driverPhotonView && boat.driverPhotonView.IsMine)
        {
            horizontalInput = input.x;
            verticalInput = input.y;
        }
    }
    private void GetInput()
    {
        if(boat.driverSeat != -1)
        {
#if UNITY_ANDROID

#else
            if(boat.driverPhotonView != null && boat.driverPhotonView.IsMine)
            {
                Vector2 moveInput = starterAsset.FindAction("Move").ReadValue<Vector2>();
                // Steering Input
                horizontalInput = moveInput.x;//Input.GetAxis("Horizontal");

                // Acceleration Input
                verticalInput = moveInput.y;//Input.GetAxis("Vertical");
                // Breaking Input
                isBreaking = Input.GetKey(KeyCode.Space);

            }
#endif
        }
        else
        {

            horizontalInput = 0;
            verticalInput = 0;
            isBreaking = true;
        }

       
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}