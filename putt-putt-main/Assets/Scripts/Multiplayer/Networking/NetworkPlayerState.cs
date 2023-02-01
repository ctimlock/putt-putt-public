using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Utility.Serialisation;
using Unity.Collections;

public class NetworkPlayerState : PlayerStateOld, INetworkSerializable, IEquatable<PlayerStateOld>
{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PlayerId);
        serializer.SerializeValue(ref Name);
        serializer.SerializeValue(ref TurnOrder);
        serializer.SerializeValue(ref LobbyReady);
    }

    public bool Equals(PlayerStateOld other)
    {
        return PlayerId.Equals(other.PlayerId);
    }
}
