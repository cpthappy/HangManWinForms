using System.Linq;
using System.Text;

namespace Hangman
{
    public class Word
    {
        public string SearchWord { get; private set; }

        public string DisplayValue
        {
            get
            {
                var sb = new StringBuilder(SearchWord.Length);

                for (var i = 0; i < SearchWord.Length; i++)
                {
                    sb.Append(VisibleLetters[i] ? SearchWord[i] : '_');
                    sb.Append(" ");
                }

                return sb.ToString();
            }
        }

        private bool[] VisibleLetters { get; set; }

        public int NumberOfMisses { get; private set; }

        public bool IsComplete
        {
            get
            {
                return VisibleLetters.All<bool>(x => x);
            }
        }

        public Word(string word)
        {
            SearchWord = word;
            VisibleLetters = new bool[word.Length];
            for(var i = 0; i < word.Length; ++i)
            {
                VisibleLetters[i] = false;
            }
            NumberOfMisses = 0;
        }

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

            if(!found)
            {
                NumberOfMisses++;
            }

            return found;
        }
    }
}
