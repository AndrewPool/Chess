
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is how you do the chess game, if you will. this is the wrapper structure for the game tree,
/// it houses a root property as well as the public interface for the entire tree.
/// init one with a standard board set up, or any other setup, if you wish,
/// Then call Pick(Move)->DeciderNode to continue
///  it will grow a tree from there, pruning, and expanding depending on how good i am at coding.
/// </summary>
public struct Decider  {
    //this was tripping me up, not hidding the impementation...
    private readonly DeciderNode root;

    //player whose turn it is.
    public readonly bool player;
    /// <summary>
    /// you must give one of these to the PickFunction if you wish to use this datatype correctly, please don't mutate this dict...
    /// </summary>
    /// <returns>a valid move dictionary</returns>
    public ICollection<Move> Choices()
    {
        return root.to.Keys;
    }

    /// <summary>
    /// this isn't getting called for some reason... this is very strange.
    /// call before winner to avoid confusion
    /// </summary>
    /// <returns>true if game is over</returns>
    public bool Over()
    {
        Debug.Log("white won:" + root.board.whiteWon);
        Debug.Log("Black won:" + root.board.blackWon);
        if (root.board.whiteWon || root.board.blackWon)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// returns the bool cooresponding to the winner
    /// </summary>
    /// <returns>true for white, false for black.</returns>
    public bool Winner()
    {
        if (root.board.whiteWon)
        {
            return true;
        }
        return false;
    }

    public int ScoreForCurrentState()
    {
        return root.board.score;
    }

    public SmartSquare[,] GetCurrentState()
    {
        return root.board.board;
    }
    //this is neat...


    public SmartSquare SquareForCurrentIndex(int row, int col)
    {
        return root.board.board[row, col];
    }
    /// <summary>
    /// This has the chance of returning a null value in the case there are no moves, be preparded to catch i guess.
    /// </summary>
    /// <returns>the first move, because it's unimplemented</returns>
    public Move PickOneForMe()
    {

        HashSet<DeciderNode> nodesInTree = new HashSet<DeciderNode>();
        
        MaxHeap heap = new MaxHeap(root);
        if (root.to.Keys == null)
        {
            return null;
        }










        Move bestMove = new Move(new Location(-1,-1), new Location(-1,-1));
        bool first = true;
        foreach (Move move in root.to.Keys)
        {
            if (first)
            {
                bestMove = move;
                first = false;
            }
            else
            {
                
                    bestMove =BetterMove(bestMove,move);
               
            }
           
        }
        return bestMove;//there are 
    }
    private Move BetterMove(Move m1, Move m2)
    {
        var returnMove = m1;
        if (player)
        {
            if (((DeciderNode)root.to[m1]).board.score > ((DeciderNode)root.to[m2]).board.score)
            {
                returnMove = m2;
            }
        }
        else
        {
            if (((DeciderNode)root.to[m1]).board.score < ((DeciderNode)root.to[m2]).board.score)
            {
                returnMove = m2;
            }
        }
        return returnMove;
    }

    //-----------------------------inits are below, these could be overloaded...------------------------------------
    /// <summary>
    /// This is the main intended interface for Decider, make a new one! based on the current one's properties.
    /// </summary>
    /// <param name="move">this must be in the Choices.Key property</param>
    /// <returns>a new Decider, for sweet saftey and functionality</returns>
    public Decider Pick(Move move)
    {

        //DateTime startTime = System.DateTime.Now;

        //DateTime endTime = System.DateTime.Now;
        //long difference = endTime.Ticks - startTime.Ticks;
        



        //Debug.Log("Pick took");
        //Debug.Log(difference / TimeSpan.TicksPerMillisecond);
        //Debug.Log("Milli-Seconds");
        //Debug.Log(difference);
        //Debug.Log("Ticks");
        DeciderNode node = (DeciderNode)root.to[move];
        root.to = null;
        return new Decider(node, !player);
    }

   

    //this is for internal Deciderness.
    public Decider(DeciderNode node, bool player)
    {
        this.player = player;
        root = node;
        if (root.IsLeaf)
        {
            root.SetMovesTo(player);
        }
    }


    /// <summary>
    /// This is for setting up. don't feed a game in progress here, it will destroy the whole tree!
    /// </summary>
    /// <param name="setupState"></param>
    public Decider(SmartSquare[,] setupState)
    {
        this.player = true;
        root = new DeciderNode(setupState);
        root.SetMovesTo(true);//first player
        //we know this is a leaf
    }
    //------------------------------init above------------------------------

    struct Empty { }

}


