using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******
 * Jacob Lehmer
 * 5/06/15
 * This is the score class which will contain a name and a score,
 * also will use the comparable interface to allow sorting
 * *******/

namespace RossHigleyProject7a
{
    [Serializable]
    class Score : IComparable
    {
        private int score;
        private string name;


        #region Properties for the private data
        //this is the getter for the score
        public int Scoreval { get { return score; } }

        //this is the getter for the name
        public string Name { get { return name; } }

        #endregion


        /*****
         * Jacob Lehmer
         * 5/06/15
         * This is the constructor for the score class
         * ****/
        public Score(int _score, string _name)
        {
            score = _score;
            name = _name;
        }

        /******
         * Jacob Lehmer
         * 5/06/15
         * This is the compare to method to allow the sorting of the list
         * ******/
        public int CompareTo(object _comparingScore)
        {
            if (_comparingScore == null) return 1;
            Score otherScore = _comparingScore as Score;
            return this.Scoreval.CompareTo(otherScore.Scoreval);
        }

        /******
         * Jacob Lehmer
         * 5/06/15
         * This is the to String override
         * *****/
        public override string ToString()
        {
            return name + " With a Score of: " + score.ToString() + "\r\n";
        }


    }
}
