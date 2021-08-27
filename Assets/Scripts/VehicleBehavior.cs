using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    public HingeJoint frontLeft;
    public HingeJoint frontRight;
    public HingeJoint rearLeft;
    public HingeJoint rearRight;

    private void Update()
    {
        JointMotor rearLeftMotor = rearLeft.motor;
        rearLeftMotor.targetVelocity = 0.0f;
        rearLeft.motor = rearLeftMotor;
        if (Input.GetKey(KeyCode.A))
        {
            rearLeftMotor.targetVelocity = 500.0f;
            rearLeft.motor = rearLeftMotor;
        }

        JointMotor rearRightMotor = rearRight.motor;
        rearRightMotor.targetVelocity = 0.0f;
        rearLeft.motor = rearRightMotor;
        if (Input.GetKey(KeyCode.D))
        {
            rearRightMotor.targetVelocity = -500.0f;
            rearRight.motor = rearRightMotor;
        }
    }
}
