using System;
using System.Linq;

public class GenerujLabirynt
{
    public void RenderWalls()
    {
        Console.SetCursorPosition(0, 0);
        WithColors(ConsoleColor.Gray, ConsoleColor.Black, () =>
        {
            Render(WallsString, true);
        });
    }

    public void RenderChar()
    {
        if (CharPosition.X < 0 || CharPosition.Y < 0) return;
        Console.SetCursorPosition(CharPosition.X, CharPosition.Y);
        WithColors(ConsoleColor.Black, ConsoleColor.Blue, () =>
        {
            if (CharMovDirection.HasValue && CharMovFrame.HasValue)
            {
                int frame = (int)CharMovFrame % CharAnimations[(int)CharMovDirection].Length;
                Console.Write(CharAnimations[(int)CharMovDirection][frame]);
            }
            else
            {
                Console.Write(' ');
            }
        });
    }

    public void WithColors(ConsoleColor foreground, ConsoleColor background, Action action)
    {
        ConsoleColor originalForeground = Console.ForegroundColor;
        ConsoleColor originalBackground = Console.BackgroundColor;
        try
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            action();
        }
        finally
        {
            Console.ForegroundColor = originalForeground;
            Console.BackgroundColor = originalBackground;
        }
    }

    public void Render(string @string, bool renderSpace = true)
    {
        int x = Console.CursorLeft;
        int y = Console.CursorTop;
        foreach (char c in @string)
        {
            if (c == '\n')
            {
                Console.SetCursorPosition(x, ++y);
            }
            else
            {
                Console.Write(c);
            }
        }
    }

    public (int Width, int Height) GetBoardDimensions()
    {
        int width = WallsString.IndexOf('\n');
        int height = WallsString.Count(c => c == '\n') + 1;
        return (width, height);
    }

    #region Ascii
    public string WallsString =
       "╔═════════════════════════════════════════════════════════════════════════════════════════════╗\n" +
       "║   ║                    ║                                ║                                   ║\n" +
       "║    ════     ═══════════     ══════════     ═════════    ║    ════════     ══════════════    ║\n" +
       "║                                       ║   ║         ║   ║   ║            ║                  ║\n" +
       "║        ═══     ║        ║   ══════════║    ═════════║   ║   ║════════    ║     ═════════════║\n" +
       "║   ║   ║   ║    ║    ║   ║   ║         ║             ║   ║   ║        ║   ║    ║             ║\n" +
       "║   ║   ║        ║    ║   ║   ║    ║    ║   ╔═════╗   ║   ║   ║        ║   ║                  ║\n" +
       "║    ═════════════    ║   ║    ═════    ║   ╚═════╝   ║   ║    ════════     ══════════════════║\n" +
       "║                     ║   ║                           ║         ║                 ║   ║       ║\n" +
       "║    ═════════════════║    ═══════════════════════     ═════    ║   ║   ╔═════╗   ║   ║  ║    ║\n" +
       "║               ║     ║                           ║         ║   ║   ║   ╚═════╝   ║    ══     ║\n" +
       "║═══════        ║     ║    ═══════════════     ═════════    ║   ║   ║                         ║\n" +
       "║       ║   ║   ║     ║                   ║             ║   ║   ║    ═════════════     ═══    ║\n" +
       "║           ║   ║                         ║   ╔═════╗   ║   ║   ║                 ║   ║       ║\n" +
       "║═══════════     ════════════════════     ║   ╚═════╝   ║   ║   ║    ═════════════     ═══════║\n" +
       "║                                    ║    ║                 ║   ║                             ║\n" +
       "║   ║     ═══     ═══════════════    ║══════════════════     ═════════════════════════        ║\n" +
       "║   ║    ║   ║             ║     ║   ║                  ║                      ║      ║   ║   ║\n" +
       "║   ║    ║   ║   ╔═════╗   ║     ║   ║                  ║    ══════════════════║   ║  ║   ║   ║\n" +
       "║   ║    ║   ║   ╚═════╝   ║     ║    ══════════════    ║                      ║   ║  ║   ║   ║\n" +
       "║        ║   ║             ║     ║                  ║══════════════════    ║   ║   ║  ║   ║   ║\n" +
       "╠══════════════════════     ═════     ══════════              ║        ║   ║   ║    ══    ║   ║\n" +
       "║                          ║                    ║   ╔═════╗   ║    ║   ║   ║   ║          ║   ║\n" +
       "║    ═══════════════════════════════════════════    ╚═════╝   ║     ═══    ║    ══════════    ║\n" +
       "║                                                                          ║                  ║\n" +
       "╚═════════════════════════════════════════════════════════════════════════════════════════════╝";

    public string[] CharAnimations =
    {
        "// //",
        "@@ @@",
        "|| ||",
        "00 00",
    };
    #endregion

    public (int X, int Y) CharPosition;
    public Direction? CharMovDirection = default;
    public int? CharMovFrame = default;

    public Przeciwnik[] Enemies = Array.Empty<Przeciwnik>();
    public Drzwi[] Drzwi = Array.Empty<Drzwi>();
    public Dzwignia[] Dzwignia = Array.Empty<Dzwignia>();
    public Pulapka[] Pulapka = Array.Empty<Pulapka>();
    public Skarby[] Skarby = Array.Empty<Skarby>();
    public Klucz[] Klucz = Array.Empty<Klucz>();
    public Mikstura[] Mikstura = Array.Empty<Mikstura>();

    public void RenderEnemies()
    {
        if (Enemies == null) return;
        foreach (Przeciwnik skelly in Enemies)
        {
            if (skelly.Position.X < 0 || skelly.Position.Y < 0) continue;
            Console.SetCursorPosition(skelly.Position.X, skelly.Position.Y);
            WithColors(skelly.Color, ConsoleColor.Red, () => Console.Write('P'));
        }
    }
    public void RenderDrzwi()
    {
        if (Drzwi == null) return;
        foreach (Drzwi d in Drzwi)
        {
            if (d.Position.X < 0 || d.Position.Y < 0) continue;
            Console.SetCursorPosition(d.Position.X, d.Position.Y);
            WithColors(d.Color, ConsoleColor.Black, () => Console.Write('D'));
        }
    }
    public void RenderDzwignia()
    {
        if (Dzwignia == null) return;
        foreach (Dzwignia dzw in Dzwignia)
        {
            if (dzw.Position.X < 0 || dzw.Position.Y < 0) continue;
            Console.SetCursorPosition(dzw.Position.X, dzw.Position.Y);
            WithColors(ConsoleColor.Red, ConsoleColor.Black, () => Console.Write('L'));
        }
    }
    public void RenderPułapki()
    {
        if (Pulapka == null) return;
        foreach (Pulapka pul in Pulapka)
        {
            if (pul.Position.X < 0 || pul.Position.Y < 0) continue;
            Console.SetCursorPosition(pul.Position.X, pul.Position.Y);
            WithColors(ConsoleColor.Red, ConsoleColor.Black, () => Console.Write('T'));
        }
    }
    public void RenderSkarbow()
    {
        if (Skarby == null) return;
        foreach (Skarby skarb in Skarby)
        {
            if (skarb.Position.X < 0 || skarb.Position.Y < 0) continue;
            Console.SetCursorPosition(skarb.Position.X, skarb.Position.Y);
            WithColors(ConsoleColor.Yellow, ConsoleColor.Black, () => Console.Write('S'));
        }
    }
    public void RenderKlucz()
    {
        if (Klucz == null) return;
        foreach (Klucz klucz in Klucz)
        {
            if (klucz.Position.X < 0 || klucz.Position.Y < 0) continue;
            Console.SetCursorPosition(klucz.Position.X, klucz.Position.Y);
            WithColors(ConsoleColor.Yellow, ConsoleColor.Gray, () => Console.Write('K'));
        }
    }
    public void RenderMikstura()
    {
        if (Mikstura == null) return;
        foreach (Mikstura mikstura in Mikstura)
        {
            if (mikstura.Position.X < 0 || mikstura.Position.Y < 0) continue;
            Console.SetCursorPosition(mikstura.Position.X, mikstura.Position.Y);
            WithColors(ConsoleColor.Cyan, ConsoleColor.DarkGreen, () => Console.Write('M'));
        }
    }

    public void UpdateEnemies()
    {
        if (Enemies == null) return;
        foreach (Przeciwnik skelly in Enemies)
        {
            skelly.Update?.Invoke();
        }
    }
}