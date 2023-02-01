using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuUi : MonoBehaviour
{
    public TMP_InputField PlayerName;
    public Button HostJoinButton;
    public Button ClientJoinButton;

    public void OnEnable()
    {
        PlayerName.onValueChanged.AddListener(OnPlayerNameInputChanged);
    }

    public void OnPlayerNameInputChanged(string newValue)
    {
        if (newValue == null || newValue == "")
        {
            HostJoinButton.interactable = false;
            ClientJoinButton.interactable = false;
            return;
        }

        HostJoinButton.interactable = true;
        ClientJoinButton.interactable = true;
    }

    public void JoinGameAsHost()
    {
        var name = PlayerName.text;
        NetworkMultiplayerManager.Singleton.ConnectAsHost(name);
    }

    public void JoinGameAsClient()
    {
        var name = PlayerName.text;
        NetworkMultiplayerManager.Singleton.ConnectAsClient(name);
    }
}
