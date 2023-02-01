using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel
{
    public WheelCollider Collider;
    public GameObject MeshObject;
    public bool CanTurn;

    public Wheel(WheelCollider collider, GameObject meshobject, bool canTurn = false)
    {
        Collider = collider;
        MeshObject = meshobject;
        CanTurn = canTurn;
    }
}
