
using System;
using System.Collections.Generic;
using UnityEngine;
using robinhood;
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
        Debug.Log("white won:" + root.board.WhiteWon);
        Debug.Log("Black won:" + root.board.BlackWon);
        if (root.board.WhiteWon || root.board.BlackWon)
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
        if (root.board.WhiteWon)
        {
            return true;
        }
        return false;
    }

    public int ScoreForCurrentState()
    {
        return root.board.Score;
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


    //DateTime startTime = System.DateTime.Now;
    //DateTime endTime = System.DateTime.Now;
    //long difference = endTime.Ticks - startTime.Ticks;
    //Debug.Log("Pick took");
    //Debug.Log(difference / TimeSpan.TicksPerMillisecond);
    //Debug.Log("Milli-Seconds");
    //Debug.Log(difference);
    //Debug.Log("Ticks");


    /// <summary>
    /// This has the chance of returning a null value/crash in the case there are no moves, be preparded to catch i guess.
    /// </summary>
    /// <returns>the first move, because it's unimplemented</returns>
    public Move PickOneForMe()
    {

        Debug.Log("text works");
        return TwoDeep();

    }
    //TODO make N deep
    public Move TwoDeep()
    {
        Debug.Log("two deep picker");
        IDictionary<DeciderNode, Empty> checkList = new Dictionary<DeciderNode,Empty>(10000);
        ICollection<ITraversable> nodes = root.ToNodes();
        foreach (DeciderNode node in nodes)
        {

            checkList.Add(node, new Empty());
        }

        foreach (DeciderNode node in nodes)
        {

            node.AddAndCreateAllUniqueChildrenAgainstChecklist(checkList, 2);
        }
        DeciderNode bestNode = new DeciderNode(SmartSquare.StandardBoardSetUp());

        int best = int.MaxValue;
        if (root.Player)
        {
            foreach (ITraversable child in nodes)
            {

                DeciderNode testChild = (DeciderNode)child;
                int score = testChild.ScoreForBranch();


                if (score < best)
                {
                    best = score;
                    bestNode = testChild;
                }




            }
        }
        else
        {
           
            best = int.MinValue;
          
            foreach (ITraversable child in nodes)
            {
                DeciderNode testChild = (DeciderNode)child;
                int score = testChild.ScoreForBranch();


                if (score > best)
                {
                    best = score;
                    bestNode = testChild;
                }

            }


        }

        return bestNode.board.moveToMakeThis;//TODO this will return null







    }
    public Move OneDeep()
    {
        Debug.Log("one deep picker");
        ICollection<ITraversable> nodes = root.ToNodes();
        foreach (DeciderNode node in nodes)
        {
           
            node.SetMovesTo();
        }
        nodes = root.ToNodes();
        DeciderNode bestNode = new DeciderNode(SmartSquare.StandardBoardSetUp());

        int best = int.MinValue;
        if (!root.Player)
        {
            foreach (ITraversable child in nodes)
            {

                DeciderNode testChild = (DeciderNode)child;
                int score = testChild.BestMoveScore();


                if (score > best)
                {
                    best = score;
                    bestNode = testChild;
                }




            }
        }
        else
        {
            best = int.MaxValue;
            foreach (ITraversable child in nodes)
            {

                DeciderNode testChild = (DeciderNode)child;
                int score = testChild.BestMoveScore();


                if (score < best)
                {
                    best = score;
                    bestNode = testChild;
                }
            }

        }
      
        return bestNode.board.moveToMakeThis;
    }
    private Move StupidHeapBuild()
    {
        Debug.Log("picking one");
        IDictionary<DeciderNode, Empty> nodesInTree = new Dictionary<DeciderNode, Empty>(10000);

        
        MaxHeap heap = new MaxHeap(root);
        Debug.Log("adding nodes to checklist");
        root.AddNodesToTreeRecursivly(nodesInTree);

        if (root.to.Keys == null)
        {
            return null;
        }
        Debug.Log("popping and adding to tree");
        while (nodesInTree.Count < 10000 && heap.HasTop())
        {
            DeciderNode top = (DeciderNode)heap.Pop();


            top.SetMovesTo();
            foreach (DeciderNode node in top.to.Values)
            {
                if (!nodesInTree.ContainsKey(node))
                {

                    heap.AddToHeap(node);
                    nodesInTree.Add(node, new Empty());

                }
            }


        }
        Debug.Log(heap.Count);
        // picking the best for the player;

        DeciderNode topOfHeap = (DeciderNode)heap.Pop();
        while (topOfHeap.Player != root.Player && heap.HasTop())
        {

            topOfHeap = (DeciderNode)heap.Pop();

        }

        return MoveForNode(topOfHeap);


    }


    private Move MoveForNode(DeciderNode node)
    {

        DeciderNode check = node;
        
        while (check.From() != null)
        {
            if ((DeciderNode)check.From() == root)
            {
                return check.board.moveToMakeThis;
            }
            check = (DeciderNode)check.From();
        }
        return node.board.moveToMakeThis;
    }
    private Move BetterMove(Move m1, Move m2)
    {
        var returnMove = m1;
        if (root.Player)
        {

            if (((DeciderNode)root.to[m1]).board.Score > ((DeciderNode)root.to[m2]).board.Score)
            {
                returnMove = m2;
            }
        }
        else
        {
            Debug.Log(((DeciderNode)root.to[m1]).board.Score);
            //((DeciderNode)root.to[m1]).board.Score
            if (((DeciderNode)root.to[m1]).board.Score < ((DeciderNode)root.to[m2]).board.Score)
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

        DeciderNode node = (DeciderNode)root.to[move];
        root.ConvertToTuber();
        return new Decider(node);
    }

   

    //this is for internal Deciderness.
    public Decider(DeciderNode node)
    {
        
        root = node;
        if (root.IsLeaf)
        {
            root.SetMovesTo();
        }
    }


    /// <summary>
    /// This is for setting up. don't feed a game in progress here, it will destroy the whole tree!
    /// </summary>
    /// <param name="setupState"></param>
    public Decider(SmartSquare[,] setupState)
    {
      
        root = new DeciderNode(setupState);
        root.SetMovesTo();//first player
        //we know this is a leaf
    }
    //------------------------------init above------------------------------

 
}


