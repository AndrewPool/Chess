using System;
using System.Collections.Generic;
using UnityEngine;


public struct SmartSquare
{
    public readonly bool isEmpty;
    public readonly Unit unit;//these bytes might be bad

    //these could be arraylists.. idk what that means in C# though
    public Location[] moves;
    public Location[] movesTo;//{ get; private set; }// = new List<byte>();



    //public SmartSquare Copy()
    //{
    //    return new SmartSquare(unit.player, unit.token);
    //}

    //manually resizing arrays is fun
    public void AddToMoves(Location square)
    {
        Location[] temp = new Location[moves.Length + 1];
        for (int i = 0; i < moves.Length; i++)
        {
            temp[i] = moves[i];
        }
        temp[temp.Length - 1] = square;
        moves = temp;
    }

    public void AddToMovesTo(Location square)
    {
        Location[] temp = new Location[movesTo.Length + 1];
        for (int i = 0; i < movesTo.Length; i++)
        {
            temp[i] = movesTo[i];
        }
        temp[temp.Length - 1] = square;
        movesTo = temp;
    }

    //can't have anything nice, stupid squares
    public SmartSquare(bool player = true, Token token = Token.None)//it can't have a default
    {
        unit = new Unit(player, token);
        if(unit.token == Token.None) {
            isEmpty = true;
        }
        else { isEmpty = false; }

        moves = new Location[0];
        movesTo = new Location[0];

    }
    public SmartSquare ConvertToQueen()
    {
        return new SmartSquare(moves, movesTo, new Unit(unit.player, Token.Queen));
    }
    private SmartSquare(Location[] moves, Location[] movesTo, Unit unit)
    {
        isEmpty = false;
        this.moves = moves;
        this.movesTo = movesTo;
        this.unit = unit;
    }

    public static SmartSquare[,] StandardBoardSetUp()
    {
        return new SmartSquare[8,8]
        {
             { new SmartSquare(true, Token.Rook), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Rook) }
        };
    }
    
}

public struct Unit
{
    public readonly bool player;
    public readonly Token token;

    public Unit(bool player, Token token)
    {
        this.player = player;
        this.token = token;
    }
  
}