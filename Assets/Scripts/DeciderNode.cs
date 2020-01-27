using System;

using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a node for the chess tree
/// </summary>
public class DeciderNode : ITraversable, IEquatable<DeciderNode>
{

    // public int score{ get { return board.score; } }

    public int HeapScore
    {
        get
        {
         //   return board.Score;
            return Math.Abs(board.Score);
        }
    }

    public bool IsLeaf { get; private set; }

    public readonly ChessBoard board;

    private readonly DeciderNode from;

    /// <summary>
    /// this can now be null able on mate, this is your mate check.
    /// </summary>
    public IDictionary<Move, ITraversable> to { get; private set; }

    public bool WhiteKingHasMoved { get; private set; }

    public bool BlackKingHasMoved { get; private set; }

    public readonly bool poison;//very* inside joke

    public bool Player { get; private set; }


    /// <summary>
    /// for setting up a new game
    /// </summary>
    /// <param name="board"></param>
    /// <param name="from"></param>
    public DeciderNode(SmartSquare[,] board, DeciderNode from = null)
    {
        Player = true;
        WhiteKingHasMoved = false;
        BlackKingHasMoved = false;
        poison = false;
        this.board = new ChessBoard(board: board);
        this.from = from;
        to = null;
        IsLeaf = true;

    }
    //this is internally built
    public DeciderNode(ChessBoard board, DeciderNode from)
    {
        Player = !from.Player;
        WhiteKingHasMoved = from.WhiteKingHasMoved;
        BlackKingHasMoved = from.BlackKingHasMoved;
        this.board = board;
        this.from = from;
        to = null;
        IsLeaf = true;

    }


    public void ConvertToTuber()
    {
        to = null;
    }
    /// <summary>
    /// Be aware that this also clears the moves list on all the squares on completion, to save space...
    /// this can be updated later create a different square type without the pointers. idk, that's a lot of work, not a lot of pay off.
    /// </summary>
    /// 
    public void SetMovesTo()
    {
        //  Debug.Log("making moves smart");
        int count = 0;
        to = new Dictionary<Move, ITraversable>();

        if(board.BlackWon || board.WhiteWon)
        {

            

            return;
        }
      //  Debug.Log(Player);
        //check to see if the kings have moved
        if (WhiteKingHasMoved == false && board.board[0, 4].unit.token != Token.King) WhiteKingHasMoved = true;

        if (BlackKingHasMoved == false && board.board[7, 4].unit.token != Token.King) BlackKingHasMoved = true;
       
        //cycle through the spaces...
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                //if they are the current player, that was passed
                if (board.board[row, col].unit.player == Player)
                {

                    //cycles all the moves
                   // Debug.Log(board.board[row, col].moves.Length);
                    //this is terrible but i'm not sure if it's worse than resizing arrays...
                    for (int i = 0; i < board.board[row, col].moves.Length; i++)
{
                        
                        Move newMove = new Move(new Location(row, col), board.board[row, col].moves[i]);
                        DeciderNode newNode = new DeciderNode(new ChessBoard(board.board, newMove), this);



                        //this keeps you from making a move that would put you in check
                        //this would work better with a functional paradigm assigning a check func at player aloc
                        if ((!newNode.board.WhiteInCheck && Player) || (!newNode.board.BlackInCheck && !Player))
                        {
                            //i should not need this test, but also i don't know why i need it

                            if (to.ContainsKey(newMove))
                            {
                                to[newMove] = newNode;
                            }
                            else
                            {
                                to.Add(newMove, newNode);
                            }
                            count++;
                        }


                    }


                }
                //and then null the list to save just a little bit of space
                //  board.board[row, col].moves = null;
            }
        }

        //does the stuff for castleing
        if (WhiteKingHasMoved == false && Player) WhiteCastleMoveCheck();
        if (BlackKingHasMoved == false && !Player) BlackCastleMoveCheck();

        //  Debug.Log(count + " moves smarter");
        IsLeaf = false;

       board.DisposeOfResources();

    }

    private void BlackCastleMoveCheck()
    {
        bool castle = true;

        //to the left short

        for (int i = 5; i < 7; i++)
        {
            if (board.board[7, i].isEmpty == true)
            {
                foreach (Location loc in board.board[7, i].movesTo)
                {
                    if (board.board[loc.row, loc.column].unit.player == true)
                    {
                        castle = false;
                        i = 7;
                    }
                }
            }
            else
            {
                castle = false;
                i = 7;
            }
        }

        if (castle && board.board[7, 7].unit.token == Token.Rook && board.board[7, 7].unit.player == false)
        {

            Move newMove = new Move(new Location(7, 4), new Location(7, 6), MoveType.CastleShort);
            DeciderNode newNode = new DeciderNode(new ChessBoard(board.board, newMove), this);
            to.Add(newMove, newNode);
        }

        castle = true;
        //to the right long
        for (int i = 3; i > 0; i--)
        {
            if (board.board[7, i].isEmpty == true)
            {
                foreach (Location loc in board.board[7, i].movesTo)
                {
                    if (board.board[loc.row, loc.column].unit.player == true)
                    {
                        castle = false;
                        i = 0;
                    }
                }
            }
            else
            {
                castle = false;
                i = 0;
            }
        }

        if (castle && board.board[7, 0].unit.token == Token.Rook && board.board[7, 0].unit.player == false)
        {
            Move newMove = new Move(new Location(7, 4), new Location(7, 2), MoveType.CastleLong);
            DeciderNode newNode = new DeciderNode(new ChessBoard(board.board, newMove), this);
            to.Add(newMove, newNode);
        }


    }

    private void WhiteCastleMoveCheck()
    {
        bool castle = true;

        //to the right short

        for (int i = 5; i < 7; i++)
        {
            if (board.board[0, i].isEmpty == true)
            {
                foreach (Location loc in board.board[0, i].movesTo)
                {
                    if (board.board[loc.row, loc.column].unit.player == false)
                    {
                        castle = false;
                        i = 7;
                    }
                }
            }
            else
            {
                castle = false;
                i = 7;
            }
        }
        if (castle && board.board[0, 7].unit.token == Token.Rook && board.board[0, 7].unit.player == true)
        {

            Move newMove = new Move(new Location(0, 4), new Location(0, 6), MoveType.CastleShort);
            DeciderNode newNode = new DeciderNode(new ChessBoard(board.board, newMove), this);
            to.Add(newMove, newNode);
        }

        castle = true;
        //to the left long
        for (int i = 3; i > 0; i--)
        {
            if (board.board[0, i].isEmpty == true)
            {
                foreach (Location loc in board.board[0, i].movesTo)
                {
                    if (board.board[loc.row, loc.column].unit.player == false)
                    {
                        castle = false;
                        i = 0;
                    }
                }
            }

            else
            {
                castle = false;
                i = 0;
            }
        }

        if (castle && board.board[0, 0].unit.token == Token.Rook && board.board[0, 0].unit.player == true)
        {

            Move newMove = new Move(new Location(0, 4), new Location(0, 2), MoveType.CastleLong);
            DeciderNode newNode = new DeciderNode(new ChessBoard(board.board, newMove), this);
            to.Add(newMove, newNode);
        }

    }

    public ITraversable From()
    {
        return from;
    }

    public ICollection<ITraversable> ToNodes()
    {
        return to.Values;
    }


    public bool Equals(DeciderNode other)
    {
        if (other == null) return false;
        if (this == other) return true;

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (other.board.board[row, col].unit.player != board.board[row, col].unit.player || other.board.board[row, col].unit.token != board.board[row, col].unit.token)
                {
                    return false;

                }
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        return board.Hash;
    }


    public void AddNodesToTreeRecursivly(IDictionary<DeciderNode, Empty> onlyKeys)
    {
        onlyKeys.Add(this, new Empty());
        if (!IsLeaf)
        {
            foreach (DeciderNode node in to.Values)
            {
                node.AddNodesToTreeRecursivly(onlyKeys);
            }


        }
       
    }
}
