namespace TicTacToe.Core.Tests
{
    public class GameSessionTests
    {
        [Fact]
        public void GameSession_FirstCurrentPlayerIsX()
        {
            GameSession session = new();

            Assert.Equal(CellState.X, session.CurrentPlayer);
        }

        [Fact]
        public void GameSession_AfterMoveXCurrentPlayerIsO()
        {
            GameSession session = new();
            var result = session.TryMakeMove(4);

            Assert.True(result);
            Assert.Equal(CellState.O, session.CurrentPlayer);
        }

        [Fact]
        public void TryMakeMove_OccupiedCell_ReturnsFalse()
        {
            GameSession session = new();

            session.TryMakeMove(4);
            var result = session.TryMakeMove(4);

            Assert.False(result); 
        }

        [Fact]
        public void GameSession_XWinOnTheTopLine()
        {
            GameSession session = new();

            session.TryMakeMove(0);
            session.TryMakeMove(3);
            session.TryMakeMove(1);
            session.TryMakeMove(4);
            session.TryMakeMove(2);
             
            var result = session.Status;
            Assert.Equal(GameStatus.XWin, result);
        }

        [Fact]
        public void GameSession_OWinOnTheTopLine()
        {
            GameSession session = new();

            session.TryMakeMove(8);
            session.TryMakeMove(0);
            session.TryMakeMove(7);
            session.TryMakeMove(1);
            session.TryMakeMove(4);
            session.TryMakeMove(2);

            var result = session.Status;
            Assert.Equal(GameStatus.OWin, result);  
        }

        [Fact]
        public void GameSession_DrawOnBoard()
        {
            GameSession session = new();
             
            session.TryMakeMove(4);
            session.TryMakeMove(0);
            session.TryMakeMove(3);
            session.TryMakeMove(1);
            session.TryMakeMove(2);
            session.TryMakeMove(5);
            session.TryMakeMove(7);
            session.TryMakeMove(6);
            session.TryMakeMove(8);

            var result = session.Status; 
            Assert.Equal(GameStatus.Draw, result);
        }

        [Fact]
        public void TryMakeMove_AfterVictory_ReturnsFalse()
        {
            GameSession session = new();

            session.TryMakeMove(0);
            session.TryMakeMove(3);
            session.TryMakeMove(1);
            session.TryMakeMove(4);
            session.TryMakeMove(2);

            var result = session.TryMakeMove(5);

            Assert.False(result);
        }

        [Fact]
        public void GameSession_Restart()
        {
            GameSession session = new();

            session.TryMakeMove(0);
            session.TryMakeMove(3);
            session.TryMakeMove(1);
            session.TryMakeMove(4);
            session.TryMakeMove(2);
            
            session.Restart();

            Assert.Equal(GameStatus.InProgress, session.Status);
            Assert.Equal(CellState.X, session.CurrentPlayer);
            Assert.Equal(9, session.Board.GetEmptyCells().Count);
        }
    }
} 