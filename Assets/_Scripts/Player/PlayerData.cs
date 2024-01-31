using System;

public struct PlayerData : IEquatable<PlayerData>
{
    public ulong ClientId;

    public bool Equals(PlayerData other)
    {
        return ClientId == other.ClientId;
    }
}
