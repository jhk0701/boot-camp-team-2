using System;

interface IBrickFactory
{
    Brick Create(BrickType type);
}