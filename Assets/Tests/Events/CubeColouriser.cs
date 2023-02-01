using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CubeColouriser : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public List<Material> Materials;

    public void Awake()
    {
        MeshRenderer = this.GetComponent<MeshRenderer>();
    }

    public void Start()
    {
        MeshRenderer.material = Materials[0];
    }

    public void SwitchMaterial()
    {
        MeshRenderer.material = Materials.GetRandom();
    }
}
