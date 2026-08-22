namespace TicTacToe.Core
{
    public class RandomAi(CellState myMark) : IAi
    {
        private readonly CellState _myMark = myMark;
         
        /// <summary>
        /// Совершает рандомный ход.
        /// </summary>  
        /// <returns>Рандомный индекс клетки для хода</returns>
        public int ChooseMove(Board board)
        {
            var emptyCells = board.GetEmptyCells();

            if (emptyCells.Count == 0) 
                throw new InvalidOperationException("No empty cells");

            int move = emptyCells[Random.Shared.Next(emptyCells.Count)];

            return move;
        } 
    }
}