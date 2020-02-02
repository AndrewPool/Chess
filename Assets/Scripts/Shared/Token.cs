//this page has the token options, as well as a helper class to get relevant values in context,

public enum Token {

	None,
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King,

    
}

public static class TokenHelper
{


    public static string String(Token token, bool player)
    {

        switch (token)
        {
            case Token.None: return "  ";
            case Token.Pawn: if (player) { return "wPn"; } return "bPn";
            case Token.Knight: if (player) { return "wKn"; } return "bKn";
            case Token.Bishop: if (player) { return "wBh"; } return "bBh";
            case Token.Rook: if (player) { return "wRk"; } return "bRk";
            case Token.Queen: if (player) { return "wQN"; } return "bQN";
            case Token.King: if (player) { return "wKG"; } return "bKG";

        }
        return "String() didn't work for some reasons";
    }

    public static int Value(Token token, bool player)
    {

        switch (token)
        {
            case Token.None: return 0;
            case Token.Pawn: if (player) { return -1; } return 1;
            case Token.Knight: if (player) { return -3; } return 3;
            case Token.Bishop: if (player) { return -4; } return 4;
            case Token.Rook: if (player) { return -5; } return 5;
            case Token.Queen: if (player) { return -10; } return 10;
            case Token.King: if (player) { return int.MinValue/10; } return int.MaxValue/10;

        }
        return int.MaxValue;

    }
   
}