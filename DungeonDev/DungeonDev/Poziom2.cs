using System;
using System.Collections.Generic;

public class Poziom2 : GenerujLabirynt
{
    public Poziom2()
    {
        WallsString =
            "╔═════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
            "║                      ║                                  ║        ║   ║                      ║\n" +
            "║══════════════════    ║═════════════════    ║    ════════         ║   ║                  ║   ║\n" +
            "║                      ║                 ║   ║                 ║   ║   ║══════════════════║   ║\n" +
            "║    ══════════════    ║   ║    ═════    ║   ║     ════════    ║   ║   ║                  ║   ║\n" +
            "║   ║              ║   ║   ║   ║     ║   ║   ║   ║         ║   ║   ║   ║   ╔═════╗   ║    ║   ║\n" +
            "║   ║          ║   ║   ║   ║   ║         ║   ║             ║   ║   ║   ║   ╚═════╝   ║    ║   ║\n" +
            "║    ══════════    ║   ║   ║    ═════════    ║═════════════║   ║                     ║        ║\n" +
            "║                  ║       ║                 ║             ║   ║    ══════════════════════    ║\n" +
            "║    ═════════════════     ║══════════════════      ═══    ║   ║   ║                      ║   ║\n" +
            "║   ║       ║         ║    ║             ║         ║   ║   ║   ║   ║   ║                  ║   ║\n" +
            "║   ║   ║   ║     ║   ║    ║   ╔═════╗   ║     ║   ║   ║   ║   ║   ║    ══════════════════    ║\n" +
            "║   ║   ║   ║      ═══     ║   ╚═════╝   ║      ═══    ║   ║   ║   ║                          ║\n" +
            "║       ║   ║                            ║             ║       ║    ══════════════════════    ║\n" +
            "║═══════     ═══════════════════         ║════════════════     ║   ║                          ║\n" +
            "║                                   ║    ║   ║                 ║   ║   ╔═════╗    ════════════║\n" +
            "║    ═══════     ═══════════════════║    ║   ║   ╔═════╗   ║   ║   ║   ╚═════╝   ║        ║   ║\n" +
            "║   ║       ║   ║         ║         ║    ║   ║   ╚═════╝   ║   ║                      ║   ║   ║\n" +
            "║   ║   ║   ║   ║     ║   ║    ═════     ║                 ║   ║    ═════════════════ ║   ║   ║\n" +
            "║   ║   ║   ║    ═════    ║                  ══════════════        ║                  ║       ║\n" +
            "║       ║   ║             ║═════════════════║       ║              ║              ║   ║   ║   ║\n" +
            "╠═════════════════════    ║                 ║   ║   ║   ║   ║    ═════════════════║   ║   ║   ║\n" +
            "║                         ║   ║   ╔═════╗   ║   ║   ║   ║   ║   ║                 ║   ║   ║   ║\n" +
            "║    ═════════════════════    ║   ╚═════╝   ║   ║    ═══    ║    ════════     ════    ║   ║   ║\n" +
            "║                             ║                 ║           ║            ║                    ║\n" +
            "╚═════════════════════════════════════════════════════════════════════════════════════════════╝";
        CharPosition = (2, 1);
        CharMovDirection = Direction.Right;
        CharMovFrame = 0;

        Przeciwnik a = new("Szkielet", 10, 20, this, new Battle());
        a.Position = a.StartPosition = (29, 10);
        a.Color = ConsoleColor.Black;
        a.FramesToUpdate = 6;
        a.Update = () => a.UpdateEnemy(a);
        a.MovementPattern = new List<(int X, int Y)> { (29, 10), (39, 10), (39, 13), (29, 13) };

        Przeciwnik b = new("Szkielet", 10, 20, this, new Battle());
        b.Position = b.StartPosition = (32, 21);
        b.Color = ConsoleColor.Black;
        b.FramesToUpdate = 6;
        b.Update = () => b.UpdateEnemy(b);
        b.MovementPattern = new List<(int X, int Y)> { (32, 21), (32, 24), (42, 24), (42, 21) };

        Przeciwnik c = new("Szkielet", 10, 20, this, new Battle());
        c.Position = c.StartPosition = (47, 15);
        c.Color = ConsoleColor.Black;
        c.FramesToUpdate = 6;
        c.Update = () => c.UpdateEnemy(c);
        c.MovementPattern = new List<(int X, int Y)> { (47, 15), (57, 15), (57, 18), (47, 18) };

        Przeciwnik d = new("Szkielet", 10, 20, this, new Battle());
        d.Position = d.StartPosition = (69, 14);
        d.Color = ConsoleColor.Black;
        d.FramesToUpdate = 6;
        d.Update = () => d.UpdateEnemy(d);
        d.MovementPattern = new List<(int X, int Y)> { (69, 14), (79, 14), (79, 17), (69, 17) };

        Przeciwnik e = new("Szkielet", 10, 20, this, new Battle());
        e.Position = e.StartPosition = (73, 4);
        e.Color = ConsoleColor.Black;
        e.FramesToUpdate = 6;
        e.Update = () => e.UpdateEnemy(e);
        e.MovementPattern = new List<(int X, int Y)> { (73, 4), (73, 7), (83, 7), (83, 4) };

        Enemies = new Przeciwnik[] { a, b, c, d, e };

        Pulapka[] traps = { new Pulapka((12, 15), 10) };
        Drzwi[] drzwi = { new Drzwi((6, 10), ConsoleColor.Yellow) };
        Dzwignia[] dzwignia = { new Dzwignia((25, 19), drzwi[0]) };
        Skarby[] treasures = { new Skarby((30, 10), 100) };
        Mikstura[] mikstura = { new Mikstura((10, 5), 50) };
        Klucz[] klucz = { new Klucz((40, 15), drzwi[0]) };

        Pulapka = traps;
        Drzwi = drzwi;
        Dzwignia = dzwignia;
        Skarby = treasures;
        Mikstura = mikstura;
        Klucz = klucz;
    }
}