using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float accelerationMagnitude = 0.5f;
    public float speed = 5.0f;
    public float jumpStrength = 10.0f;
    public float airControl = 1.0f;
    public float gravityModifier = 1.0f;
    public bool faceWithCamera = true;

    public Camera playerCamera;
    
    private CharacterController _controller;
    [SerializeField]
    private Animator _animator;

    private Vector3 _horizontalVelocity;
    private Vector3 _verticalVelocity;
    private bool _isGrounded = false;
    private float _deltaRotation = 0.0f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Get movement input
        float inputForward = Input.GetAxis("Vertical");
        float inputRight = Input.GetAxis("Horizontal");

        //Get camera forward vector
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0.0f;
        cameraForward.Normalize();
        //Get camera right vector
        Vector3 cameraRight = playerCamera.transform.right;

        //Get jump input
        bool jumpInput = Input.GetButtonDown("Jump");

        //Calculate acceleration
        Vector3 horizontalAcceleration = (cameraForward * inputForward) + (cameraRight * inputRight);
        horizontalAcceleration.Normalize();
        horizontalAcceleration *= accelerationMagnitude;

        //Accelerate
        _horizontalVelocity += horizontalAcceleration * Time.deltaTime;
        if (_horizontalVelocity.magnitude > speed)
        {
            _horizontalVelocity.Normalize();
            _horizontalVelocity *= speed;
        }

        //Decelerate
        if (horizontalAcceleration.magnitude == 0.0f)
        {
            Vector3 horizontalDeceleration = _horizontalVelocity.normalized * accelerationMagnitude;
            _horizontalVelocity -= horizontalDeceleration * Time.deltaTime;
            if (_horizontalVelocity.magnitude < accelerationMagnitude)
            {
                _horizontalVelocity = Vector3.zero;
            }
        }

        //Check for ground
        _isGrounded = _controller.isGrounded;

        //Apply jump strength
        if (jumpInput && _isGrounded)
        {
            _verticalVelocity.y = jumpStrength;
            jumpInput = false;
        }

        //Stop on ground
        if (_isGrounded && _verticalVelocity.y < 0.0f)
        {
            _verticalVelocity.y = -1.0f;
        }

        //Apply gravity
        _verticalVelocity += Physics.gravity * gravityModifier * Time.deltaTime;

        //Update animations
        if (faceWithCamera)
        {
            transform.forward = cameraForward;
            _animator.SetFloat("Speed", inputForward);
            _animator.SetFloat("Direction", inputRight);
        }
        else
        {
            if (_horizontalVelocity != Vector3.zero)
                transform.forward = _horizontalVelocity.normalized;
            _animator.SetFloat("Speed", _horizontalVelocity.magnitude / speed);
        }
        _animator.SetBool("Jump", !_isGrounded);
        _animator.SetFloat("VerticalSpeed", _verticalVelocity.y / jumpStrength);

        //Move
        _controller.Move((_horizontalVelocity + _verticalVelocity) * Time.deltaTime);
    }
}
