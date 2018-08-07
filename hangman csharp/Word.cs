using System.Linq;
using System.Text;

namespace Hangman
{
    /// <summary>
    /// Class for managing state of a word in hang man game
    /// </summary>
    public class Word
    {
        /// <summary>
        /// String containing the word
        /// </summary>
        public string SearchWord { get; private set; }

        /// <summary>
        /// Current value for displaying the current state of guessed word
        /// contains an underscore for missing letters
        /// </summary>
        public string DisplayValue
        {
            get
            {
                var sb = new StringBuilder(SearchWord.Length);

                for (var i = 0; i < SearchWord.Length; i++)
                {
                    sb.Append(VisibleLetters[i] ? SearchWord[i] : ' ');
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Array with state of letters
        /// True = Letter correctly guessed and visible
        /// False = Letter not visible
        /// </summary>
        private bool[] VisibleLetters { get; set; }

        /// <summary>
        /// True, if all letters where correctly guessed, false otherwise
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return VisibleLetters.All<bool>(x => x);
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="word">String containing word to be guessed</param>
        public Word(string word)
        {
            SearchWord = word.ToUpper() ;
            VisibleLetters = new bool[word.Length];
            for(var i = 0; i < word.Length; ++i)
            {
                VisibleLetters[i] = false;
            }
        }

        /// <summary>
        /// Make a guess
        /// </summary>
        /// <param name="letter">letter from current guess</param>
        /// <returns>True, if letter is contained in word, false otherwise</returns>
        public bool GuessLetter(char letter)
        {
            bool found = false;

            for(var i = 0; i < SearchWord.Length; ++i)
            {
                if(SearchWord[i]==letter)
                {
                    VisibleLetters[i] = true;
                    found = true;
                }
            }

            return found;
        }
    }
}
