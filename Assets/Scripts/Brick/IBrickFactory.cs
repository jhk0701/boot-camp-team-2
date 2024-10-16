using System;

interface IBrickFactory
{
    Brick Create(BrickType type);
    Brick Create(PlacementData data);
}