public class GameState
{
    public Gracz Player { get; set; }
    public Przeciwnik[] Enemies { get; set; }
    public Pulapka[] Traps { get; set; }
    public Skarby[] Treasures { get; set; }
}