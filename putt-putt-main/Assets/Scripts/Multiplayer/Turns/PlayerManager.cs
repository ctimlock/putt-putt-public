using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Netcode;

public class PlayerManager : MonoBehaviour
{
    public ulong CurrentPlayerId;
    public PlayerColourManager PlayerColourManager;
    private Dictionary<ulong, Player> PlayerDatasByPlayerId = new Dictionary<ulong, Player>();
    public GameEventListener OnClientConnected;
    public GameEventListener OnClientDisconnected;

    public void OnEnable()
    {
        UpdatePlayersFromPlayerData();
    }

    public void UpdatePlayersFromPlayerData()
    {
        var clients = NetworkMultiplayerManager.Singleton.LobbyPlayerStates;
        var playersToRemoveIds = PlayerDatasByPlayerId.Keys.ToList();
        var playerDatasToAdd = new List<Player>();

        foreach (var client in clients)
        {
            var clientId = client.Key;

            if (playersToRemoveIds.Contains(clientId))
            {
                playersToRemoveIds.Remove(clientId);
                continue;
            }

            // playerDatasToAdd.Add(); TODO: Refactor for monobehaviours
        }

        foreach (var playerId in playersToRemoveIds)
        {
            RemovePlayer(playerId);
        }

        foreach (var player in playerDatasToAdd)
        {
            // playerDatasToAdd.Add(); TODO: Refactor for monobehaviours
        }
    }

    public void AddPlayer(Player player)
    {
        PlayerDatasByPlayerId.Add(player.PlayerState.PlayerId, player);
    }

    public void RemovePlayer(ulong playerId)
    {
        PlayerDatasByPlayerId.Remove(playerId);
    }

    public void NextTurn()
    {
        PlayerDatasByPlayerId.TryGetValue(CurrentPlayerId, out var lastPlayer);
        lastPlayer?.EndTurn();

        var playerIds = PlayerDatasByPlayerId.Keys.ToList();
        var nextPlayerId = playerIds.GetNext(CurrentPlayerId);

        var currentPlayer = PlayerDatasByPlayerId[nextPlayerId];

        CurrentPlayerId = nextPlayerId;
        currentPlayer.StartTurn();
    }
}
