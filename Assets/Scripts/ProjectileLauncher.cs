using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Rigidbody projectile;

    public bool useTarget = true;
    public Transform target;
    public bool useTime = true;
    public float projectileTime = 2.0f;
    public bool useGravity = true;
    public float gravityModifier = 1.0f;
    public bool useInitialVelocity = false;
    public Vector3 initialVelocity = new Vector3();
    public bool useFinalVelocity = false;
    public Vector3 finalVelocity = new Vector3();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LaunchProjectile(target.position - transform.position, projectileTime, Physics.gravity);
        }
    }

    public void LaunchProjectile(Vector3 displacement, float time, Vector3 acceleration)
    {
        Vector3 initialVelocity = FindInitialVelocity(displacement, acceleration, time);

        Rigidbody projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
        projectileInstance.AddForce(initialVelocity, ForceMode.VelocityChange);
    }

    public void LaunchProjectile(Vector3 displacement, Vector3 initialVelocity, float time)
    {
        Vector3 acceleration = FindAcceleration(displacement, initialVelocity, time);
        
    }

    private Vector3 FindFinalVelocity(Vector3 initialVelocity, Vector3 acceleration, float time)
    {
        //v = v0 + a*t
        Vector3 finalVelocity = initialVelocity + acceleration * time;

        return finalVelocity;
    }

    private Vector3 FindDisplacement(Vector3 initialVelocity, Vector3 acceleration, float time)
    {
        //deltaX = v0*t + (1/2)*a*t^2
        Vector3 displacement = initialVelocity * time + 0.5f * acceleration * time * time;

        return displacement;
    }

    private Vector3 FindInitialVelocity(Vector3 displacement, Vector3 acceleration, float time)
    {
        //deltaX = v0*t + (1/2)*a*t^2
        //deltaX - (1/2)*a*t^2 = v0*t
        //deltaX/t - (1/2)*a*t = v0
        //v0 = deltaX/t - (1/2)*a*t
        Vector3 initialVelocity = displacement / time - 0.5f * acceleration * time;

        return initialVelocity;
    }

    private Vector3 FindAcceleration(Vector3 displacement, Vector3 initialVelocity, float time)
    {
        //deltaX = v0*t + (1/2)*a*t^2
        //deltaX - v0*t = (1/2)*a*t^2
        //2*(deltaX - v0*t) = a*t^2
        //2/t*(deltaX/t - v0) = a
        //a = 2/t*(deltaX/t - v0)
        Vector3 acceleration = 2 / time * (displacement / time - initialVelocity);

        return acceleration;
    }
}
