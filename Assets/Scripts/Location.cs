using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Location : IEquatable<Location>
{
    
    public readonly int row;
    public readonly int column;
    public Location(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public int Mapped2D()
    {
        return (row * 8) + column;
    }


    public Location Add(Location increment)
    {
        return new Location(row + increment.row, column + increment.column);
    }
    public bool Equals(Location other)
    {
        if (row == other.row & column == other.column)
        {
            return true;
        }
        return false;
    }

    public String String()
    {
        return row + " " + column;
    }

    //bunch of static location arrays for moving using as a refrence for pieces.
    public readonly static Location[] KnightHops = new Location[8]
        {//clockwise from top left
            new Location(2,-1)

            ,new Location(2,1)

            ,new Location(1,2)

            ,new Location(-1,2)

            ,new Location(-2,1)

            ,new Location(-2,-1)

            ,new Location(-1,-2)

            ,new Location(1 ,-2)

        };
    public readonly static Location[] RookTravelVectors = new Location[4]
        {
            new Location(0,1)

            ,new Location(1,0)

            ,new Location(0,-1)

            ,new Location(-1,0)

        };

    public readonly static Location[] BishopTravelVectors = new Location[4]
        {
            new Location(1,-1)

            ,new Location(1,1)

            ,new Location(-1,1)

            ,new Location(-1,-1)
        };

    public readonly static Location[] RoyaltyTravelVectors = new Location[8]
            {
            new Location(1,-1)

            ,new Location(1,0)

            ,new Location(1,1)

            ,new Location(0,1)

            ,new Location(-1,1)

            ,new Location(-1,0)

            ,new Location(-1,-1)

            ,new Location(0,-1)

            };

}
