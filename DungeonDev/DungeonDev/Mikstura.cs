using System;

public class Mikstura : Przedmiot
{
    public int Leczenie { get; set; }

    public Mikstura((int X, int Y) position, int leczenie) : base(position)
    {
        Leczenie = leczenie;
    }

    public override void Uzyj(Gracz gracz)
    {
        gracz.Zdrowie += Leczenie;
        if (gracz.Zdrowie > 100) gracz.Zdrowie = 100;
        Log.Pisze($"Użyto mikstury (+{Leczenie} HP). Zdrowie: {gracz.Zdrowie}");
        this.Position = (-1, -1);
    }
}