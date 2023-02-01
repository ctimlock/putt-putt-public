using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera PlayerCameraMain;
    [SerializeField] bool EnableSpeedingZoomBack = true;
    [SerializeField] int MaxRangeModifierFromCamera = 50;
    [SerializeField] int CartMaxSpeed = 30; //NEEDS to be added through cart manager 
    [SerializeField] Vector3 MinimumCameraOffset = new Vector3(0, 10, -30);
    [SerializeField] float MinmumRearTrackingRange = 15;
    [SerializeField] float MinmumHeightTrackingRange = 10;
    [Range(0, 10)] public float FollowSpeed = 0.25f;
    [Range(0, 10)] public float MaxCameraFollowSpeed = 0.25f;
    Vector3 Direction;
    [SerializeField] float MaximumCameraRearOffset = 45;
    [SerializeField] float MaximumCameraHeightOffset = 20;
    float Radius;
    [SerializeField] float Velocity;
    Rigidbody TrackingRigidbody;

    void Start()
    {
        if (TrackingRigidbody == null) TrackingRigidbody = GetComponent<Rigidbody>();
        if (PlayerCameraMain == null) PlayerCameraMain = Camera.main;
        Direction = (PlayerCameraMain.transform.position - transform.position).normalized;
        Radius = Vector3.Distance(PlayerCameraMain.transform.position, transform.position);
    }

    void FixedUpdate()
    {
        Velocity = TrackingRigidbody.velocity.magnitude;
        CameraFollowGameObject();
    }

    void CameraFollowGameObject()
    {
        TrackingPosition();
        TrackingRotation();
    }

    private Vector3 SetZoomPosition()
    {
        var maxSpeedVelocityPercent = (Velocity / CartMaxSpeed);
        var offsetRear = MaximumCameraRearOffset * maxSpeedVelocityPercent > MinmumRearTrackingRange && EnableSpeedingZoomBack ? MaximumCameraRearOffset * maxSpeedVelocityPercent : MinmumRearTrackingRange;
        var offsetUp = MaximumCameraHeightOffset * maxSpeedVelocityPercent > MinmumHeightTrackingRange && EnableSpeedingZoomBack ? MaximumCameraHeightOffset * maxSpeedVelocityPercent : MinmumHeightTrackingRange;
        var cameraOffsetPostion = transform.position + (-transform.right * offsetRear);
        cameraOffsetPostion += Vector3.up * offsetUp;
        return cameraOffsetPostion;
    }

    private void TrackingPosition()
    {
        var followSpeed = Time.smoothDeltaTime * FollowSpeed;
        var lerpSpeed = followSpeed * followSpeed * (3f - 2f * followSpeed);
        var posUpdate = Vector3.Lerp(SetZoomPosition(), PlayerCameraMain.transform.position, lerpSpeed);
        PlayerCameraMain.transform.position = posUpdate;
    }

    private void TrackingRotation()
    {
        PlayerCameraMain.transform.LookAt(transform.position);
    }
}
