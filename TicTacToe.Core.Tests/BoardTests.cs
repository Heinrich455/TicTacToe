namespace TicTacToe.Core.Tests
{
    public class BoardTests
    {
        [Fact]
        public void TryPlace_EmptyBoard_CellBecomesX()
        {
            Board board = new();

            bool result = board.TryPlace(0, CellState.X);
            Assert.True(result);
            Assert.Equal(CellState.X, board[0]);
        }

        [Fact]
        public void TryPlace_OccupiedCell_DoesNotChangeCell()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            bool result = board.TryPlace(0, CellState.O);

            Assert.False(result);
            Assert.Equal(CellState.X, board[0]);
        }

        [Fact]
        public void IsEmpty_EmptyBoard_ReturnsTrue()
        {
            Board board = new();

            Assert.True(board.IsEmpty(0));
        }

        [Fact]
        public void IsEmpty_OccupiedCell_ReturnsFalse()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);

            Assert.False(board.IsEmpty(0));
        }

        [Fact]
        public void TryPlace_EmptyMark_ReturnsFalse()
        {
            Board board = new();

            bool result = board.TryPlace(0, CellState.Empty);
            Assert.False(result);
        }

        [Fact]
        public void TryPlace_OutOfBoard_ReturnsFalse()
        {
            Board board = new();

            bool result1 = board.TryPlace(-1, CellState.X);
            Assert.False(result1);

            bool result2 = board.TryPlace(9, CellState.X);
            Assert.False(result2);
        }

        [Fact]
        public void IsEmpty_InvalidIndex_ReturnsFalse()
        {
            Board board = new();

            Assert.False(board.IsEmpty(-1));
            Assert.False(board.IsEmpty(9));
        }

        [Fact]
        public void Indexer_ValidIndex_ReturnsCellState()
        {
            Board board = new();

            board.TryPlace(1, CellState.X);

            Assert.Equal(CellState.X, board[1]);
        }

        [Fact]
        public void Indexer_InvalidIndex_ThrowsException()
        {
            Board board = new();

            Assert.Throws<ArgumentOutOfRangeException>(() => board[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => board[9]);
        }

        [Fact]
        public void GetWinner_EmptyBoard_ReturnsNull()
        {
            Board board = new();

            Assert.Null(board.GetWinner());
        }

        [Fact]
        public void GetWinner_MixedTopLine_ReturnsNull()
        {
            Board board = new();

            board.TryPlace(0, CellState.X);
            board.TryPlace(1, CellState.O);
            board.TryPlace(2, CellState.X);

            Assert.Null(board.GetWinner());
        }

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(3, 4, 5)]
        [InlineData(6, 7, 8)]

        [InlineData(0, 3, 6)]
        [InlineData(1, 4, 7)]
        [InlineData(2, 5, 8)]

        [InlineData(0, 4, 8)]
        [InlineData(2, 4, 6)]
        public void GetWinner_WinningLine_ReturnsX(int a, int b, int c)
        {
            Board board = new();

            board.TryPlace(a, CellState.X);
            board.TryPlace(b, CellState.X);
            board.TryPlace(c, CellState.X);

            var result = board.GetWinner();
            Assert.Equal(CellState.X, result);
        }

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(3, 4, 5)]
        [InlineData(6, 7, 8)]

        [InlineData(0, 3, 6)]
        [InlineData(1, 4, 7)]
        [InlineData(2, 5, 8)]

        [InlineData(0, 4, 8)]
        [InlineData(2, 4, 6)]
        public void GetWinner_WinningLine_ReturnsO(int a, int b, int c)
        {
            Board board = new();

            board.TryPlace(a, CellState.O);
            board.TryPlace(b, CellState.O);
            board.TryPlace(c, CellState.O);

            var result = board.GetWinner();
            Assert.Equal(CellState.O, result);
        }

        [Fact]
        public void IsFull_EmptyBoard_ReturnsFalse()
        {
            Board board = new();

            Assert.False(board.IsFull());
        }

        [Fact]
        public void IsFull_DrawBoard_ReturnsTrue()
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

            Assert.True(board.IsFull());
            Assert.Null(board.GetWinner());
        }

        [Fact]
        public void GetEmptyCells_EmptyBoard_ReturnsAllCells()
        {
            Board board = new();
            List<int> expected = [0, 1, 2, 3, 4, 5, 6, 7, 8];

            var result = board.GetEmptyCells();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetEmptyCells_OccupiedCells_ReturnsOnlyEmptyCells()
        {
            Board board = new();
            List<int> expected = [1, 2, 3, 4, 6, 7, 8];

            board.TryPlace(0, CellState.X);
            board.TryPlace(5, CellState.O);

            var result = board.GetEmptyCells();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Clear_OccupiedBoard_MakesAllCellsEmpty()
        {
            Board board = new();
            List<int> expected = [0, 1, 2, 3, 4, 5, 6, 7, 8];

            board.TryPlace(0, CellState.X);
            board.TryPlace(5, CellState.O);

            board.Clear();

            var result = board.GetEmptyCells();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void UndoMove_MakesOccupiedCellEmpty()
        {
            Board board = new();
            board.TryPlace(2, CellState.X);

            board.UndoMove(2);

            Assert.True(board.IsEmpty(2));
        }

        [Fact]
        public void UndoMove_CellRemainsEmpty()
        {
            Board board = new();
            board.UndoMove(2);

            Assert.True(board.IsEmpty(2));
        }

        [Fact]
        public void UndoMove_InvalidIndex_ThrowsException()
        {
            Board board = new();

            Assert.Throws<ArgumentOutOfRangeException>(() => board.UndoMove(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => board.UndoMove(9));
        }
    }
}