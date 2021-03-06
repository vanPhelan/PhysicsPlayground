using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    public float acceleration = 100.0f;

    [SerializeField]
    private HingeJoint frontLeft;
    [SerializeField]
    private HingeJoint frontRight;
    [SerializeField]
    private HingeJoint rearLeft;
    [SerializeField]
    private HingeJoint rearRight;

    private void Update()
    {
        JointMotor rearLeftMotor = rearLeft.motor;
        rearLeftMotor.targetVelocity = 0.0f;
        rearLeftMotor.force = acceleration;
        rearLeft.motor = rearLeftMotor;
        if (Input.GetKey(KeyCode.A))
        {
            rearLeftMotor.targetVelocity = 500.0f;
            rearLeft.motor = rearLeftMotor;
        }

        JointMotor rearRightMotor = rearRight.motor;
        rearRightMotor.targetVelocity = 0.0f;
        rearRightMotor.force = acceleration;
        rearRight.motor = rearRightMotor;
        if (Input.GetKey(KeyCode.D))
        {
            rearRightMotor.targetVelocity = -500.0f;
            rearRight.motor = rearRightMotor;
        }
    }
}
