using System;
using System.Collections.Generic;
using System.IO;

public class GameSaveLoad
{
    private const string SaveFilePath = "savegame.txt";

    public void SaveGame(GameState gameState)
    {
        if (gameState == null || gameState.Player == null) return;

        try
        {
            using (StreamWriter writer = new StreamWriter(SaveFilePath))
            {
                writer.WriteLine(gameState.Player.Imie);
                writer.WriteLine(gameState.Player.Zdrowie);
                writer.WriteLine(gameState.Player.Atak);
                writer.WriteLine(gameState.Player.Punkty);
                writer.WriteLine(gameState.Player.CharPosition.X);
                writer.WriteLine(gameState.Player.CharPosition.Y);

                int enemyCount = gameState.Enemies?.Length ?? 0;
                writer.WriteLine(enemyCount);
                if (gameState.Enemies != null)
                {
                    foreach (var enemy in gameState.Enemies)
                    {
                        writer.WriteLine(enemy.Imie);
                        writer.WriteLine(enemy.Zdrowie);
                        writer.WriteLine(enemy.Atak);
                        writer.WriteLine(enemy.Position.X);
                        writer.WriteLine(enemy.Position.Y);
                        writer.WriteLine(enemy.FramesToUpdate);

                        foreach (var (X, Y) in enemy.MovementPattern)
                        {
                            writer.WriteLine($"{X},{Y}");
                        }
                        writer.WriteLine("END");
                    }
                }

                int trapCount = gameState.Traps?.Length ?? 0;
                writer.WriteLine(trapCount);
                if (gameState.Traps != null)
                {
                    foreach (var trap in gameState.Traps)
                    {
                        writer.WriteLine(trap.Position.X);
                        writer.WriteLine(trap.Position.Y);
                        writer.WriteLine(trap.Obrazenia);
                    }
                }

                int treasureCount = gameState.Treasures?.Length ?? 0;
                writer.WriteLine(treasureCount);
                if (gameState.Treasures != null)
                {
                    foreach (var treasure in gameState.Treasures)
                    {
                        writer.WriteLine(treasure.Position.X);
                        writer.WriteLine(treasure.Position.Y);
                        writer.WriteLine(treasure.Wartosc);
                    }
                }
            }
            Console.WriteLine("Gra została zapisana.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd podczas zapisywania stanu gry: " + ex.Message);
        }
    }

    public GameState LoadGame()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
            {
                Console.WriteLine("Brak zapisanego stanu gry.");
                return null;
            }

            GameState gameState = new GameState();
            GenerujLabirynt labirynt = new GenerujLabirynt();
            Battle battle = new Battle();

            using (StreamReader reader = new StreamReader(SaveFilePath))
            {
                string imie = reader.ReadLine();
                int zdrowie = int.Parse(reader.ReadLine());
                int atak = int.Parse(reader.ReadLine());
                int punkty = int.Parse(reader.ReadLine());
                int posX = int.Parse(reader.ReadLine());
                int posY = int.Parse(reader.ReadLine());

                gameState.Player = new Gracz(imie, zdrowie, atak, labirynt, punkty)
                {
                    CharPosition = (posX, posY)
                };

                int enemyCount = int.Parse(reader.ReadLine());
                gameState.Enemies = new Przeciwnik[enemyCount];
                for (int i = 0; i < enemyCount; i++)
                {
                    string enemyImie = reader.ReadLine();
                    int enemyZdrowie = int.Parse(reader.ReadLine());
                    int enemyAtak = int.Parse(reader.ReadLine());
                    int ePosX = int.Parse(reader.ReadLine());
                    int ePosY = int.Parse(reader.ReadLine());
                    int framesToUpdate = int.Parse(reader.ReadLine());

                    Przeciwnik enemy = new Przeciwnik(enemyImie, enemyZdrowie, enemyAtak, labirynt, battle)
                    {
                        Position = (ePosX, ePosY),
                        FramesToUpdate = framesToUpdate,
                        MovementPattern = new List<(int X, int Y)>()
                    };

                    string line;
                    while ((line = reader.ReadLine()) != "END")
                    {
                        string[] parts = line.Split(',');
                        enemy.MovementPattern.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                    }

                    gameState.Enemies[i] = enemy;
                }

                int trapCount = int.Parse(reader.ReadLine());
                gameState.Traps = new Pulapka[trapCount];
                for (int i = 0; i < trapCount; i++)
                {
                    int x = int.Parse(reader.ReadLine());
                    int y = int.Parse(reader.ReadLine());
                    int obrazenia = int.Parse(reader.ReadLine());

                    gameState.Traps[i] = new Pulapka((x, y), obrazenia);
                }

                int treasureCount = int.Parse(reader.ReadLine());
                gameState.Treasures = new Skarby[treasureCount];
                for (int i = 0; i < treasureCount; i++)
                {
                    int x = int.Parse(reader.ReadLine());
                    int y = int.Parse(reader.ReadLine());
                    int wartosc = int.Parse(reader.ReadLine());

                    gameState.Treasures[i] = new Skarby((x, y), wartosc);
                }
            }

            Console.WriteLine("Gra została wczytana.");
            return gameState;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd podczas wczytywania stanu gry: " + ex.Message);
            return null;
        }
    }
}