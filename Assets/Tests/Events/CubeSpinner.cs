using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpinner : MonoBehaviour
{
    private Quaternion TargetRotation;

    public void Start()
    {
        TargetRotation = this.transform.rotation;
    }

    public void Update()
    {
        var currentRotation = this.transform.rotation;
        if (currentRotation == TargetRotation) return;
        
        this.transform.rotation = Quaternion.Slerp(currentRotation, TargetRotation, Time.deltaTime);
    }

    public void SpinCube()
    {
        TargetRotation = Random.rotation;
    }
}