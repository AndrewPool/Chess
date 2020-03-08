
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
        return ((from.row * 8 + from.column)*64) + ((to.row * 8) + to.column);
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

/// <summary>
/// instructions for the move, in order to make sure conditions are met properly
/// </summary>
public enum MoveType
{//addpoision
    Standard,
    CastleShort,
    CastleLong,
    Poisson


}
/// <summary>
/// This is me imagineing that this is swift and creating a healper class that functions to convert enum types to differernt values, and back
/// </summary>
//public static class MoveTypeHelper
//{
//    public static MoveType PoissonForIndex(int i)
//    {
//        if (i == 0) return MoveType.Poisson0;
//        if (i == 1) return MoveType.Poisson1;
//        if (i == 2) return MoveType.Poisson2;
//        if (i == 3) return MoveType.Poisson3;
//        if (i == 4) return MoveType.Poisson4;
//        if (i == 5) return MoveType.Poisson5;
//        if (i == 6) return MoveType.Poisson6;
//        if (i == 7) return MoveType.Poisson7;

//        return MoveType.Standard;

//    }

//    public static int ColumnForPoisson(MoveType moveType)
//    {
//        switch (moveType)
//        { 
//            case MoveType.Poisson0:return 0;
//            case MoveType.Poisson1:return 1;
//            case MoveType.Poisson2:return 2;
//            case MoveType.Poisson3:return 3;
//            case MoveType.Poisson4:return 4;
//            case MoveType.Poisson5:return 5;
//            case MoveType.Poisson6:return 6;
//            case MoveType.Poisson7:return 7;
//                //i think it works like this...
           
//        }
//        return -1;
//    }
//}
