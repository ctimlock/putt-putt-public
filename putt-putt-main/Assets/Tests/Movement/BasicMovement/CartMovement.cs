using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class CartMovement : MonoBehaviour
{
    [SerializeField] bool HaltMovement;

    [Header("Acceleration Movemment")]
    [SerializeField] float CartSpeed;
    [SerializeField] float AccelerationMaxSpeed;
    [SerializeField] float AccelerationDrag;

    [Header("Pulse Movemment")]
    [SerializeField] bool PulseMovement;
    [SerializeField] float PulseTopSpeed;
    [SerializeField] float MinimumSpeedTriggerBoostThreshhold;
    [SerializeField] float PulseMaxSpeed;

    [Header("Movemment Settings")]
    [SerializeField] float Veloctiy;
    [SerializeField] float InputAcceleration;
    [SerializeField] Rigidbody RigidbodyCart;
    [SerializeField] BoxCollider ColliderCart;

    //Private Variables
    float InputSteering;
    public float RawSteeringInput;
    public float SteeringDrag = 1f;
    public float RawAccelerationInput;
    bool PulseAccelertationRoutineRunning = false;
    IEnumerator PulseRoutine;
    bool CartGrounded;
    float GroundCheckDistance;

    private void Start()
    {
        if (ColliderCart == null)
        {
            try
            {
                GetComponent<BoxCollider>();
            }
            catch (System.Exception)
            {
                Debug.LogError($"Collider required for {this.name} Object");
            }
        }

        if (RigidbodyCart == null)
        {
            try
            {
                GetComponent<Rigidbody>();
            }
            catch (System.Exception)
            {
                Debug.LogError($"Rigidbody required for {this.name} Object");
            }
        }
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    {
        InputPreProcessing();
        if (!HaltMovement)
        {
            if (!CartGrounded) ForwardAcceleration();
            TurningSettings();
        }
        KeepGrounded();
    }

    private void KeepGrounded() // Split into own cs script
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (GroundCheckDistance == 0) GroundCheckDistance = ColliderCart.bounds.extents.y + 0.01f;
        CartGrounded = Physics.Raycast(ray, out hit, GroundCheckDistance);
    }

    private void InputPreProcessing()
    {
        AccelerationPreProcessing();
        TurningPreProcessing();
    }

    private void TurningPreProcessing()
    {
        RawSteeringInput = Mathf.Lerp((RawSteeringInput), 0, (SteeringDrag * Time.deltaTime));
        InputSteering = RawSteeringInput;
    }

    private void AccelerationPreProcessing()
    {
        RawAccelerationInput = Mathf.Lerp((RawAccelerationInput), 0, (AccelerationDrag * Time.deltaTime));
        InputAcceleration = RawAccelerationInput;
    }

    private void ForwardAcceleration()
    {
        if (PulseMovement) PulseForwardAcceleration();
        else ConstantForwardAcceleration();
    }

    private void PulseForwardAcceleration()
    {
        if (PulseRoutine == null) PulseRoutine = PulseAccelerationRoutine();

        if (PulseAccelertationRoutineRunning == false)
        {
            StopCoroutine(PulseRoutine);
            if (RigidbodyCart.velocity.magnitude < MinimumSpeedTriggerBoostThreshhold)
            {
                PulseAccelertationRoutineRunning = true;
                if (!HaltMovement) StartCoroutine(PulseAccelerationRoutine());
            }
        }
    }

    private void TurningSettings()
    {
        RigidbodyCart.transform.Rotate(new Vector3(0, InputSteering, 0));
    }

    IEnumerator PulseAccelerationRoutine()
    {
        var posUpdate = RigidbodyCart.transform.right * PulseTopSpeed;
        if (RigidbodyCart.velocity.magnitude > AccelerationMaxSpeed)
        {
            posUpdate = Vector3.ClampMagnitude(posUpdate, AccelerationMaxSpeed);
        }
        RigidbodyCart.AddForce(posUpdate, ForceMode.Impulse);
        Veloctiy = RigidbodyCart.velocity.magnitude;
        PulseAccelertationRoutineRunning = false;
        yield return null;
    }

    private void ConstantForwardAcceleration()
    {
        var posUpdate = RigidbodyCart.transform.right * (CartSpeed * InputAcceleration);
        if (RigidbodyCart.velocity.magnitude > AccelerationMaxSpeed)
        {
            posUpdate = Vector3.ClampMagnitude(posUpdate, AccelerationMaxSpeed);
        }
        RigidbodyCart.AddForce(posUpdate, ForceMode.Acceleration);
        Veloctiy = RigidbodyCart.velocity.magnitude;
    }
}