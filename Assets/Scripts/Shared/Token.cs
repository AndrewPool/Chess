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
            case Token.Pawn: if (player) { return -2; } return 2;
            case Token.Knight: if (player) { return -6; } return 6;
            case Token.Bishop: if (player) { return -7; } return 7;
            case Token.Rook: if (player) { return -10; } return 10;
            case Token.Queen: if (player) { return -20; } return 20;
            case Token.King: if (player) { return int.MinValue/10000; } return int.MaxValue/10000;

        }
        return int.MaxValue;

    }
   
}