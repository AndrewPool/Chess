using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Unit
{
    public readonly bool player;
    public readonly Token token;

    public Unit(bool player, Token token)
    {
        this.player = player;
        this.token = token;
    }

    /// <summary>
    /// refer to documentation
    /// </summary>
    /// <returns>int</returns>
    public int HashValue()
    {
        switch (token)
        {//the complier does this multiplication that is why it is here
           //case Token.None: return 0;
            case Token.Pawn: if (player) { return 1 * 64; } return 12 * 64;
            case Token.Knight: if (player) { return 2 * 64; } return 7 * 64;
            case Token.Bishop: if (player) { return 3 * 64; } return 8 * 64;
            case Token.Rook: if (player) { return 4 * 64; } return 9 * 64;
            case Token.Queen: if (player) { return 5 * 64; } return 10 * 64;
            case Token.King: if (player) { return 6 * 64; } return 11 * 64;

        }
        return 0;
    }

}