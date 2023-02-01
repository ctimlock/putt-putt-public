using System;
using Unity.Netcode;
using Utility.Serialisation;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct PlayerState : INetworkSerializable, IEquatable<PlayerState>
{
    public ulong ClientId;
    public FixedString128Bytes Name;
    public bool LobbyReady;
    public bool HasControl;
    public SerialisableColor Colour;

    public PlayerState(string name)
    {
        this.ClientId = 0;
        this.Name = new FixedString128Bytes(name);
        this.LobbyReady = false;
        this.HasControl = false;
        this.Colour = Color.black;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref Name);
        serializer.SerializeValue(ref LobbyReady);
        serializer.SerializeValue(ref HasControl);
        serializer.SerializeValue(ref Colour);
    }

    public bool Equals(PlayerState other)
    {
        return ClientId.Equals(other.ClientId);
    }
}
