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

        public Game()
        {
            InitNewGame();
        }

        public void InitNewGame()
        {
            SearchWord = new Word("Datenschutzgrundverordnung");
            Stage = 0;
        }

        public void IncreaseStage()
        {
            Stage += 1;
        }

       
    }
}
