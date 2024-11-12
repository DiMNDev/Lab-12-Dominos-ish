using lab_12.Library;

namespace TheGame;

public class Game
{
    public event Action? GameReset;
    public event Action? GameStateChanged;

    public Game()
    {
        Reset();
    }

    public void Reset()
    {
        PlayerOne = null;
        PlayerTwo = null;
        Board = new List<Tile> { new Tile(1, 1) };
        GameReset?.Invoke();
    }

    public void Join(Player player)
    {
        if (PlayerOne is null)
        {
            PlayerOne = player;
        }
        else if (PlayerTwo is null)
        {
            PlayerTwo = player;
        }
        else
        {
            throw new GameFullException();
        }
        GameStateChanged?.Invoke();
    }
    public static Game Instance { get; set; }
    public Player? PlayerOne { get; private set; }
    public Player? PlayerTwo { get; private set; }
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
    public bool IsGameOver => NoOneCanPlay || PlayerOne?.Tiles.Count() == 0 || PlayerTwo?.Tiles.Count() == 0;

    public bool IsPlayable => PlayerOne is not null && PlayerTwo is not null && IsGameOver is false;
    public void PlayTile(Player player, Tile tile)
    {
        if (player.Tiles.Contains(tile) == false)
        {
            throw new InvalidMoveException();
        }

        var numtomatch = Board.Last().Num2;

        if (tile.Num1 == numtomatch)
        {
            Board.Add(tile);
            player.Tiles.Remove(tile);
        }
        else if (tile.Num2 == numtomatch)
        {
            player.Tiles.Remove(tile);
            Board.Add(new Tile(tile.Num2, tile.Num1));
        }
        else
        { throw new InvalidMoveException(); }

        GameStateChanged?.Invoke();
    }
}
