namespace TicTacToeWinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            exitButton = new Button();
            yesButton = new Button();
            greetingLabel = new Label();
            newGameButton = new Button();
            startSecondCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // exitButton
            // 
            exitButton.AutoSize = true;
            exitButton.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            exitButton.Location = new Point(81, 377);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(200, 51);
            exitButton.TabIndex = 0;
            exitButton.Text = "ВЫХОД";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += ExitButton_Click;
            // 
            // yesButton
            // 
            yesButton.Location = new Point(148, 91);
            yesButton.Name = "yesButton";
            yesButton.Size = new Size(112, 34);
            yesButton.TabIndex = 1;
            yesButton.Text = "Да";
            yesButton.UseVisualStyleBackColor = true;
            yesButton.Click += YesButton_Click;
            // 
            // greetingLabel
            // 
            greetingLabel.AutoSize = true;
            greetingLabel.Font = new Font("Times New Roman", 16F, FontStyle.Bold, GraphicsUnit.Point, 204);
            greetingLabel.Location = new Point(39, 28);
            greetingLabel.Name = "greetingLabel";
            greetingLabel.Size = new Size(311, 36);
            greetingLabel.TabIndex = 2;
            greetingLabel.Text = "Хотите начать игру?";
            // 
            // newGameButton
            // 
            newGameButton.AutoSize = true;
            newGameButton.Location = new Point(114, 335);
            newGameButton.Name = "newGameButton";
            newGameButton.Size = new Size(146, 36);
            newGameButton.TabIndex = 3;
            newGameButton.Text = "Новая игра";
            newGameButton.UseVisualStyleBackColor = true;
            newGameButton.Visible = false;
            newGameButton.Click += NewGameButton_Click;
            // 
            // stastSecondCheckBox
            // 
            startSecondCheckBox.AutoSize = true;
            startSecondCheckBox.Location = new Point(70, 299);
            startSecondCheckBox.Name = "stastSecondCheckBox";
            startSecondCheckBox.Size = new Size(233, 30);
            startSecondCheckBox.TabIndex = 4;
            startSecondCheckBox.Text = "Начинать вторым";
            startSecondCheckBox.UseVisualStyleBackColor = true;
            startSecondCheckBox.Visible = false;
            startSecondCheckBox.CheckedChanged += StartSecondCheckBox_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(14F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 440);
            Controls.Add(startSecondCheckBox);
            Controls.Add(newGameButton);
            Controls.Add(greetingLabel);
            Controls.Add(yesButton);
            Controls.Add(exitButton);
            Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Крестики-Нолики";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exitButton;
        private Button yesButton;
        private Label greetingLabel;
        private Button newGameButton;
        private CheckBox startSecondCheckBox;
    }
}
