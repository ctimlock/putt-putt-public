using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Utility.Serialisation;
using System.Linq;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerColourManager))]
public class NetworkMultiplayerManager : NetworkBehaviour
{
    private static NetworkMultiplayerManager singleton;
    public static NetworkMultiplayerManager Singleton { get => singleton; }
    public PlayerState LocalPlayerState;
    public Dictionary<ulong, PlayerState> LobbyPlayerStates { get; private set; }
    public GameEvent OnClientsUpdatedEvent;
    public PlayerColourManager PlayerColourManager;

    private string LobbySceneReference = "MultiplayerLobby";

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;
        DontDestroyOnLoad(gameObject);

        LobbyPlayerStates = new Dictionary<ulong, PlayerState>();

        PlayerColourManager = this.GetComponent<PlayerColourManager>();
    }

    public void OnEnable()
    {
        if (!NetworkManager.Singleton) return;

        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    public void OnDisable()
    {
        if (!NetworkManager.Singleton) return;

        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    private void OnServerStarted()
    {
        if (!NetworkManager.Singleton.IsHost) return;

        LocalPlayerState.ClientId = NetworkManager.Singleton.LocalClientId;

        UpsertPlayerStateServerRpc(LocalPlayerState);
    }

    private void OnClientConnected(ulong clientId)
    {     
        if (NetworkManager.Singleton.IsHost || clientId != NetworkManager.Singleton.LocalClientId) return;

        LocalPlayerState.ClientId = clientId;

        UpsertPlayerStateServerRpc(LocalPlayerState);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        LobbyPlayerStates.Remove(clientId);

        var lobbyPlayerStates = LobbyPlayerStates.Values.ToArray();
        UpdatePlayerStatesClientRpc(lobbyPlayerStates);
    }

    public void ConnectAsHost(string playerName)
    {
        InitialiseLocalPlayerState(playerName);
        NetworkManager.Singleton.StartHost();

        var status = NetworkManager.SceneManager.LoadScene(LobbySceneReference, LoadSceneMode.Single);
    }

    public void ConnectAsClient(string playerName)
    {
        InitialiseLocalPlayerState(playerName);
        NetworkManager.Singleton.StartClient();
    }

    private void InitialiseLocalPlayerState(string playerName)
    {
        LocalPlayerState = new PlayerState(playerName);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChooseNewRandomColourServerRpc(
        PlayerState newPlayerState, ServerRpcParams serverRpcParams = default
    )
    {
        if (newPlayerState.ClientId != serverRpcParams.Receive.SenderClientId) return;

        newPlayerState = PlayerColourManager.ChooseRandomColour(newPlayerState);

        UpsertPlayerStateForClients(newPlayerState);
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpsertPlayerStateServerRpc(PlayerState newPlayerState)
    {
        if (!LobbyPlayerStates.ContainsKey(newPlayerState.ClientId))
        {
            newPlayerState = PlayerColourManager.ChooseRandomColour(newPlayerState);
        }

        UpsertPlayerStateForClients(newPlayerState);
    }

    private void UpsertPlayerStateForClients(PlayerState newPlayerState)
    {
        LobbyPlayerStates[newPlayerState.ClientId] = newPlayerState;

        var lobbyPlayerStates = LobbyPlayerStates.Values.ToArray();

        UpdatePlayerStatesClientRpc(lobbyPlayerStates);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestPlayerStateSyncServerRpc()
    {
        var lobbyPlayerStates = LobbyPlayerStates.Values.ToArray();
        UpdatePlayerStatesClientRpc(lobbyPlayerStates);
    }

    [ClientRpc]
    private void UpdatePlayerStatesClientRpc(PlayerState[] playerStatesToSync)
    {
        var localClientsToSyncIds = LobbyPlayerStates.Keys.ToList();

        foreach (var state in playerStatesToSync)
        {
            var clientId = state.ClientId;
            LobbyPlayerStates[clientId] = state;
            localClientsToSyncIds.Remove(clientId);
        }

        LocalPlayerState = LobbyPlayerStates[NetworkManager.Singleton.LocalClientId];

        foreach (var clientId in localClientsToSyncIds)
        {
            LobbyPlayerStates.Remove(clientId);
        }

        OnClientsUpdatedEvent.Raise();
    }

    public bool TryGetPlayerDataAt(int clientIndex, out PlayerState? playerData)
    {
        try
        {
            var clientId = LobbyPlayerStates.ElementAt(clientIndex).Value.ClientId;
            return TryGetPlayerData(clientId, out playerData);
        }
        catch
        {
            playerData = null;
            return false;
        }
    }

    public bool TryGetPlayerData(ulong clientId, out PlayerState? playerState)
    {
        try
        {
            playerState = LobbyPlayerStates[clientId];
            return true;
        }
        catch
        {
            playerState = null;
            return false;
        }
    }
}
