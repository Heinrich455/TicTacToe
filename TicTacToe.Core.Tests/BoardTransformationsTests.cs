namespace TicTacToe.Core.Tests
{
    public class BoardTransformationsTests
    {
        [Fact]
        public void GetCanonicalKey_RotatedPositions_AreEqual()
        {
            Board board1 = new();
            board1.TryPlace(0, CellState.X);
             
            Board board2 = new();
            board2.TryPlace(0, CellState.X);

            Board board3 = new();
            board3.TryPlace(0, CellState.X);

            Board board4 = new();
            board4.TryPlace(0, CellState.X);

            string key1 = BoardTransformations.GetCanonicalKey(board1, CellState.O);
            string key2 = BoardTransformations.GetCanonicalKey(board2, CellState.O);
            string key3 = BoardTransformations.GetCanonicalKey(board3, CellState.O);
            string key4 = BoardTransformations.GetCanonicalKey(board4, CellState.O);

            Assert.Equal(key1, key2);
            Assert.Equal(key1, key3);
            Assert.Equal(key1, key4);
        }

        [Fact]
        public void GetCanonicalKey_MirroredPositions_AreEqual()
        { 
            Board board1 = new();

            board1.TryPlace(4, CellState.X);
            board1.TryPlace(5, CellState.O);
            board1.TryPlace(2, CellState.X);
            board1.TryPlace(6, CellState.O);
            board1.TryPlace(0, CellState.X);
            board1.TryPlace(8, CellState.O);
             
            Board board2 = new();

            board2.TryPlace(4, CellState.X);
            board2.TryPlace(7, CellState.O);
            board2.TryPlace(0, CellState.X);
            board2.TryPlace(8, CellState.O);
            board2.TryPlace(6, CellState.X);
            board2.TryPlace(2, CellState.O);

            string key1 = BoardTransformations.GetCanonicalKey(board1, CellState.X);
            string key2 = BoardTransformations.GetCanonicalKey(board2, CellState.X);

            Assert.Equal(key1, key2);
        }

        [Fact]
        public void GetCanonicalKey_DifferentPositions_AreDifferent()
        { 
            Board board1 = new();
            board1.TryPlace(0, CellState.X);
             
            Board board2 = new();
            board2.TryPlace(1, CellState.X);

            string key1 = BoardTransformations.GetCanonicalKey(board1, CellState.O);
            string key2 = BoardTransformations.GetCanonicalKey(board2, CellState.O);

            Assert.NotEqual(key1, key2);
        }
    }
} 