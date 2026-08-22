namespace TicTacToe.Core.Tests
{
    public class MixedAiTests
    {
        [Fact]
        public void ChooseMove_AlwaysReturnsValidMove()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(4, CellState.O);

            MixedAi mixedAi = new(CellState.X, 0.5);

            for (int i = 0; i < 10; i++)
            {
                int moveAi = mixedAi.ChooseMove(board);
                Assert.Contains(moveAi, board.GetEmptyCells());
            }
        }

        [Fact]
        public void ChooseMove_ProbabilityZero_AlwaysRandom()
        {
            Board board = new();
            MixedAi mixedAi = new(CellState.X, 0);
             
            int move = mixedAi.ChooseMove(board);
            Assert.Contains(move, board.GetEmptyCells());
        }

        [Fact]
        public void ChooseMove_ProbabilityOne_AlwaysMinimax()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(3, CellState.O); 
            board.TryPlace(1, CellState.X);
            board.TryPlace(4, CellState.O);

            MixedAi mixedAi = new(CellState.X, 1);
             
            int move = mixedAi.ChooseMove(board); 
            Assert.Equal(2, move);
        }
    }
} 