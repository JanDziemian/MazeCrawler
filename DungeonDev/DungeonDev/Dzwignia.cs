using System;

public class Dzwignia : Obiekt
{
    public bool Aktywna { get; set; }
    public Drzwi PowiazaneDrzwi { get; set; }

    public Dzwignia((int X, int Y) position, Drzwi powiazaneDrzwi) : base(position)
    {
        PowiazaneDrzwi = powiazaneDrzwi;
        Aktywna = false;
    }

    public override void Aktywuj(Gracz gracz)
    {
        if (Aktywna)
        {
            Log.Pisze("Dźwignia jest już aktywna.");
        }
        else
        {
            Aktywna = true;
            PowiazaneDrzwi.Otworz();
            Log.Pisze("Aktywowałeś dźwignię – powiązane drzwi się otworzyły!");
        }
    }

    public void Uzyj(Gracz gracz) => Aktywuj(gracz);
}