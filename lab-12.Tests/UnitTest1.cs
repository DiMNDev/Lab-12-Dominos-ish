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
}