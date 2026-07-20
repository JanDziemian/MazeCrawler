using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        EkranGlowny ekranGlowny = new EkranGlowny();
        ekranGlowny.PokazEkranStartowy();
        ekranGlowny.Menu();

        GenerujLabirynt Labirynt = new GenerujLabirynt();
        Battle battle = new Battle();

        Gracz player = new Gracz("Hero", 100, 10, Labirynt, 0)
        {
            CharPosition = (2, 1)
        };

        Przeciwnik a = new("Szkielet", 10, 20, Labirynt, battle);
        a.Position = a.StartPosition = (42, 5);
        a.Color = ConsoleColor.Black;
        a.FramesToUpdate = 6;
        a.Update = () => a.UpdateEnemy(a);
        a.MovementPattern = new List<(int X, int Y)> {
            (42, 5), (43, 5), (44, 5), (45, 5), (46, 5), (47, 5), (48, 5), (49, 5), (50, 5), (51, 5), (52, 5), (52, 6), (52, 7),
            (52, 8), (51, 8), (50, 8), (49, 8), (48, 8), (47, 8), (46, 8), (45, 8), (44, 8), (43, 8), (42, 8), (42, 7), (42, 6), (42, 5)
        };

        Przeciwnik b = new("Szkielet", 10, 20, Labirynt, battle);
        b.Position = b.StartPosition = (44, 12);
        b.Color = ConsoleColor.Black;
        b.FramesToUpdate = 6;
        b.Update = () => b.UpdateEnemy(b);
        b.MovementPattern = new List<(int X, int Y)> {
            (44, 12), (44, 13), (44, 14), (44, 15), (45, 15), (46, 15), (47, 15), (48, 15), (49, 15), (50, 15), (51, 15), (52, 15), (53,15),
            (54, 15), (54, 14), (54, 13), (54, 12), (53, 12), (52, 12), (51, 12), (50, 12), (49, 12), (48, 12), (47, 12), (46, 12), (45, 12)
        };

        Przeciwnik c = new("Szkielet", 10, 20, Labirynt, battle);
        c.Position = c.StartPosition = (15, 17);
        c.Color = ConsoleColor.Black;
        c.FramesToUpdate = 6;
        c.Update = () => c.UpdateEnemy(c);
        c.MovementPattern = new List<(int X, int Y)> {
            (15, 17), (16, 17), (17, 17), (18, 17), (19, 17), (20, 17), (21, 17), (22, 17), (23, 17), (24, 17), (25, 17),
            (25, 18), (25, 19), (25, 20), (24, 20), (23, 20), (22, 20), (21, 20), (20, 20), (19, 20), (18, 20), (17, 20), (16, 20), (15, 20)
        };

        Przeciwnik d = new("Szkielet", 10, 20, Labirynt, battle);
        d.Position = d.StartPosition = (50, 21);
        d.Color = ConsoleColor.Black;
        d.FramesToUpdate = 6;
        d.Update = () => d.UpdateEnemy(d);
        d.MovementPattern = new List<(int X, int Y)> {
            (50, 21), (51, 21), (52, 21), (53, 21), (54, 21),(55, 21), (56, 21), (57, 21), (58, 21), (59, 21), (60, 21),
            (60, 22), (60, 23), (60, 24), (59, 24), (58, 24), (57, 24), (56, 24), (55, 24), (54, 24), (53, 24), (52, 24), (51, 24), (50, 24)
        };

        Przeciwnik e = new("Szkielet", 10, 20, Labirynt, battle);
        e.Position = e.StartPosition = (70, 8);
        e.Color = ConsoleColor.Black;
        e.FramesToUpdate = 6;
        e.Update = () => e.UpdateEnemy(e);
        e.MovementPattern = new List<(int X, int Y)> {
            (70, 8), (70, 9), (70, 10), (70, 11), (71, 11),(72, 11),(73, 11), (74, 11),(75, 11),(76, 11), (77, 11),(78, 11),(79, 11),
            (80, 11), (80, 10), (80, 9), (80, 8)
        };

        Labirynt.Enemies = new Przeciwnik[] { a, b, c, d, e };

        Drzwi[] drzwi = { new Drzwi((6, 10), ConsoleColor.Yellow) };
        Labirynt.Pulapka = new Pulapka[] { new Pulapka((12, 15), 10) };
        Labirynt.Drzwi = drzwi;
        Labirynt.Dzwignia = new Dzwignia[] { new Dzwignia((25, 19), drzwi[0]) };
        Labirynt.Skarby = new Skarby[] { new Skarby((30, 10), 100) };
        Labirynt.Mikstura = new Mikstura[] { new Mikstura((10, 5), 50) };
        Labirynt.Klucz = new Klucz[] { new Klucz((40, 15), drzwi[0]) };

        Console.SetWindowSize(100, 60);
        Console.SetBufferSize(100, 60);
        Console.Clear();

        Labirynt.RenderWalls();
        player.RenderPlayer();
        Labirynt.RenderEnemies();
        Labirynt.RenderDrzwi();
        Labirynt.RenderPułapki();
        Labirynt.RenderSkarbow();
        Labirynt.RenderDzwignia();

        while (player.CzyZyje() && player.CharPosition != (X: 92, Y: 24))
        {
            if (player.HandleInput()) return;

            Labirynt.RenderWalls();
            player.RenderPlayer();
            Labirynt.RenderEnemies();
            Labirynt.UpdateEnemies();
            Labirynt.RenderDrzwi();
            Labirynt.RenderPułapki();
            Labirynt.RenderSkarbow();
            Labirynt.RenderDzwignia();
            Labirynt.RenderMikstura();
            Labirynt.RenderKlucz();

            Thread.Sleep(100);
        }

        if (!player.CzyZyje())
        {
            Console.WriteLine("Gracz nie żyje.");
            return;
        }

        Console.Clear();
        Log.Czysc();
        Poziom2 poziom2 = new Poziom2();
        player.UpdateLabirynt(poziom2);

        poziom2.CharMovDirection = player.Labirynt.CharMovDirection;
        poziom2.CharMovFrame = player.Labirynt.CharMovFrame;

        Console.SetWindowSize(100, 60);
        Console.SetBufferSize(100, 60);
        Console.Clear();

        while (player.CzyZyje())
        {
            if (player.HandleInput()) return;

            poziom2.RenderWalls();
            player.RenderPlayer();
            poziom2.RenderEnemies();
            poziom2.UpdateEnemies();
            poziom2.RenderDrzwi();
            poziom2.RenderPułapki();
            poziom2.RenderSkarbow();
            poziom2.RenderDzwignia();
            poziom2.RenderMikstura();
            poziom2.RenderKlucz();

            Thread.Sleep(100);
        }

        Console.WriteLine("Gracz nie żyje.");
    }
}