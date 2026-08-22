using TicTacToe.Core;

namespace TicTacToeConsoleApp
{
    public class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("Хотите начать новую игру?");
                Console.WriteLine("Нажмите на \"1\" для начала игры.");
                Console.WriteLine("Любая другая клавиша - выход из игры!"); 

                var userInputChar = Console.ReadKey().KeyChar;

                if (userInputChar == '1')
                {
                    Console.Clear();

                    GameSession session = new();
                    RandomAi randomAi = new(CellState.O);

                    Console.WriteLine("Выбирайте пронумерованную, доступную и не занятую клетку для хода.");
                    Console.WriteLine();

                    while (session.Status == GameStatus.InProgress)
                    {
                        ShowBoard();

                        while (true)
                        {
                            Console.Write("Ваш ход: ");

                            var userInput = Console.ReadLine();
                            var success = int.TryParse(userInput, out int userMove);

                            if (!success)
                            {
                                Console.WriteLine("Вы ввели не число!");
                                Console.WriteLine("Выберите не занятую клетку для хода.");
                            }

                            else
                            {
                                var tryUserMove = session.TryMakeMove(userMove - 1);

                                if (!tryUserMove)
                                    Console.WriteLine("Выберите доступную, не занятую клетку!");

                                else
                                {
                                    Console.Clear();

                                    if (session.Status != GameStatus.InProgress)
                                    {
                                        ShowBoard();
                                        ShowGameStatus();
                                        break;
                                    }

                                    else
                                    {
                                        var aiMove = randomAi.ChooseMove(session.Board);

                                        if (!session.TryMakeMove(aiMove))
                                            throw new Exception("ии не смог сделать ход"); 

                                        if (session.Status != GameStatus.InProgress)
                                        {
                                            ShowBoard();
                                            ShowGameStatus();
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    void ShowGameStatus()
                    {
                        switch (session.Status)
                        {
                            case GameStatus.XWin:
                                Console.WriteLine("Победа X!");
                                break;
                            case GameStatus.OWin:
                                Console.WriteLine("Победа O!");
                                break;
                            case GameStatus.Draw:
                                Console.WriteLine("Ничья!");
                                break;
                        }
                        Console.WriteLine();
                    }

                    string FormatCell(int index)
                    {
                        return session.Board[index] switch
                        {
                            CellState.X => "X",
                            CellState.O => "O",
                            _ => (index + 1).ToString()
                        };
                    }

                    void ShowBoard()
                    {
                        Console.WriteLine($" {FormatCell(0)} | {FormatCell(1)} | {FormatCell(2)} ");
                        Console.WriteLine(" ---------- ");
                        Console.WriteLine($" {FormatCell(3)} | {FormatCell(4)} | {FormatCell(5)} ");
                        Console.WriteLine(" ---------- ");
                        Console.WriteLine($" {FormatCell(6)} | {FormatCell(7)} | {FormatCell(8)} ");
                        Console.WriteLine();
                    }
                }

                else break;
            } 
        }
    }
}