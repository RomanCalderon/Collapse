using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Coordinate position;
    public Coordinate Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    
    public Node(Coordinate coord)
    {
        position = coord;
    }

    public Node GetNeighbor(Coordinate relativeCoord)
    {
        Coordinate targetPosition = position + relativeCoord;
        
        if (targetPosition.X >= 0 && targetPosition.X <= 2 &&
            targetPosition.Y >= 0 && targetPosition.Y <= 2 &&
            targetPosition.Z >= 0 && targetPosition.Z <= 2)
        {
            return GameManager.nodes[
                Mathf.RoundToInt(targetPosition.X),
                Mathf.RoundToInt(targetPosition.Y),
                Mathf.RoundToInt(targetPosition.Z)
                ];
        }
        else
            return null;
    }
}

public class Coordinate
{
    public int X, Y, Z;

    public Coordinate(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Coordinate(Vector3 position)
    {
        X = Mathf.RoundToInt(position.x);
        Y = Mathf.RoundToInt(position.y);
        Z = Mathf.RoundToInt(position.z);
    }

    public Coordinate(Vector2 position)
    {
        X = Mathf.RoundToInt(position.x);
        Z = Mathf.RoundToInt(position.y);
    }

    public static Vector3 ToVector(Coordinate coord)
    {
        return new Vector3(coord.X, coord.Y, coord.Z);
    }

    public static Coordinate operator+(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
    }

    public override string ToString()
    {
        return "(" + X + ", " + Y + ", " + Z + ")";
    }
}
