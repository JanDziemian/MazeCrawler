using System;
using System.Collections.Generic;

public class Battle
{
    public static void Positions(Gracz gracz, Przeciwnik przeciwnik)
    {
        if (gracz.CharPosition == przeciwnik.Position)
        {
            Console.Clear();
            if (przeciwnik.Zdrowie <= 0)
            {
                Console.WriteLine("Przeciwnik nie żyje...");
                Console.Read();
                Console.Clear();
            }
            else
            {
                StartFight(gracz, przeciwnik);
            }
        }
    }

    public static void StartFight(Stworzenie gracz, Stworzenie skelly)
    {
        while (true)
        {
            if (GetAttackResult(gracz, skelly) == "Koniec Walki")
            {
                Console.WriteLine("Koniec Walki");
                Console.ReadLine();
                Console.Clear();
                break;
            }
            if (GetAttackResult(skelly, gracz) == "Koniec Walki")
            {
                Console.WriteLine("Koniec Walki");
                Console.ReadLine();
                Console.Clear();
                break;
            }
        }
    }

    public static string GetAttackResult(Stworzenie stworzenieA, Stworzenie stworzenieB)
    {
        int damage = stworzenieA.Atak;
        if (damage > 0)
        {
            stworzenieB.Zdrowie -= damage;
        }
        else damage = 0;

        Console.WriteLine("{0} atakuje {1} i zadaje {2} obrażeń!", stworzenieA.Imie, stworzenieB.Imie, damage);
        Console.WriteLine("{0} posiada {1} punktów zdrowia\n", stworzenieB.Imie, stworzenieB.Zdrowie);

        if (stworzenieB.Zdrowie <= 0)
        {
            Console.WriteLine("{1} umiera, a {0} zwycięża!! \n", stworzenieA.Imie, stworzenieB.Imie);
            if (stworzenieB is Przeciwnik skelly)
            {
                skelly.Position = (-1, -1);
                skelly.MovementPattern = new List<(int X, int Y)> { (-1, -1) };
            }
            return "Koniec Walki";
        }
        else return "Wznów Walkę";
    }
}