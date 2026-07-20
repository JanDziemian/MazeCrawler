using System;

public abstract class Obiekt
{
    public (int X, int Y) Position;
    public ConsoleColor Color;

    public Obiekt((int X, int Y) position)
    {
        Position = position;
    }

    public abstract void Aktywuj(Gracz gracz);
}