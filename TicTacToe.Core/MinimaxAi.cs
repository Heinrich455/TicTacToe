namespace TicTacToe.Core
{
    public class MinimaxAi(CellState myMark) : IAi
    { 
        private readonly Dictionary<string, int> _cache = [];
        private readonly CellState _myMark = myMark;

        /// <summary>
        /// Совершает один из лучших ходов.
        /// </summary>  
        /// <returns>Рандомный индекс из лучших клеток для идеального хода</returns>
        public int ChooseMove(Board board)
        { 
            var bestMoves = new List<int>();
            int bestScore = int.MinValue;

            foreach (int move in board.GetEmptyCells())
            { 
                board.TryPlace(move, _myMark);
                 
                CellState opponent = _myMark == CellState.X ? CellState.O : CellState.X;
                int score = Minimax(board, opponent);
                 
                board.UndoMove(move);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMoves.Clear();
                    bestMoves.Add(move);
                }

                else if (score == bestScore) 
                    bestMoves.Add(move); 
            }

            return bestMoves[Random.Shared.Next(bestMoves.Count)];
        }
         
        private int Minimax(Board board, CellState currentPlayer)
        { 
            CellState? winner = board.GetWinner();

            if (winner != null) 
                return winner.Value == _myMark ? +1 : -1; 

            if (board.IsFull())
                return 0;

            string cacheKey = GetCacheKey(board, currentPlayer);

            if (_cache.TryGetValue(cacheKey, out int cachedScore))
                return cachedScore;

            int bestScore;

            if (currentPlayer == _myMark)
            { 
                bestScore = int.MinValue;

                foreach (int move in board.GetEmptyCells())
                {
                    board.TryPlace(move, currentPlayer);
                    CellState nextPlayer = currentPlayer == CellState.X ? CellState.O : CellState.X;

                    int score = Minimax(board, nextPlayer);
                    board.UndoMove(move);

                    if (score > bestScore)
                        bestScore = score;
                } 
            }

            else
            { 
                bestScore = int.MaxValue;

                foreach (int move in board.GetEmptyCells())
                {
                    board.TryPlace(move, currentPlayer);
                    CellState nextPlayer = currentPlayer == CellState.X ? CellState.O : CellState.X;

                    int score = Minimax(board, nextPlayer);
                    board.UndoMove(move);

                    if (score < bestScore)
                        bestScore = score;
                }

                
            }
             
            _cache[cacheKey] = bestScore;
            return bestScore;
        }

        private static string GetCacheKey(Board board, CellState currentPlayer)
            => BoardTransformations.GetCanonicalKey(board, currentPlayer);
    }
} 