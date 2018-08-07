using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hangman
{
    public partial class MainWindow : Form
    {
        private Game _Game { get; set; }

        private Button[] _AlphabetButtons { get; set; }

        private List<Label> _Labels  { get; set; }

    public MainWindow()
        {
            _Labels = new List<Label>();
            _Game = new Game();

            InitializeComponent();
        }

        
        bool ignore;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            _AlphabetButtons = this.Controls.OfType<Button>().Except(new Button[] { btnNew }).ToArray();
            Array.ForEach(_AlphabetButtons, b => b.Click += alphabetButton_Click);

            StartNewGame();
        }

        private void alphabetButton_Click(object sender, EventArgs e)
        {
            if (ignore)
                return;
            Button b = (Button)sender;
            b.Enabled = false;

            Array.ForEach(_Labels.ToArray(), lbl => lbl.Text = lbl.Tag.ToString() == b.Text ? b.Text : lbl.Text);
            for (int x = 1; x <= _Labels.Count - 1; x++)
            {
                _Labels[x].Left = _Labels[x - 1].Right;
            }

            if (_Labels[_Labels.Count - 1].Right > this.ClientSize.Width - 14)
            {
                this.SetClientSizeCore(_Labels[_Labels.Count - 1].Right + 14, 381);
            }

            if (!_Labels.Any(lbl => lbl.Text == b.Text))
            {
                _Game.IncreaseStage();
            }
            ignore = _Labels.All(lbl => lbl.Text != " ") || _Game.GameLost;

            this.Invalidate();
        }

        private void MainWindow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (_Game.Stage >= 1)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 85, 190, 210, 190);
            }
            if (_Game.Stage >= 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 148, 190, 148, 50);
            }
            if (_Game.Stage >= 3)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 148, 50, 198, 50);
            }
            if (_Game.Stage >= 4)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 50, 198, 70);
            }
            if (_Game.Stage >= 5)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(188, 70, 20, 20));
            }
            if (_Game.Stage >= 6)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 90, 198, 130);
            }
            if (_Game.Stage >= 7)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 95, 183, 115);
            }
            if (_Game.Stage >= 8)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 95, 213, 115);
            }
            if (_Game.Stage >= 9)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 130, 183, 170);
            }
            if (_Game.Stage >= 10)
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

            ignore = false;

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
