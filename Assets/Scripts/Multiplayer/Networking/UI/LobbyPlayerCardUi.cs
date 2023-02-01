using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using System.Linq;

public class LobbyPlayerCardUi : MonoBehaviour
{
    public int CardIndex;
    public Image Image;
    public TMP_Text Text;
    public TMP_Text Number;
    public Button ChangeColourButton;

    public void Refresh()
    {
        var hasClient = NetworkMultiplayerManager.Singleton.TryGetPlayerDataAt(CardIndex, out var clientState);

        if (!hasClient)
        {
            Image.color = Color.white;
            Text.text = "Waiting...";
            ChangeColourButton.interactable = false;
            return;
        }
        
        var playerState = clientState.Value;
        Image.color = playerState.Colour;
        Text.text = playerState.Name.ToString();
        Number.text = (CardIndex + 1).ToString();

        var localClientId = NetworkMultiplayerManager.Singleton.LocalPlayerState.ClientId;
        if (localClientId != playerState.ClientId) return;

        ChangeColourButton.interactable = true;
    }

    public void ChooseNewRandomColour()
    {
        var hasClient = NetworkMultiplayerManager.Singleton.TryGetPlayerDataAt(CardIndex, out var clientState);

        if (!hasClient) return;

        NetworkMultiplayerManager.Singleton.ChooseNewRandomColourServerRpc(clientState.Value);
    }
}
