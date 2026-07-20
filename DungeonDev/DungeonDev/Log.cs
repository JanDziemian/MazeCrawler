using System;
using System.Collections.Generic;

public static class Log
{
    private const int StartY = 27;
    private const int MaxLinii = 5;
    private static readonly Queue<string> Historia = new Queue<string>();

    public static void Pisze(string wiadomosc)
    {
        Historia.Enqueue(wiadomosc);
        if (Historia.Count > MaxLinii)
        {
            Historia.Dequeue();
        }

        Renduj();
    }

    public static void Renduj()
    {
        int currentY = StartY;
        foreach (var linia in Historia)
        {
            Console.SetCursorPosition(0, currentY);
            Console.Write(linia.PadRight(90));
            currentY++;
        }
    }

    public static void Czysc()
    {
        Historia.Clear();
        for (int i = 0; i < MaxLinii; i++)
        {
            Console.SetCursorPosition(0, StartY + i);
            Console.Write(new string(' ', 90));
        }
    }
}