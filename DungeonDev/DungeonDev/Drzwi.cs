using System;

public class Drzwi : Obiekt
{
    public bool Otwarte { get; set; }

    public Drzwi((int X, int Y) position, ConsoleColor color) : base(position)
    {
        Otwarte = false;
        Color = color;
    }

    public override void Aktywuj(Gracz gracz)
    {
        if (Otwarte)
        {
            Log.Pisze("Drzwi są już otwarte.");
        }
        else
        {
            Log.Pisze("Drzwi są zamknięte. Potrzebujesz klucza lub dźwigni.");
        }
    }

    public void Otworz()
    {
        Otwarte = true;
        Log.Pisze("Drzwi zostały otwarte!");
    }
}