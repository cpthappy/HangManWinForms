using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Hangman
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Manages game state
        /// </summary>
        private Game _Game { get; set; }


        /// <summary>
        /// Array with all character buttons
        /// </summary>
        private Button[] _AlphabetButtons { get; set; }


        /// <summary>
        /// List with all labels for displaying the current word
        /// </summary>
        private List<Label> _Labels { get; set; }

        public MainWindow()
        {
            _Labels = new List<Label>();
            _Game = new Game();

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            // Get all buttons except new button:
            _AlphabetButtons = this.Controls.OfType<Button>().Except(new Button[] { btnNew }).ToArray();

            // Register click handler for each character button
            Array.ForEach(_AlphabetButtons, b => b.Click += alphabetButton_Click);

            StartNewGame();
        }

        private void alphabetButton_Click(object sender, EventArgs e)
        {
            if (_Game.IsFinished)
            {
                // Ignore input for finished game
                return;
            }

            Button b = (Button)sender;
            b.Enabled = false;

            // Take button text as guessed letter
            if (this._Game.GuessLetter(b.Text[0]))
            {
                // in case of successfull guess update display
                for (var i = 0; i < this._Game.SearchWord.DisplayValue.Length; i++)
                {
                    _Labels[i].Text = this._Game.SearchWord.DisplayValue[i].ToString();
                }
            }

            // Adjust position and size of labels
            for (int x = 1; x <= _Labels.Count - 1; x++)
            {
                _Labels[x].Left = _Labels[x - 1].Right;
            }

            if (_Labels[_Labels.Count - 1].Right > this.ClientSize.Width - 14)
            {
                this.SetClientSizeCore(_Labels[_Labels.Count - 1].Right + 14, 381);
            }

            this.Invalidate();
        }

        private void MainWindow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
           
            if (_Game.NumberOfMisses >= 1)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 85, 190, 210, 190);
            }
            if (_Game.NumberOfMisses >= 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 148, 190, 148, 50);
            }
            if (_Game.NumberOfMisses >= 3)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 148, 50, 198, 50);
            }
            if (_Game.NumberOfMisses >= 4)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 50, 198, 70);
            }
            if (_Game.NumberOfMisses >= 5)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(188, 70, 20, 20));
            }
            if (_Game.NumberOfMisses >= 6)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 90, 198, 130);
            }
            if (_Game.NumberOfMisses >= 7)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 95, 183, 115);
            }
            if (_Game.NumberOfMisses >= 8)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 95, 213, 115);
            }
            if (_Game.NumberOfMisses >= 9)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 130, 183, 170);
            }
            if (_Game.NumberOfMisses >= 10)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 130, 213, 170);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            ResetUI();
            _Game.InitNewGame();
            CreateNewLetterLabels();
            this.Invalidate();
        }

        private void CreateNewLetterLabels()
        {
            int startX = 14;
            foreach (char c in _Game.SearchWord.DisplayValue)
            {
                Label lbl = new Label();
                lbl.Text = " ";
                lbl.Font = new Font(this.Font.Name, 35, FontStyle.Underline);
                lbl.Location = new Point(startX, 250);
                lbl.Tag = c.ToString();
                lbl.AutoSize = true;
                this.Controls.Add(lbl);
                _Labels.Add(lbl);
                startX = lbl.Right;
            }
        }

        private void ResetUI()
        {
            this.SetClientSizeCore(546, 381);
            Array.ForEach(this.Controls.OfType<Label>().ToArray(), lbl => lbl.Dispose());
            Array.ForEach(_AlphabetButtons, b => b.Enabled = true);
            _Labels = new List<Label>();
        }
    }
}
