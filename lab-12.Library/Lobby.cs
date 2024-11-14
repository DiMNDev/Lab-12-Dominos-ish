namespace lab_12.Lobby;
using TheGame;

public static class Lobby
{
    private static Dictionary<string, Game> games = new();
    public static bool CreateGame(string gameName)
    {
        var newGame = new Game();
        var gameAddedOk = games.TryAdd(gameName, newGame);
        if (gameAddedOk)
        {
            LobbyChanged?.Invoke();
            newGame.GameStateChanged += () => LobbyChanged?.Invoke();
        }
        return gameAddedOk;
    }
    public static event Action LobbyChanged;

    public static Game? GetGame(string gameName)
    {
        return games.ContainsKey(gameName) ? games[gameName] : null;
    }

    public static IEnumerable<(string Name, Game Game)> ActiveGames => games
                                                        .Where(g => g.Value.PlayerTwo is not null && g.Value.IsGameOver is false)
                                                        .Select(g => (g.Key, g.Value));
    public static IEnumerable<string> OpenGameNames => games.Where(g => g.Value.PlayerTwo == null).Select(g => g.Key);
}