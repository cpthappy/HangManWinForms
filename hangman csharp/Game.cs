using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Game
    {
        public Word SearchWord { get; private set; }

        public int Stage { get; private set; }

        public bool GameLost
        {
            get
            {
                return Stage == 10;
            }
        }

        public bool GameWon
        {
            get
            {
                return SearchWord.IsComplete;
            }
        }

        public bool IgnoreInput { get; private set; }

        public Game()
        {
            InitNewGame();
        }

        public void InitNewGame()
        {
            SearchWord = new Word("Datenschutzgrundverordnung");
            Stage = 0;
            IgnoreInput = false;
        }

        public void IncreaseStage()
        {
            Stage += 1;
            Stage = Math.Min(Stage, 10);
        }




    }
}
