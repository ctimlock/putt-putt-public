using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerColourManager : MonoBehaviour
{
    public Color ActivePlayerColour = Color.green;
    public Color[] ColourChoices = new Color[] { Color.blue, Color.red };
    private List<Color> AvailableColours;

    public void Awake()
    {
        AvailableColours = ColourChoices.ToList();
    }

    /// <summary>
    /// Return your existing colour to the pool, and choose a new one
    /// </summary>
    public void ChooseColour(Color? currentColour, Color newColour)
    {
        throw new NotImplementedException("Choose Colour not implemented!");
    }

    /// <summary>
    /// Return your existing colour to the pool, and choose a new random one for this player
    /// </summary>
    public PlayerState ChooseRandomColour(PlayerState player)
    {
        var newColour = AvailableColours.GetRandom();
        if (newColour == null) Debug.LogError("Not enough available player colours!");

        AvailableColours.Remove(newColour);

        if (ColourChoices.Contains(player.Colour))
        {
            AvailableColours.Add(player.Colour);
        }

        player.Colour = newColour;

        return player;
    }
}
