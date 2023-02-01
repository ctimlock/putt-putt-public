using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateOld PlayerState;
    private PlayerColouriser PlayerColouriser;
    public PlayerManager PlayerManager;

    public void Awake()
    {
        PlayerColouriser = this.GetComponent<PlayerColouriser>();
    }

    public void Start()
    {
        // var playerColour = PlayerManager.PlayerColourManager.ChooseRandomColour(null);
        // PlayerColouriser.SetDefaultPlayerColour(playerColour);
    }

    public void Update()
    {
        if (!PlayerState.HasControl) return;
    }

    /// <summary>
    /// Start the players turn
    /// </summary>
    public void StartTurn()
    {
        PlayerState.HasControl = true;
        var activePlayerColour = PlayerManager.PlayerColourManager.ActivePlayerColour;

        PlayerColouriser.SetPlayerColour(activePlayerColour);
    }

    /// <summary>
    /// End the players turn
    /// </summary>
    public void EndTurn()
    {
        PlayerState.HasControl = false;

        PlayerColouriser.ResetPlayerColour();
    }
}
