namespace TicTacToe.Core.Tests
{
    public class MinimaxAiTests
    {
        [Fact]
        public void ChooseMove_SeesWinningLineAndWins()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(3, CellState.O);

            board.TryPlace(1, CellState.X);
            board.TryPlace(4, CellState.O);

            MinimaxAi minimaxAi = new(CellState.X);
            var aiMove = minimaxAi.ChooseMove(board);

            board.TryPlace(aiMove, CellState.X);

            Assert.Equal(CellState.X, board.GetWinner()); 
        }

        [Fact]
        public void ChooseMove_PreventsOpponentFromWinning()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(4, CellState.O);

            board.TryPlace(1, CellState.X); 

            MinimaxAi minimaxAi = new(CellState.O);
            var aiMove = minimaxAi.ChooseMove(board);
             
            board.TryPlace(aiMove, CellState.O); 
            Assert.Equal(CellState.O, board[2]); 
        }

        [Fact]
        public void ChooseMove_WithCache_StillFindsWinningLine()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(3, CellState.O);

            board.TryPlace(1, CellState.X);
            board.TryPlace(4, CellState.O);

            MinimaxAi ai = new(CellState.X);
             
            int move1 = ai.ChooseMove(board);
            int move2 = ai.ChooseMove(board);

            Assert.Equal(2, move1);
            Assert.Equal(2, move2);  
        }

        [Fact]
        public void ChooseMove_MultipleCalls_WithWinningMove_ReturnsSameMove()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(3, CellState.O);

            board.TryPlace(1, CellState.X);
            board.TryPlace(4, CellState.O); 

            MinimaxAi ai = new(CellState.X);

            int firstMove = ai.ChooseMove(board);
            Assert.Equal(2, firstMove);

            for (int i = 0; i < 5; i++)
            {
                int move = ai.ChooseMove(board);
                Assert.Equal(2, move); 
            }
        }
    }
} 