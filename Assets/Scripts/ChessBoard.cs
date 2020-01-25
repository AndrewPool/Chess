
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It's kinda terrible that this isn't safe or shit, what can you do...
/// This is a container for the board array this could easy also be a static helper class that configures squares[,]
/// </summary>
public struct ChessBoard {

    public readonly Move moveToMakeThis;
    public bool whiteInCheck;
    public bool blackInCheck;
    public bool whiteWon;
    public bool blackWon;
    public int score;
    
    public readonly SmartSquare[,] board;

   

    /// <summary>
    ///
    /// this is if for somereason you already have a bunch of smart squares, like vecause i created one for setting up the game.
    /// </summary>
    /// <param name="board"></param>
    public ChessBoard(SmartSquare[,] board)
    {
        //we figure this out later
        whiteInCheck = false;
        blackInCheck = false;
        whiteWon = false;
        blackWon = false;
        score = 0;
        this.board = board;
        moveToMakeThis =new Move(new Location(4,4), new Location(4,4));//this only works for new games!!!
        MakeNodesSmart();
    }

    /// <summary>
    /// this is the main interface for creating a ChessBoard, it wants a bunch of smart squares, and then it does the transform on it, and makes a new one.
    /// </summary>
    /// <param name="array"> this is the starting situation before the board changes</param>
    /// <param name="move"> this is the instructions for the board to make a move</param>
    public ChessBoard(SmartSquare[,] array, Move move)
    {
        moveToMakeThis = move;//this is for poisson
        //we figure this out later
        whiteWon = false;
        blackWon = false;
        whiteInCheck = false;
        blackInCheck = false;
        score = 0;
        //i'm not sure why all of this is nessessary. blame microsoft.
        SmartSquare[,] newArray = (SmartSquare[,]) array.Clone();
        // SmartSquare[,] newArray = new SmartSquare[8, 8];

        //this could be done in make nodes smart. maybe...
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {

               //this is nessessary because these are objects.
               //also we can't do it on the fly because we also tag the to
               //we would have to implement so much shit to get it to work.
                    newArray[row, col].movesTo = new Location[0];
              
                    newArray[row, col].moves = new Location[0];
            

            }
        }
        //makes the new location have the token as the old spaces
        newArray[move.to.row, move.to.column] = newArray[move.from.row, move.from.column];
        // then clears the space
        newArray[move.from.row, move.from.column] = new SmartSquare(true, Token.None);

        MoveTypeHandle(move, newArray);
        
        board = newArray;

        MakeNodesSmart();
    }
    private static void MoveTypeHandle(Move move, SmartSquare[,] board)
    {
       // Debug.Log()
        switch (move.moveType)
        {
            case MoveType.CastleLong:
                if(move.from.row == 7)
                {
                    board[7, 3] = board[7, 0];
                    board[7,0] = new SmartSquare(true, Token.None);

                }
                else
                {
                    board[0, 3] = board[0, 0];
                    board[0, 0] = new SmartSquare(true, Token.None);
                }
                break;
            case MoveType.CastleShort:
                if (move.from.row == 7)
                {
                    board[7, 5] = board[7, 7];
                    board[7, 7] = new SmartSquare(true, Token.None);

                }
                else
                {
                    board[0, 5] = board[0, 7];
                    board[0, 7] = new SmartSquare(true, Token.None);
                }
                break;
        }

    }


    //////---------------------------everything is in Make nodes smart,
    ///we want to do everything in this pass, and get all relevant info out of it. this is exciting times for all of us
    ///-------------------------------------
    //TODO bring setmovesto in DeciderNODE implementation down to here to resolve fancy moves

    /// <summary>
    /// soo many magic numbers in this method
    /// it is a mounth full, but hey, you do what you can to limit the boxes, also everything is where you'ld look for it so that something
    /// </summary>
    private void MakeNodesSmart()
    {
        bool whiteKingFound = false;
        bool blackKingFound = false;

        Location whiteKingLocation = new Location(-1, -1);
        Location blackKingLocation = new Location(-1, -1);

        IList<Location> gradeAblePlaces = new List<Location>();

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Location fromLoc = new Location(row, col);
                Location checkLoc;

                //different players move in different directions
                if (!board[row, col].isEmpty)
                {
                    gradeAblePlaces.Add(fromLoc);
                    switch (board[row, col].unit.token)
                    {

                        case Token.None: break;
                        case Token.Pawn:
                            //this is for catching the queen ranking!!

                            //TODO add poisn movement in the move 
                            if (row == 0 || row == 7)
                            {
                                board[row, col] = board[row, col].ConvertToQueen();
                                SetMovementWithVectors(Location.RoyaltyTravelVectors, fromLoc);
                                break;

                            }
                            //Debug.Log("pawn found");
                            //check space below
                            if (board[row, col].unit.player)//is white
                            {
                                //check space above
                                checkLoc = new Location(row + 1, col);

                                if (ValidateSpace(checkLoc))
                                {
                                    if (board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        SetMovement(from: fromLoc, to: checkLoc);

                                        //if you are on the first row, try a double move
                                        checkLoc = new Location(row + 2, col);
                                        if (row == 1 && board[row + 2, col].isEmpty)
                                        {
                                            SetMovement(from: fromLoc, to: checkLoc);
                                        }


                                    }
                                }
                                //up and left
                                checkLoc = new Location(row + 1, col - 1);
                                if (ValidateSpace(checkLoc))
                                {
                                    if (!board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        if (board[checkLoc.row, checkLoc.column].unit.player == false)//we know this is black 
                                        {
                                            SetMovement(from: fromLoc, to: checkLoc);
                                        }
                                        else
                                        {
                                            SetGuardMovement(fromLoc, checkLoc);
                                        }
                                    }
                                }

                                //up and right
                                checkLoc = new Location(row + 1, col + 1);
                                if (ValidateSpace(checkLoc))
                                {
                                    if (!board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        if (board[checkLoc.row, checkLoc.column].unit.player == false)//we know this is black 
                                        {
                                            SetMovement(from: fromLoc, to: checkLoc);
                                        }
                                        else//we know it it friendly, we guard
                                        {
                                            SetGuardMovement(fromLoc, checkLoc);
                                        }
                                    }
                                }
                            }

                            else//if black
                            {
                                //check space above
                                checkLoc = new Location(row - 1, col);
                                if (ValidateSpace(checkLoc))
                                {
                                    if (board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        SetMovement(from: fromLoc, to: checkLoc);

                                        //if you are on the first row, try a double move
                                        checkLoc = new Location(row - 2, col);
                                        if (row == 6 && board[checkLoc.row, checkLoc.column].isEmpty)
                                        {
                                            SetMovement(from: fromLoc, to: checkLoc);
                                        }

                                    }
                                }
                                //down and left
                                checkLoc = new Location(row - 1, col - 1);
                                if (ValidateSpace(checkLoc))
                                {
                                    if (!board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        if (board[checkLoc.row, checkLoc.column].unit.player == true)//we know this is white 
                                        {
                                            SetMovement(from: fromLoc, to: checkLoc);
                                        }
                                        else
                                        {
                                            SetGuardMovement(fromLoc, checkLoc);
                                        }
                                    }
                                }
                                //down and right
                                checkLoc = new Location(row - 1, col + 1);
                                if (ValidateSpace(checkLoc))
                                {
                                    if (!board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        if (board[checkLoc.row, checkLoc.column].unit.player == true)//we know this is white 
                                        {
                                            SetMovement(from: fromLoc, to: checkLoc);
                                        }
                                        else//we know it it friendly, we guard
                                        {
                                            SetGuardMovement(fromLoc, checkLoc);
                                        }
                                    }
                                }
                            }

                            break;



                        case Token.Knight:
                            for (int i = 0; i < Location.KnightHops.Length; i++)
                            {
                                checkLoc = fromLoc.Add(Location.KnightHops[i]);
                                if (ValidateSpace(checkLoc))
                                {
                                    //if the space is empty, se movement
                                    if (board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        SetMovement(fromLoc, checkLoc);
                                    }
                                    else
                                    {//set guard movment if friendly, take movment if hostile

                                        if (board[checkLoc.row, checkLoc.column].unit.player == board[fromLoc.row, fromLoc.column].unit.player)
                                        {
                                            SetGuardMovement(fromLoc, checkLoc);
                                        }
                                        else
                                        {
                                            SetMovement(fromLoc, checkLoc);
                                        }
                                    }

                                }

                            }
                            break;



                        case Token.Bishop:
                            SetMovementWithVectors(Location.BishopTravelVectors, fromLoc);
                            break;



                        case Token.Rook:
                            SetMovementWithVectors(Location.RookTravelVectors, fromLoc);
                            break;



                        case Token.Queen:
                            SetMovementWithVectors(Location.RoyaltyTravelVectors, fromLoc);
                            break;



                        case Token.King:
                            if (board[row, col].unit.player)
                            {
                                whiteKingLocation = new Location(row, col);
                                whiteKingFound = true;
                            }
                            else
                            {
                                blackKingLocation = new Location(row, col);
                                blackKingFound = true;
                            }

                            for (int i = 0; i < Location.RoyaltyTravelVectors.Length; i++)
                            {
                                Location[] vectors = Location.RoyaltyTravelVectors;
                                checkLoc = fromLoc.Add(vectors[i]);
                                if (ValidateSpace(checkLoc))
                                {
                                    if (board[checkLoc.row, checkLoc.column].isEmpty)
                                    {
                                        SetMovement(fromLoc, checkLoc);
                                    }

                                    else if (board[checkLoc.row, checkLoc.column].unit.player == board[fromLoc.row, fromLoc.column].unit.player)
                                    {
                                        SetGuardMovement(fromLoc, checkLoc);
                                    }
                                    else//is not friendly
                                    {
                                        SetMovement(fromLoc, checkLoc);
                                    }

                                }
                            }
                            break;

                    }
                }
            }
        }
        //can put the castle thingy here too. don't use the moves to to figure this out.

        if (whiteKingFound)
        {
            if (board[whiteKingLocation.row, whiteKingLocation.column].movesTo.Length > 0)
            {
                whiteInCheck = true;
            }
        }
        else
        {
            blackWon = true;
            return;
        }
        if (blackKingFound) { 
        if (board[blackKingLocation.row, blackKingLocation.column].movesTo.Length > 0)
        {
            blackInCheck = true;
        }
        } else
        {
            whiteWon = true;
            return;
        }
        //this is the scoreing "functino" i guess it went here because it goes here i guess.
        int score = 0;

        //Debug.Log(gradeAblePlaces.Count.ToString());
        foreach(Location piece in gradeAblePlaces)
        {
           
            bool player = board[piece.row, piece.column].unit.player;
            int unitscore = TokenHelper.Value(board[piece.row, piece.column].unit.token, player);

            int bestMove = 0;
            Location[] moves = board[piece.row, piece.column].moves;
            for (int i = 0; i < moves.Length; i++)
            {
                int locationScore = TokenHelper.Value(board[moves[i].row, moves[i].column].unit.token, player);

                if (player)//if white
                {
                    if (locationScore > bestMove) bestMove = locationScore;
                }
                else//if black
                {
                    if (locationScore < bestMove) bestMove = locationScore;
                }
            }

            moves = board[piece.row, piece.column].movesTo;

            int chainScore = ScoreForPlayer(moves, player, unitscore);


            score = score + unitscore + bestMove + chainScore; 

        }
        this.score = score;

    }














    private int ScoreForPlayer(Location[] movesTo, bool player, int firstPieceValue)
    {
        int score = 0;
        bool currentPlayer = player;
        List<int> goodNumbers = new List<int>();
        List<int> badNumbers = new List<int>();
        for (int i = 0; i < movesTo.Length; i++)
        {
            bool otherTokenPlayer = board[movesTo[i].row, movesTo[i].column].unit.player;

            if(otherTokenPlayer == currentPlayer)
            {
                goodNumbers.Add(TokenHelper.Value(board[movesTo[i].row, movesTo[i].column].unit.token, otherTokenPlayer));
            }
            else//its a bad one.
            {
                badNumbers.Add(TokenHelper.Value(board[movesTo[i].row, movesTo[i].column].unit.token, otherTokenPlayer));
            }

        }

        Dictionary<bool, List<int>> cycleDictList = new Dictionary<bool, List<int>> { {currentPlayer, goodNumbers},{ !currentPlayer, badNumbers } };
        int goodNumIndex = 0;
        int badNumIndex = 0;
        int badNumCount = badNumbers.Count;
       
        if (badNumCount > 0)
        {
            //so white wants a normal sort up because they go 1to9
            //black wants a reverse sort because they go -1to-9
            if (currentPlayer)//is white
            {
                goodNumbers.Sort();
                badNumbers.Sort();
                badNumbers.Reverse();
            }
            else//currentplayer is black
            {
                badNumbers.Sort();
                goodNumbers.Sort();
                goodNumbers.Reverse();
            }
            
            //if the first piece is worth more than second
            if (Math.Abs(firstPieceValue) >= Math.Abs(badNumbers[badNumIndex]))
            {
                score =- firstPieceValue;



                //int count = badNumCount < goodNumCount ? badNumCount : goodNumCount;
                int goodNumCount = goodNumbers.Count;
                if (goodNumCount > 0) {


                    bool cyclePlayer = !currentPlayer;
                    do
                    {
                       
                        List<int> toBeDeletedFrom = cycleDictList[currentPlayer];
                        List<int> toDelete = cycleDictList[!currentPlayer];

                        if (cyclePlayer == currentPlayer)
                        {
                            if (Math.Abs(toDelete[goodNumIndex]) >= Math.Abs(badNumbers[badNumIndex]))
                            {
                                score = -toDelete[goodNumIndex];
                                goodNumIndex++;
                            }
                            else
                            {
                                return score;
                            }

                        }
                        else
                        {
                            if (Math.Abs(toDelete[badNumIndex]) >= Math.Abs(badNumbers[goodNumIndex]))
                            {
                                score = -toDelete[badNumIndex];
                                badNumIndex++;
                            }
                            else
                            {
                                
                                return score;
                            }
                        }



                        cyclePlayer = !cyclePlayer;

                    } while (goodNumIndex < goodNumCount && badNumIndex < badNumCount);

                }
                
                
              

            
            }


        }


        return score;
    }

    private void SetMovementWithVectors(Location[] vectors, Location fromLoc)
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            Location checkLoc = fromLoc.Add(vectors[i]);
            while (ValidateSpace(checkLoc))
            {
                if (board[checkLoc.row, checkLoc.column].isEmpty)
                {
                    SetMovement(fromLoc, checkLoc);
                    checkLoc = checkLoc.Add(vectors[i]);
                }else if (board[checkLoc.row, checkLoc.column].unit.player == board[fromLoc.row, fromLoc.column].unit.player)
                {
                    SetGuardMovement(fromLoc, checkLoc);
                    break;
                }
                else//is not friendly
                {
                    SetMovement(fromLoc, checkLoc);
                    break;
                }
              
            }
        }
        
    }

    //makes sure teh space is on the board
    private bool ValidateSpace(Location location)
    {
        if (location.column < 8 && location.row < 8 && location.row >= 0 && location.column >= 0) return true;
        return false;
    }

    //doesn't set guard points for defending the king, that wouldn't make much sense, since defending is more like reclaming it's dead body
    private void SetGuardMovement(Location from, Location to)
    {
        if (board[to.row, to.column].unit.token != Token.King) board[to.row, to.column].AddToMovesTo(from);
    }


    //adds a  to and from thing
    private void SetMovement(Location from, Location to)
    {
        
            board[from.row, from.column].AddToMoves(to);

            board[to.row, to.column].AddToMovesTo(from);
        
    }

}
