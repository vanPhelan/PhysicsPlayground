using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class ProjectileBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private ConstantForce _constantForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _constantForce = GetComponent<ConstantForce>();
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    public ConstantForce GetConstantForce()
    {
        return _constantForce;
    }
}
