using System;

public abstract class Stworzenie
{
    public int Zdrowie { get; set; }
    public int Atak { get; set; }
    public string Imie { get; set; }
    public GenerujLabirynt Labirynt { get; set; }

    public Stworzenie(int zdrowie, int atak, string imie)
    {
        Zdrowie = zdrowie;
        Atak = atak;
        Imie = imie;
    }

    public bool CzyZyje() => Zdrowie > 0;

    public char BoardAt(int x, int y)
    {
        var (width, height) = Labirynt.GetBoardDimensions();
        if (x < 0 || x >= width || y < 0 || y >= height) return '#';
        return Labirynt.WallsString[y * (width + 1) + x];
    }

    public bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';

    public bool CanMove(int x, int y, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return !IsWall(x, y - 1);
            case Direction.Down: return !IsWall(x, y + 1);
            case Direction.Left: return !IsWall(x - 1, y);
            case Direction.Right: return !IsWall(x + 1, y);
            default: throw new NotImplementedException();
        }
    }
}