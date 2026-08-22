namespace TicTacToe.Core.Tests
{
    public class RandomAiTests
    {
        [Fact]
        public void ChooseMove_ReturnsOneOfEmptyCells()
        {
            Board board = new(); 
            board.TryPlace(4, CellState.X);
             
            RandomAi randomAi = new(CellState.O);
            var move = randomAi.ChooseMove(board);

            Assert.Contains(move, board.GetEmptyCells()); 
        }

        [Fact]
        public void ChooseMove_FullBoard_ThrowsException()
        {
            Board board = new();

            board.TryPlace(4, CellState.X);
            board.TryPlace(0, CellState.O);
            board.TryPlace(3, CellState.X);
            board.TryPlace(1, CellState.O);
            board.TryPlace(2, CellState.X);
            board.TryPlace(5, CellState.O);
            board.TryPlace(7, CellState.X);
            board.TryPlace(6, CellState.O);
            board.TryPlace(8, CellState.X);

            RandomAi randomAi = new(CellState.O); 
             
            Assert.Throws<InvalidOperationException>(() => randomAi.ChooseMove(board)); 
        }
    }
} 