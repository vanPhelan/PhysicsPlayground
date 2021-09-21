using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public enum Variable
    {
        DISPLACEMENT,
        INITIAL_VELOCITY,
        FINAL_VELOCITY,
        TIME,
        ACCELERATION
    }

    public ProjectileBehavior projectile;

    public Variable solveFor = Variable.INITIAL_VELOCITY;

    public Transform target;
    public Vector3 launchingVelocity = new Vector3();
    public Vector3 landingVelocity = new Vector3();
    public float projectileTime = 2.0f;
    public float gravityModifier = 1.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 displacement = target.position - transform.position;
            float time = projectileTime;
            Vector3 acceleration = Physics.gravity * gravityModifier;
            LaunchProjectile(displacement, time, acceleration);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 displacement = target.position - transform.position;
            Vector3 initialVelocity = launchingVelocity;
            float time = projectileTime;
            LaunchProjectile(displacement, initialVelocity, time);
        }
    }

    public void LaunchProjectile(Vector3 displacement, float time, Vector3 acceleration)
    {
        Vector3 initialVelocity = FindInitialVelocity(displacement, time, acceleration);

        ProjectileBehavior projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        Rigidbody projectileRigidbody = projectileInstance.GetRigidbody();
        projectileRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
    }

    public void LaunchProjectile(Vector3 displacement, Vector3 initialVelocity, float time)
    {
        Vector3 acceleration = FindAcceleration(displacement, initialVelocity, time);

        ProjectileBehavior projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        Rigidbody projectileRigidbody = projectileInstance.GetRigidbody();
        ConstantForce projectileConstantForce = projectileInstance.GetConstantForce();
        projectileRigidbody.useGravity = false;
        projectileConstantForce.force = acceleration;
        projectileRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
    }

    private Vector3 FindFinalVelocity(Vector3 initialVelocity, float time, Vector3 acceleration)
    {
        //v = v0 + a*t
        Vector3 finalVelocity = initialVelocity + acceleration * time;

        return finalVelocity;
    }

    private Vector3 FindDisplacement(Vector3 initialVelocity, float time, Vector3 acceleration)
    {
        //∆x = v0*t + (1/2)*a*t^2
        Vector3 displacement = initialVelocity * time + 0.5f * acceleration * time * time;

        return displacement;
    }

    private Vector3 FindInitialVelocity(Vector3 displacement, float time, Vector3 acceleration)
    {
        //∆x = v0*t + (1/2)*a*t^2
        //∆x - (1/2)*a*t^2 = v0*t
        //∆x/t - (1/2)*a*t = v0
        //v0 = ∆x/t - (1/2)*a*t
        Vector3 initialVelocity = displacement / time - 0.5f * acceleration * time;

        return initialVelocity;
    }

    //private Vector3 FindInitialVelocity(Vector3 displacement, Vector3 finalVelocity, Vector3 acceleration)
    //{
    //    //v^2 = v0^2 + 2a∆x
    //    //v^2 - 2a∆x = v0^2
    //    //sqrt(v^2 - 2a∆x) = v0
    //    //v0 = sqrt(v^2 - 2a∆x)
    //    //v0 = v - sqrt(2a∆x)
    //    Vector3 initialVelocity = finalVelocity - (Vector3.Cross(acceleration, displacement) * 2);
    //}

    private Vector3 FindAcceleration(Vector3 displacement, Vector3 initialVelocity, float time)
    {
        //∆x = v0*t + (1/2)*a*t^2
        //∆x - v0*t = (1/2)*a*t^2
        //2*(∆x - v0*t) = a*t^2
        //2/t*(∆x/t - v0) = a
        //a = 2/t*(∆x/t - v0)
        Vector3 acceleration = 2 / time * (displacement / time - initialVelocity);

        return acceleration;
    }
}
