using System;

public class Skarby : Obiekt
{
    public int Wartosc { get; set; }

    public Skarby((int X, int Y) position, int wartosc) : base(position)
    {
        Wartosc = wartosc;
    }

    public override void Aktywuj(Gracz gracz)
    {
        gracz.Punkty += Wartosc;
        Log.Pisze($"Znalazłeś skarb (+{Wartosc} pkt)! Łącznie: {gracz.Punkty} pkt");
        this.Position = (-1, -1);
    }
}