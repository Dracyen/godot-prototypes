using System;

public enum Direction
{
    South = 0,
    SouthWest = 1,
    West = 2,
    NorthWest = 3,
    North = 4,
    NorthEast = 5,
    East = 6,
    SouthEast = 7
}

public enum AnimState
{
    Idle,
    Walk,
    Run
}