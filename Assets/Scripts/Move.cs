using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public readonly Location from;
    public readonly Location to;
    public readonly MoveType moveType;

    public Move(Location from, Location to)
    {
        this.from = from;
        this.to = to;
        this.moveType = MoveType.Standard;
    }
    public Move(Location from, Location to, MoveType type)
    {
        this.from = from;
        this.to = to;
        this.moveType = type;
    }

    public override int GetHashCode()
    {
        return ((from.row * 8 + from.column)*10000) + ((to.row * 8) + to.column);
    }
    
    public override bool Equals(object other)
    {
        if (other == null) return false;
        if (this == other) return true;
        Move otherMove = (Move)other;//cast as a move
        if (from.Equals(otherMove.from) && to.Equals(otherMove.to)) return true;
        return false;
    }
}
public enum MoveType
{
    Standard,
    CastleShort,
    CastleLong,


}

