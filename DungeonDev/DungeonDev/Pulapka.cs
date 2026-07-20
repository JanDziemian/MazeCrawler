using System;

public class Pulapka : Obiekt
{
    public int Obrazenia { get; set; }

    public Pulapka((int X, int Y) position, int obrazenia) : base(position)
    {
        Obrazenia = obrazenia;
    }

    public override void Aktywuj(Gracz gracz)
    {
        gracz.Zdrowie -= Obrazenia;
        Log.Pisze($"Wpadłeś w pułapkę! Otrzymujesz {Obrazenia} obrażeń. Zdrowie: {gracz.Zdrowie}");
        this.Position = (-1, -1);
    }
}