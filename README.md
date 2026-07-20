#  Console Maze Crawler

Tekstowa gra przygodowa 2D w konsoli, napisana w języku C#. Przemierzaj skomplikowane labirynty, zbieraj skarby, lecz się miksturami, rozwiązuj zagadki z dźwigniami i kluczami oraz walcz z patrolującymi przeciwnikami!

---

##  Funkcje i mechaniki gry

* **Eksploracja labiryntu:** Renderowana w konsoli mapa ASCII z zaawansowaną obsługą kolizji oraz płynnym przejściem między poziomami.
* **Interaktywne obiekty i przeszkody:**
  *  **Drzwi:** Blokują ruch gracza 1 pole przed nimi, dopóki nie zostaną odblokowane.
  * **Klucze &  Dźwignie:** Mechanizmy pozwalające otwierać zamknięte drzwi.
  *  **Mikstury:** Przywracają punkty zdrowia (HP) gracza.
  *  **Skarby:** Zwiększają wynik punktowy.
  *  **Pułapki:** Zadają obrażenia po wdepnięciu na ich pole.
* **Przeciwnicy i system walki:** Przeciwnicy poruszają się po wyznaczonych trasach patrolowych. Wejście na pole wroga uruchamia automatyczną walkę turową.
* **Dynamiczny Log Komunikatów:** Wszystkie powiadomienia (np. o zebraniu przedmiotu, braku klucza czy obrażeniach) wyświetlają się w wydzielonym obszarze pod mapą, nie psując widoku labiryntu.
* **Zapis i Wczytywanie Stany Gry:** W dowolnym momencie gry możesz zapisać postęp (`S`), a następnie wczytać go z poziomu Menu Głównego (`savegame.txt`).

---

##  Sterowanie

| Klawisz | Akcja |
| :--- | :--- |
| **Strzałki (Góra / Dół / Lewo / Prawo)** | Poruszanie się postacią po mapie |
| **S** | Zapisanie stanu gry |
| **Escape** | Wyjście z gry |
| **1 / 2 / 3** | Nawigacja w Menu Głównym (1: Start, 2: Instrukcja, 3: Wczytaj) |

---

##  Architektura projektu

```text
├── Program.cs             # Główna pętla gry, konfiguracja okna i inicjalizacja
├── EkranGlowny.cs         # Menu główne, ekran startowy i instrukcja
├── Log.cs                 # Dedykowany system logowania komunikatów pod mapą
├── Direction.cs           # Enum z kierunkami ruchu
│
├── Stworzenie.cs          # Klasa bazowa (zdrowie, atak, pozycja)
├── Gracz.cs               # Logika gracza, obsługa wejścia, kolizje z drzwiami
├── Przeciwnik.cs          # AI przeciwników i patrolowanie po ścieżce
├── Battle.cs              # System rozstrzygania pojedynków
│
├── GenerujLabirynt.cs     # Renderowanie mapy, wrogów i obiektów
├── Poziom2.cs             # Definicja i układy drugiego poziomu
│
├── Obiekt.cs              # Klasa bazowa obiektów na mapie
├── Drzwi.cs               # Blokada przejścia
├── Dzwignia.cs            # Mechanizm otwierania drzwi
├── Pulapka.cs             # Obiekt zadający obrażenia
├── Skarby.cs              # Obiekt wartościowy (punkty)
│
├── Przedmiot.cs           # Klasa bazowa przedmiotów podnoszonych
├── Klucz.cs               # Przedmiot otwierający drzwi
├── Mikstura.cs            # Przedmiot leczący
│
├── GameState.cs           # Struktura danych zapisywanego stanu
└── GameSaveLoad.cs        # Obsługa zapisu/odczytu z pliku tekstowego
