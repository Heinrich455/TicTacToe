namespace TicTacToe.Core
{
    public class MixedAi(CellState myMark, double probability) : IAi
    {
        private readonly MinimaxAi _minimaxAi = new(myMark);
        private readonly RandomAi _randomAi = new(myMark);
        private readonly double _probability = probability;
        private const double DefaultProbability = 0.7;

        /// <summary>
        /// Совершает либо лучший ход либо рандомный.
        /// </summary>  
        /// <returns>Рандомный или лучший индекс клетки для хода</returns>
        public int ChooseMove(Board board)
        {
            double randomValue = Random.Shared.NextDouble();

            if (randomValue < _probability)  
                return _minimaxAi.ChooseMove(board); 

            else 
                return _randomAi.ChooseMove(board); 
        }

        public MixedAi(CellState myMark) : this(myMark, DefaultProbability) { }
    }
} 