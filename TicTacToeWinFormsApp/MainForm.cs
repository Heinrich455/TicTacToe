using TicTacToe.Core;

namespace TicTacToeWinFormsApp
{
    public partial class MainForm : Form
    {
        private const int startPointX = 30;
        private const int startPointY = 30;
        private const int sizeCell = 100;
        private const int cellCount = 9;
        private readonly Button[] cellsButtons = new Button[cellCount];
        private Label? _difficultyLabel;
        private Button? _easyDifficultyButton;
        private Button? _mediumDifficultyButton;
        private Button? _hardDifficultyButton;
        private GameSession? _gameSession;
        private IAi? _ai;

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _difficultyLabel = new()
            {
                Location = new Point(20, 50),
                Size = new Size(sizeCell, sizeCell),
                Text = "Выберите уровень сложности:",
                AutoSize = true,
                Visible = false
            };

            _easyDifficultyButton = new()
            {
                Location = new Point(20, 120),
                Size = new Size(sizeCell, sizeCell),
                Text = "Легкий",
                AutoSize = true,
                Visible = false
            }; 

            _mediumDifficultyButton = new()
            {
                Location = new Point(120, 120),
                Size = new Size(sizeCell, sizeCell),
                Text = "Средний",
                AutoSize = true,
                Visible = false
            };

            _hardDifficultyButton = new()
            {
                Location = new Point(230, 120),
                Size = new Size(sizeCell, sizeCell),
                Text = "Сложный",
                AutoSize = true,
                Visible = false
            };

            _easyDifficultyButton.Click += EasyDifficultyButton_Click;
            _mediumDifficultyButton.Click += MediumDifficultyButton_Click;
            _hardDifficultyButton.Click += HardDifficultyButton_Click;

            Controls.Add(_difficultyLabel);
            Controls.Add(_easyDifficultyButton);
            Controls.Add(_mediumDifficultyButton);
            Controls.Add(_hardDifficultyButton);
        }
         
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            greetingLabel.Visible = false;
            yesButton.Visible = false;
             
            TurnOnSettings();
        }

        private void EasyDifficultyButton_Click(object? sender, EventArgs e)
        {
            TurnOffSettings();

            _gameSession = new GameSession();
            
            ShowBoard();

            if (startSecondCheckBox.Checked)
            {
                _ai = new RandomAi(CellState.X);

                int aiMove = _ai.ChooseMove(_gameSession.Board);

                if (!_gameSession.TryMakeMove(aiMove))
                    throw new Exception("ai failed to make a move");

                cellsButtons[aiMove].Text = "X";
                cellsButtons[aiMove].Enabled = false;
            }

            else
                _ai = new RandomAi(CellState.O);
        } 

        private void MediumDifficultyButton_Click(object? sender, EventArgs e)
        {
            TurnOffSettings();

            _gameSession = new GameSession();

            ShowBoard();

            if (startSecondCheckBox.Checked)
            {
                _ai = new MixedAi(CellState.X);
                 
                _gameSession.TryMakeMove(4); // Чисто моя прихоть, чтобы в этом случае игра всегда начиналась так

                cellsButtons[4].Text = "X";
                cellsButtons[4].Enabled = false;
            }

            else
                _ai = new MixedAi(CellState.O);
        }

        private void HardDifficultyButton_Click(object? sender, EventArgs e)
        {
            TurnOffSettings();

            _gameSession = new GameSession();
             
            ShowBoard();

            if (startSecondCheckBox.Checked)
            {
                _ai = new MinimaxAi(CellState.X);
                  
                int aiMove = _ai.ChooseMove(_gameSession.Board);

                if (!_gameSession.TryMakeMove(aiMove))
                    throw new Exception("ai failed to make a move");

                cellsButtons[aiMove].Text = "X";
                cellsButtons[aiMove].Enabled = false;
            }

            else
                _ai = new MinimaxAi(CellState.O); 
        }

        private void ShowBoard()
        {
            for (int i = 0; i < cellCount; i++)
            {
                int row = i / 3;
                int col = i % 3;

                int x = startPointX + col * sizeCell;
                int y = startPointY + row * sizeCell;

                Button button = new()
                {
                    Location = new Point(x, y),
                    Size = new Size(sizeCell, sizeCell), 
                    Tag = i
                };

                button.Click += CellButton_Click;

                cellsButtons[i] = button;
                Controls.Add(button);
            }
        }

        private void CellButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button button)
                return;

            if (_gameSession == null || _ai == null)
                return;

            if (_gameSession.Status != GameStatus.InProgress)
                return;

            int index = (int)button.Tag!;
            var currentPlayer = _gameSession.CurrentPlayer;

            if (!_gameSession.TryMakeMove(index))
                return;

            button.Text = $"{currentPlayer}";
            button.Enabled = false;

            if (_gameSession.Status != GameStatus.InProgress)
            {
                ShowResult();
                newGameButton.Visible = true;
                return;
            }

            currentPlayer = _gameSession.CurrentPlayer;
            int aiMove = _ai.ChooseMove(_gameSession.Board);

            if (!_gameSession.TryMakeMove(aiMove))
                throw new Exception("ai failed to make a move");

            cellsButtons[aiMove].Text = $"{currentPlayer}"; 
            cellsButtons[aiMove].Enabled = false;

            if (_gameSession.Status != GameStatus.InProgress)
            {
                ShowResult();
                newGameButton.Visible = true;
                return;
            }
        }
        private void ShowResult()
        {
            switch (_gameSession!.Status)
            {
                case GameStatus.XWin:
                    MessageBox.Show("Победа X!", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case GameStatus.OWin:
                    MessageBox.Show("Победа O!", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case GameStatus.Draw:
                    MessageBox.Show("Ничья!", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _gameSession!.Restart();

            foreach (var cell in cellsButtons)
            {
                cell.Visible = false;
            }

            TurnOnSettings();
        }

        private void StartSecondCheckBox_CheckedChanged(object sender, EventArgs e) { }

        private void TurnOffSettings()
        {
            _difficultyLabel!.Visible = false;

            _easyDifficultyButton!.Visible = false;
            _mediumDifficultyButton!.Visible = false;
            _hardDifficultyButton?.Visible = false;

            startSecondCheckBox.Visible = false;
        }

        private void TurnOnSettings()
        {
            _difficultyLabel!.Visible = true;
            _easyDifficultyButton!.Visible = true;
            _mediumDifficultyButton!.Visible = true;
            _hardDifficultyButton!.Visible = true;

            startSecondCheckBox.Visible = true;
            startSecondCheckBox.Checked = false;
        }
    }
}