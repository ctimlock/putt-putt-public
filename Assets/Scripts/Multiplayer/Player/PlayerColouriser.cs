using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerColouriser : MonoBehaviour
{
    private Color PlayerColour;
    private Renderer Renderer;

    void Awake()
    {
        Renderer = this.GetComponent<Renderer>();
    }

    /// <summary>
    /// Set the players default colour
    /// </summary>
    public void SetDefaultPlayerColour(Color defaultColour)
    {
        PlayerColour = defaultColour;
        Renderer.material.SetColor("_Color", defaultColour);
    }

    /// <summary>
    /// Set the players colour to a specific colour
    /// </summary>
    public void SetPlayerColour(Color newColour)
    {
        Renderer.material.SetColor("_Color", newColour);
    }

    /// <summary>
    /// Reset the players colour to default
    /// </summary>
    public void ResetPlayerColour()
    {
        Renderer.material.SetColor("_Color", PlayerColour);
    }
}
