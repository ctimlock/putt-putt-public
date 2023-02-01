using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] List<WheelCollider> WheelColliders;
    [SerializeField] List<WheelCollider> TurningWheels;
    [SerializeField] List<GameObject> WheelMeshes;
    [SerializeField] [Range(-1,1)] float CurrentTurnInput = 0f;
    [SerializeField] float MaxTurnAngle = 15f;
    [SerializeField] float MinTurnAngle = 5f;
    [SerializeField] float MaxTurnSpeed = 15f;
    [SerializeField] float SpeedDampeningFactor = 2f;
    [SerializeField] float DownhillDegreeLimit = 3f;
    public float BrakePower;
    public bool AreBrakesEnabled = false;
    private List<Wheel> Wheels;
    private Rigidbody Cart;
    private float CurrentCartAngle;
    private float CurrentTurnAngle;
    private bool IsFacingDownhill { get { return CurrentCartAngle < 270 && CurrentCartAngle > DownhillDegreeLimit;}}
    private bool AreBrakesApplied { get { return !IsFacingDownhill && AreBrakesEnabled; }}

    void Awake()
    {
        // Get the cart's Rigidbody
        Cart = WheelColliders.First().attachedRigidbody;


        // Create the set of Wheels by linking the mesh to the collider, and identifying wheels used for turning
        Wheels = new();
        foreach (var collider in WheelColliders)
        {
            var mesh = WheelMeshes.FirstOrDefault(mesh => mesh.name == collider.name);
            var canTurn = TurningWheels.Exists(tWheel => tWheel.name == collider.name);

            Wheels.Add(new Wheel(collider, mesh, canTurn));
        }
    }

    private void FixedUpdate()
    {
        // Update cart's vertical angle
        CurrentCartAngle = Cart.transform.rotation.eulerAngles.x;

        // Apply negative torque if brakes are applied 
        Wheels.ForEach((wheel) => wheel.Collider.brakeTorque = AreBrakesApplied ? BrakePower : 0f);

        // Update the current maximum turn angle given current velocity
        CurrentTurnAngle = GetCurrentMaxTurnAngle() * CurrentTurnInput;

        // Apply steering to turnable wheels
        foreach (var wheel in Wheels.Where(wheel => wheel.CanTurn))
        {
            wheel.Collider.steerAngle = CurrentTurnAngle;
        }

        // Update the corresponding mesh for each collider
        Wheels.ForEach((wheel) => UpdateWheel(wheel.Collider, wheel.MeshObject.transform));

    }

    public List<WheelCollider> GetWheels()
    {
        return this.WheelColliders;
    }

    void UpdateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        var offset = Vector3.left * collider.center.x;

        transform.position = position;
        transform.rotation = rotation;
        transform.localPosition += offset;
    }

    float GetCurrentMaxTurnAngle()
    {
        var velocityPercentage = Mathf.Clamp(Cart.velocity.z, 0, MaxTurnSpeed)/MaxTurnSpeed;

        var turnRange = MaxTurnAngle - MinTurnAngle;

        return MaxTurnAngle - (turnRange * Mathf.Pow(velocityPercentage, SpeedDampeningFactor));
    }
}