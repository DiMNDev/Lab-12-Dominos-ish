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
        game.Join(new Player("P1",0));
        game.Join(new Player("P2",0));
        var tileToPlay = new Tile(1, game.Board.First().Num1);
         game.Board.Count().Should().Be(1);
        
        // When
    FluentActions.Invoking(()=> game.PlayTile(game.PlayerOne, tileToPlay)).Should().Throw<InvalidMoveException>();
        // Then
         game.Board.Last().Should().BeEquivalentTo(tileToPlay);
    }  

     [Fact]
 public void Player1Wins()
 {
     var game = new Game();
     var player1 = new Player("P1", 7);
     var player2 = new Player("P2", 7);
     game.Join(player1);
     game.Join(player2);

     player1.Tiles.Clear();
     player1.Tiles.Add(new Tile(1, 2));
     player1.Tiles.Add(new Tile(2, 3));
     player1.Tiles.Add(new Tile(3, 4));
     var expectedBoardLength = 1 + player1.Tiles.Count;

     while (player1.Tiles.Any())
     {
         game.PlayTile(player1, player1.Tiles.First());
     }

     game.Board.Count.Should().Be(expectedBoardLength);
     game.IsPlayable.Should().BeFalse();
     game.Winner.Should().Be(player1);
     game.IsGameOver.Should().BeTrue();
 }   
}