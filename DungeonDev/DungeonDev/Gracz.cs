using System;
using System.Threading;

public class Gracz : Stworzenie
{
    public int Punkty { get; set; }
    public (int X, int Y) CharPosition;

    public Gracz(string imie, int zdrowie, int atak, GenerujLabirynt labirynt, int punkty)
        : base(zdrowie, atak, imie)
    {
        Labirynt = labirynt;
        Punkty = punkty;
    }

    public void UpdateLabirynt(GenerujLabirynt labirynt)
    {
        Labirynt = labirynt;
    }

    public void MoveChar(Direction direction)
    {
        int x_adjust = direction == Direction.Left ? -1 : direction == Direction.Right ? 1 : 0;
        int y_adjust = direction == Direction.Up ? -1 : direction == Direction.Down ? 1 : 0;
        if (CanMove(CharPosition.X + x_adjust, CharPosition.Y + y_adjust))
        {
            Console.SetCursorPosition(CharPosition.X, CharPosition.Y);
            Console.Write(" ");
            CharPosition = (CharPosition.X + x_adjust, CharPosition.Y + y_adjust);
        }
        var (width, height) = Labirynt.GetBoardDimensions();
        if (CharPosition.X < 0) CharPosition.X = width - 1;
        if (CharPosition.X >= width) CharPosition.X = 0;
        if (CharPosition.Y < 0) CharPosition.Y = height - 1;
        if (CharPosition.Y >= height) CharPosition.Y = 0;

        RenderPlayer();
        CheckInteractions();
    }

    public void RenderPlayer()
    {
        Console.SetCursorPosition(CharPosition.X, CharPosition.Y);
        Labirynt.WithColors(ConsoleColor.Black, ConsoleColor.Blue, () =>
        {
            if (Labirynt.CharMovDirection.HasValue && Labirynt.CharMovFrame.HasValue)
            {
                int frame = (int)Labirynt.CharMovFrame % Labirynt.CharAnimations[(int)Labirynt.CharMovDirection].Length;
                Console.Write(Labirynt.CharAnimations[(int)Labirynt.CharMovDirection][frame]);
            }
            else
            {
                Console.Write(Labirynt.CharAnimations[0][0]);
            }
        });
    }

    public bool CanMove(int x, int y)
    {
        if (IsWall(x, y))
            return false;

        if (Labirynt.Drzwi != null)
        {
            foreach (var drzwi in Labirynt.Drzwi)
            {
                if (drzwi.Position == (x, y) && !drzwi.Otwarte)
                {
                    drzwi.Aktywuj(this);
                    return false;
                }
            }
        }

        return true;
    }

    private GameState gameState = new GameState();

    public bool HandleInput()
    {
        while (Console.KeyAvailable)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow: Labirynt.CharMovDirection = Direction.Up; MoveChar(Direction.Up); break;
                case ConsoleKey.DownArrow: Labirynt.CharMovDirection = Direction.Down; MoveChar(Direction.Down); break;
                case ConsoleKey.LeftArrow: Labirynt.CharMovDirection = Direction.Left; MoveChar(Direction.Left); break;
                case ConsoleKey.RightArrow: Labirynt.CharMovDirection = Direction.Right; MoveChar(Direction.Right); break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.Write("Program was closed.");
                    return true;
                case ConsoleKey.S:
                    gameState.Player = this;
                    gameState.Enemies = Labirynt.Enemies;
                    gameState.Traps = Labirynt.Pulapka;
                    gameState.Treasures = Labirynt.Skarby;

                    var gameSaveLoad = new GameSaveLoad();
                    gameSaveLoad.SaveGame(gameState);
                    Console.Clear();
                    Console.WriteLine("Gra została zapisana.");
                    Thread.Sleep(500);
                    break;
            }
            Labirynt.CharMovFrame = (Labirynt.CharMovFrame ?? 0) + 1;
        }
        return false;
    }

    public void CheckInteractions()
    {
        if (Labirynt.Drzwi != null)
            foreach (var drzwi in Labirynt.Drzwi) if (CharPosition == drzwi.Position) drzwi.Aktywuj(this);

        if (Labirynt.Dzwignia != null)
            foreach (var dzwignia in Labirynt.Dzwignia) if (CharPosition == dzwignia.Position) dzwignia.Uzyj(this);

        if (Labirynt.Pulapka != null)
            foreach (var pulapka in Labirynt.Pulapka) if (CharPosition == pulapka.Position) pulapka.Aktywuj(this);

        if (Labirynt.Skarby != null)
            foreach (var skarb in Labirynt.Skarby) if (CharPosition == skarb.Position) skarb.Aktywuj(this);

        if (Labirynt.Enemies != null)
            foreach (var skelly in Labirynt.Enemies) if (CharPosition == skelly.Position) Battle.Positions(this, skelly);

        if (Labirynt.Mikstura != null)
            foreach (var mikstura in Labirynt.Mikstura) if (CharPosition == mikstura.Position) mikstura.Uzyj(this);

        if (Labirynt.Klucz != null)
            foreach (var klucz in Labirynt.Klucz) if (CharPosition == klucz.Position) klucz.Uzyj(this);
    }
}