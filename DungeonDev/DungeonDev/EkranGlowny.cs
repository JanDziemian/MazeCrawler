using System;

public class EkranGlowny
{
    public void PokazEkranStartowy()
    {
        Console.Clear();
        Console.WriteLine("Witaj w grze! Wciśnij klawisz Enter, aby rozpocząć.");
        Console.ReadLine();
        Console.Clear();
    }

    public void Menu()
    {
        GameSaveLoad gameSaveLoad = new GameSaveLoad();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Rozpocznij grę");
            Console.WriteLine("2. Instrukcja");
            Console.WriteLine("3. Wczytaj grę");
            Console.WriteLine("4. Wciśnij Escape, aby zakończyć grę.");

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.Clear();
                    WyswietlInstrukcje();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    GameState gameState = gameSaveLoad.LoadGame();
                    if (gameState == null)
                    {
                        Console.WriteLine("Nie znaleziono pliku do odczytu.");
                    }
                    Console.ReadLine();
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine("Program został zamknięty.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Spróbuj ponownie wybrać opcję. Naciśnij Enter, aby powrócić do Menu!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    public void WyswietlInstrukcje()
    {
        Console.Clear();
        Console.WriteLine("Instrukcja gry:");
        Console.WriteLine("1. Użyj strzałek, aby poruszać się po labiryncie.");
        Console.WriteLine("2. Unikaj przeciwników i pułapek.");
        Console.WriteLine("3. Zbieraj skarby i przedmioty.");
        Console.WriteLine("4. Wciśnij Escape, aby zakończyć grę.");
        Console.WriteLine("\nWciśnij klawisz Enter, aby kontynuować.");
        Console.ReadLine();
    }
}