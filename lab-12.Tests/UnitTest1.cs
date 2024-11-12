namespace lab_12.Tests;
using FluentAssertions;
using Library;
using TheGame;

public class UnitTest1
{
    [Fact]
    public void OppositeTilesAreEqual()
    {
        var t1 = new Tile(1, 2);
        var t2 = new Tile(2, 1);
        t1.Equals(t2).Should().BeTrue();
    }
    [Fact]
    public void IsGameOverShouldBeFalse()
    {
        // Arrange
        Game game = new Game();
        // Act
        var result = game.IsGameOver;
        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public void GameNotPlayableWith1Player()
    {
        // Given
        Game game = new Game();
        // When
        game.Join(new Player("P1"));
        // Then
        game.IsPlayable.Should().BeFalse();
    }
    [Fact]
    public void GameNotPlayableWith2Player()
    {
        // Given
        Game game = new Game();
        // When
        game.Join(new Player("P1"));
        game.Join(new Player("P2"));
        // Then
        game.IsPlayable.Should().BeTrue();
    }
    [Fact]
    public void GameNotPlayableWith3Player()
    {
        // Given
        Game game = new Game();
        // When
        game.Join(new Player("P1"));
        game.Join(new Player("P2"));
        FluentActions.Invoking(()=> game.Join(new Player("P3"))).Should().Throw<GameFullException>();
        // Then
        game.IsPlayable.Should().BeTrue();
        
    }
    [Fact]
    public void PlayATile()
    {
        // Given
        Game game = new Game();
        game.Join(new Player("P1"));
        game.Join(new Player("P2"));
        var tileToPlay = new Tile(1, game.Board.First().Num1);
        game.PlayerOne!.Tiles.Add(tileToPlay);
        // When
        game.PlayTile(game.PlayerOne, tileToPlay);
        // Then
         game.Board.Count().Should().Be(2);
         game.Board.Last().Should().BeEquivalentTo(tileToPlay);
    }  
    [Fact]
    public void TryToPlayATileYouDontHave()
    {
        // Given
        Game game = new Game();
        game.Join(new Player("P1"));
        game.Join(new Player("P2"));
        var tileToPlay = new Tile(1, game.Board.First().Num1);
         game.Board.Count().Should().Be(1);
        
        // When
    FluentActions.Invoking(()=> game.PlayTile(game.PlayerOne, tileToPlay)).Should().Throw<InvalidMoveException>();
        // Then
         game.Board.Last().Should().BeEquivalentTo(tileToPlay);
    }  
}