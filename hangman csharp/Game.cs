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

        public bool IsFinished
        {
            get
            {
                return GameWon || GameLost;
            }
        }

        public Game()
        {
            InitNewGame();
        }

        public void InitNewGame()
        {
            SearchWord = new Word("FOOBAR");
            Stage = 0;
        }

        private void IncreaseStage()
        {
            Stage += 1;
            Stage = Math.Min(Stage, 10);
        }

        public bool GuessLetter(char c)
        {
            var result = this.SearchWord.GuessLetter(c);

            if (!result)
            {
                IncreaseStage();
            }

            return result;
        }




    }
}
