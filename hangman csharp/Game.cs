using System;

namespace Hangman
{
    /// <summary>
    /// Class for managing game state
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Current search word
        /// </summary>
        public Word SearchWord { get; private set; }

        /// <summary>
        /// Total number of wrong guesses
        /// </summary>
        public int NumberOfMisses { get; private set; }

        /// <summary>
        /// True, if game is lost, false otherwise
        /// </summary>
        public bool GameLost
        {
            get
            {
                return NumberOfMisses >= 10;
            }
        }

        /// <summary>
        /// True, if game is won, false otherwise
        /// </summary>
        public bool GameWon
        {
            get
            {
                return SearchWord.IsComplete;
            }
        }

        /// <summary>
        /// True if game is finished (i.e. lost or won)
        /// </summary>
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

        /// <summary>
        /// Start new game
        /// </summary>
        public void InitNewGame()
        {
            SearchWord = new Word("DATENSCHUTZ");
            NumberOfMisses = 0;
        }

        public bool GuessLetter(char c)
        {
            var result = this.SearchWord.GuessLetter(c) && !GameLost;

            if (!result)
            {
                NumberOfMisses += 1;
            }

            return result;
        }
    }
}
