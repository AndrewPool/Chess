
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

    //manually resizing arrays is fun TODO
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
    /// <summary>
    /// this is for making a statndard board
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// this is for testing, not a statndard board setup
    /// </summary>
    /// <returns>normal board, but with rook at [0,0] turned into pawn</returns>
    public static SmartSquare[,] NotStandardBoardSetUp()
    {
        return new SmartSquare[8, 8]
        {
             { new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Rook) }
        };
    }
    /// <summary>
    /// this is for testing, not a statndard board setup
    /// </summary>
    /// <returns>normal board, but with rook at [0,0] turned into black rook</returns>
    public static SmartSquare[,] NotStandardBoardSetUp2()
    {
        return new SmartSquare[8, 8]
        {
             { new SmartSquare(false, Token.Rook), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Rook) }
        };
    }
    /// <summary>
    /// this is for testing, not a statndard board setup
    /// </summary>
    /// <returns>normal board, but with rook at [7,7] turned into pawn</returns>
    public static SmartSquare[,] NotStandardBoardSetUp3()
    {
        return new SmartSquare[8, 8]
        {
             { new SmartSquare(true, Token.Rook), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Pawn) }
        };
    }
    /// <summary>
    /// this is for testing, not a statndard board setup
    /// </summary>
    /// <returns>normal board, but with rook at [7,7] turned into white rook</returns>
    public static SmartSquare[,] NotStandardBoardSetUp4()
    {
        return new SmartSquare[8, 8]
        {
             { new SmartSquare(true, Token.Rook), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(true, Token.Rook) }
        };
    }
    /// <summary>
    /// this is for testing, not a statndard board setup
    /// </summary>
    /// <returns>normal board, but with  [0,0] missing</returns>
    public static SmartSquare[,] NotStandardBoardSetUp5()
    {
        return new SmartSquare[8, 8]
        {
             { new SmartSquare(true, Token.None), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Pawn) }
        };
    }
    /// <summary>
    /// this is for testing, not a statndard board setup
    /// </summary>
    /// <returns>normal board, but with  [7,7] missing</returns>
    public static SmartSquare[,] NotStandardBoardSetUp6()
    {
        return new SmartSquare[8, 8]
        {
             { new SmartSquare(true, Token.None), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Queen), new SmartSquare(true, Token.King), new SmartSquare(true, Token.Bishop), new SmartSquare(true, Token.Knight), new SmartSquare(true, Token.Rook) }
            ,{ new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn), new SmartSquare(true, Token.Pawn) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None), new SmartSquare(true, Token.None) }
            ,{ new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn), new SmartSquare(false, Token.Pawn) }
            ,{ new SmartSquare(false, Token.Rook), new SmartSquare(false, Token.Knight), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Queen), new SmartSquare(false, Token.King), new SmartSquare(false, Token.Bishop), new SmartSquare(false, Token.Knight), new SmartSquare(true, Token.None) }
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

    /// <summary>
    /// refer to documentation
    /// </summary>
    /// <returns>int</returns>
    public int HashValue()
    {
        switch (token)
        {//the complier does this multiplication that is why it is here
            case Token.None: return 0;
            case Token.Pawn: if (player) { return 1*64; } return 12* 64;
            case Token.Knight: if (player) { return 2 * 64; } return 7 * 64;
            case Token.Bishop: if (player) { return 3 * 64; } return 8 * 64;
            case Token.Rook: if (player) { return 4 * 64; } return 9 * 64;
            case Token.Queen: if (player) { return 5 * 64; } return 10 * 64;
            case Token.King: if (player) { return 6 * 64; } return 11 * 64;

        }
        return 0;
    }
  
}