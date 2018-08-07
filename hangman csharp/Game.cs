using System;
using System.Collections.Generic;

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
        /// List with possible guess words
        /// </summary>
        public List<string> PossibleWords { get; private set; }

        /// <summary>
        /// Random generator instance
        /// </summary>
        private readonly Random _Rng;

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
            _Rng = new Random();
            PossibleWords = new List<string> { "Datenschutz", "Energiekrise", "Daktyloskopie", "Gestaltungstechnik" };
            InitNewGame();
        }

        /// <summary>
        /// Start new game
        /// </summary>
        public void InitNewGame()
        {
            SearchWord = new Word(GetRandomWord());
            NumberOfMisses = 0;
        }

        private string GetRandomWord()
        {
            int randomNr = _Rng.Next(PossibleWords.Count);
            return PossibleWords[randomNr];
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
