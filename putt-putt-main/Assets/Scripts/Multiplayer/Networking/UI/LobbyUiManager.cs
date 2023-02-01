using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class LobbyUiManager : MonoBehaviour
{
    public TMP_InputField PlayerName;
    public Button StartGameButton;
    public LobbyPlayerCardUi[] PlayerCards;

    public void Awake()
    {
        if (!NetworkManager.Singleton.IsServer) return;
        
        StartGameButton.enabled = true;
    }

    public void Start()
    {
        UpdatePlayerCardUi();
    }

    public void UpdatePlayerCardUi()
    {
        foreach (var card in PlayerCards)
        {
            card.Refresh();
        }

        if (StartGameButton.enabled)
        {
            StartGameButton.interactable = NetworkMultiplayerManager.Singleton.LobbyPlayerStates.Count > 1;
        }
    }
}
