namespace TicTacToe.Core
{
    public class BoardTransformations
    {  
        // Все равнозначные позиции доски. 
        private static readonly int[][] Transformations =
        [
            [0, 1, 2, 3, 4, 5, 6, 7, 8], // оригинал
            [6, 3, 0, 7, 4, 1, 8, 5, 2], // 90° 
            [8, 7, 6, 5, 4, 3, 2, 1, 0], // 180°
            [2, 5, 8, 1, 4, 7, 0, 3, 6], // 270° 

            [6, 7, 8, 3, 4, 5, 0, 1, 2], // отражение горизонтальное
            [2, 1, 0, 5, 4, 3, 8, 7, 6], // отражение вертикальное

            [0, 3, 6, 1, 4, 7, 2, 5, 8], // отражение по диагонали \
            [8, 5, 2, 7, 4, 1, 6, 3, 0], // отражение по диагонали /
        ];
         
        // Генерирует 8 трансформаций позиции. 
        private static IEnumerable<string> GetAllTransformations(Board board, CellState currentPlayer)
        {
            foreach (var transform in Transformations)
            {
                yield return BuildKey(board, currentPlayer, transform);
            }
        }

        /// <summary>
        /// Возвращает канонический (минимальный по кодировке) ключ позиции.
        /// </summary>
        public static string GetCanonicalKey(Board board, CellState currentPlayer)
        {
            string? minKey = null; 

            foreach (var key in GetAllTransformations(board, currentPlayer))
            {
                if (minKey == null || string.CompareOrdinal(key, minKey) < 0)
                {
                    minKey = key;
                }
            }

            return minKey!;
        }

        private static string BuildKey(Board board, CellState currentPlayer, int[] transform)
        {
            var sb = new System.Text.StringBuilder(11);

            foreach (int index in transform)
            {
                sb.Append(board[index] switch
                {
                    CellState.Empty => '.',
                    CellState.X => 'X',
                    CellState.O => 'O',
                    _ => '?'
                });
            }

            sb.Append('|');
            sb.Append(currentPlayer == CellState.X ? 'X' : 'O');

            return sb.ToString();
        }
    }
} 