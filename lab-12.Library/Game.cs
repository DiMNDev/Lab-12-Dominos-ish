using lab_12.Library;

namespace TheGame;

public class Game
{
    public static Game Instance { get; set; }
    public Player PlayerOne { get; private set; }
    public Player PlayerTwo { get; private set; }
    public List<Tile> Board { get; private set; }
    public bool NoOneCanPlay
    {
        get
        {
            if (PlayerOne is null || PlayerTwo is null)
                return false;

            var player1CanPlay = PlayerOne.HasMatchFor(Board.Last());
            var player2CanPlay = PlayerOne.HasMatchFor(Board.Last());
            return !player1CanPlay && !player2CanPlay;
        }
    }
}