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
}