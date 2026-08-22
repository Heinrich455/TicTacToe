namespace TicTacToe.Core
{
    public class GameSession
    {
        public Board Board { get; } = new Board();
        public CellState CurrentPlayer { get; private set; } = CellState.X;
        public GameStatus Status { get; private set; } = GameStatus.InProgress;

        /// <summary>
        /// Пытается сделать ход по индексу.
        /// </summary>  
        /// <returns>True или False</returns>
        public bool TryMakeMove(int index)
        {
            if (Status != GameStatus.InProgress)
                return false;

            if (!Board.TryPlace(index, CurrentPlayer)) 
                return false;

            var winner = Board.GetWinner();

            if (winner != null)
            {
                Status = winner == CellState.X ? GameStatus.XWin : GameStatus.OWin;
                 
                return true;
            }

            if (Board.IsFull())
            {
                Status = GameStatus.Draw;
                return true;
            }

            CurrentPlayer = CurrentPlayer == CellState.X ? CellState.O : CellState.X;

            return true; 
        }
          
        public void Restart()
        {
            Board.Clear();
            Status = GameStatus.InProgress;
            CurrentPlayer = CellState.X;
        }
    }
}