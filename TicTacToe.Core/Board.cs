namespace TicTacToe.Core
{
    public class Board
    {
        private const int CellCount = 9;

        private readonly CellState[] _cells = new CellState[CellCount];

        /// <summary>
        /// Победные линии на поле.
        /// </summary>  
        private static readonly int[][] _winningLines =
        [
            [0, 1, 2],
            [3, 4, 5],
            [6, 7, 8],

            [0, 3, 6],
            [1, 4, 7],
            [2, 5, 8],

            [0, 4, 8],
            [2, 4, 6]
        ];

        /// <summary>
        /// Получает пустые клетки поля.
        /// </summary>  
        /// <returns>Список пустых клеток</returns>
        public IReadOnlyList<int> GetEmptyCells()
        {
            var list = new List<int>(CellCount);

            for(int i = 0; i < CellCount; i++)
            {
                if(IsEmpty(i))
                {
                    list.Add(i);
                }
            }

            return list;
        }

        /// <summary>
        /// Проверяет, что индекс входит в диапозон поля.
        /// </summary>  
        /// <returns>True или False</returns>
        private static bool IsValidIndex(int index) => index >= 0 && index < CellCount; 

        public CellState this[int index]
        {
            get
            {
                if (!IsValidIndex(index))
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _cells[index];
            }
        }

        /// <summary>
        /// Проверяет, что клетка по индексу пуста.
        /// </summary>  
        /// <returns>True или False</returns>
        public bool IsEmpty(int index) => IsValidIndex(index) && _cells[index] == CellState.Empty;

        /// <summary>
        /// Пытается разместить "марку" в клетку по индексу.
        /// </summary>  
        /// <returns>True или False</returns>
        public bool TryPlace(int index, CellState mark)
        {
            if (mark == CellState.Empty)
                return false;

            if (!IsValidIndex(index))
                return false;

            if (!IsEmpty(index))
                return false;

            _cells[index] = mark;
            return true; 
        }

        /// <summary>
        /// Получает победителя.
        /// </summary>  
        /// <returns>Либо X либо O либо null</returns>
        public CellState? GetWinner()
        {
            foreach (var line in _winningLines)
            {
                int index1 = line[0];
                int index2 = line[1];
                int index3 = line[2];

                var firstCellState = _cells[index1];

                if (firstCellState != CellState.Empty)
                {
                    if (firstCellState == _cells[index2] && firstCellState == _cells[index3])
                        return firstCellState; 
                }
            }

            return null;
        }

        /// <summary>
        /// Проверяет переполненно ли поле.
        /// </summary>  
        /// <returns>True или False</returns>
        public bool IsFull()
        {
            foreach (var cell in _cells)
            {
                if (cell == CellState.Empty)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Очищает поле.
        /// </summary>  
        public void Clear()
        { 
            for(int i = 0; i < CellCount; i++)
            { 
               _cells[i] = CellState.Empty; 
            }
        }

        /// <summary>
        /// Откат хода \ замена клетки на Empty.
        /// </summary> 
        public void UndoMove(int index)
        {
            if (!IsValidIndex(index))
                throw new ArgumentOutOfRangeException(nameof(index));

            _cells[index] = CellState.Empty;
        }
    }
}