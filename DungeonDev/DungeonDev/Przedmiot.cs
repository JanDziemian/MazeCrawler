public abstract class Przedmiot
{
    public (int X, int Y) Position;

    public Przedmiot((int X, int Y) position)
    {
        Position = position;
    }

    public abstract void Uzyj(Gracz gracz);
}