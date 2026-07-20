using System;

public class Klucz : Przedmiot
{
    public bool Podniesione { get; private set; }
    public Drzwi DrzwiDoOtwarcia { get; }

    public Klucz((int X, int Y) position, Drzwi drzwiDoOtwarcia) : base(position)
    {
        DrzwiDoOtwarcia = drzwiDoOtwarcia;
        Podniesione = false;
    }

    public override void Uzyj(Gracz gracz)
    {
        if (!Podniesione)
        {
            Podniesione = true;
            DrzwiDoOtwarcia.Otworz();
            Log.Pisze("Podniosłeś klucz i otworzyłeś drzwi!");
            this.Position = (-1, -1);
        }
    }
}