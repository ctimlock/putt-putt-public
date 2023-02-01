using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartFollower : MonoBehaviour
{
    private Vector3 DistanceToKeep;

    public Transform ToFollow;
    public float FollowSpeed = 10;

    void Awake()
    {
        if (ToFollow == null) Debug.LogWarning("You must assign a transform to follow for this object to move");

        DistanceToKeep = ToFollow.transform.position - this.transform.position;
    }

    void FixedUpdate()
    {
        var targetPosition = ToFollow.transform.position - DistanceToKeep;
        var currentPostion = this.transform.position;

        if (Vector3.Distance(currentPostion, targetPosition) < 0.01f) return;

        this.transform.position = Vector3.Lerp(currentPostion, targetPosition, FollowSpeed * Time.deltaTime); 
    }
}
