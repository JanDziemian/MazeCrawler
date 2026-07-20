using System;
using System.Collections.Generic;

public class Przeciwnik : Stworzenie
{
    public Battle Battle { get; set; }
    public (int X, int Y) StartPosition;
    public (int X, int Y) Position;
    public ConsoleColor Color;
    public Action? Update;
    public int UpdateFrame;
    public int FramesToUpdate;
    public List<(int X, int Y)> MovementPattern = new List<(int X, int Y)>();

    public Przeciwnik(string imie, int zdrowie, int atak, GenerujLabirynt labirynt, Battle battle)
        : base(zdrowie, atak, imie)
    {
        Labirynt = labirynt;
        Battle = battle;
    }

    public void UpdateEnemy(Przeciwnik skelly)
    {
        if (skelly.MovementPattern == null || skelly.MovementPattern.Count == 0) return;

        if (skelly.UpdateFrame < skelly.FramesToUpdate)
        {
            skelly.UpdateFrame++;
        }
        else
        {
            if (skelly.Position.X >= 0 && skelly.Position.Y >= 0)
            {
                Console.SetCursorPosition(skelly.Position.X, skelly.Position.Y);
                Console.Write(' ');
            }

            int currentIndex = skelly.MovementPattern.IndexOf(skelly.Position);
            if (currentIndex == -1) currentIndex = 0;
            int nextIndex = (currentIndex + 1) % skelly.MovementPattern.Count;
            var nextPosition = skelly.MovementPattern[nextIndex];

            if (!IsWall(nextPosition.X, nextPosition.Y))
            {
                skelly.Position = nextPosition;
            }

            skelly.UpdateFrame = 0;
        }
    }
}