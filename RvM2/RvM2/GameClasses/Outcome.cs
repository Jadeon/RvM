using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.GameClasses
{
    [Serializable]
    public class Outcome
    {
        private static readonly string[] defaultEffects = { "stun", "slow", "silence", "cripple", "haste" };

        private string _OutcomeType = "undefined";
        public string OutcomeType
        {
            get
            {
                return this._OutcomeType;
            }
            set
            {
                if (value == "damage" || value == "healing" || defaultEffects.Any(s => s.Equals(value)))
                {
                    this._OutcomeType = value;
                }
                else
                {
                    this._OutcomeType = "undefined";
                }
            }
        }

        private double _Prob = 1.0;
        public double Prob
        {
            get
            {
                return this._Prob;
            }
            set
            {
                if (value < 0)
                {
                    this._Prob = 1.0;
                }
                else
                {
                    this._Prob = value;
                }
            }
        }

        private int _Mag = 0;
        public int Mag
        {
            get
            {
                return this._Mag;
            }
            set
            {
                this._Mag = value;
            }
        }


        #region constructors
        /// <summary>
        /// Outcome that has no effect, should never be used.
        /// </summary>
        public Outcome() : this(1.0, 0, "default")
        {

        }

        /// <summary>
        /// Outcome with default probability of 1
        /// </summary>
        /// <param name="type">the amount of damage or healing done by the outcome</param>
        /// <param name="mag">type of the outcome must be "damage", "healing", or one of the default effects</param>
        public Outcome(string type, int mag) : this(1, mag, type)
        {

        }

        /// <summary>
        /// Outcome constructor which takes all three arguments.
        /// </summary>
        /// <param name="p">probability of the outcome (0-1]</param>
        /// <param name="mag">the amount of damage or healing done by the outcome</param>
        /// <param name="otype">type of the outcome must be "damage", "healing", or one of the default effects</param>
        public Outcome(double p, int mag, string otype)
        {
            Prob = p;
            Mag = mag;
            this.OutcomeType = otype;
        }
        #endregion        

        public override string ToString()
        {
            return "Type: " + OutcomeType + "\nAmount: " + Mag.ToString() + "\nProbability: " + Prob.ToString();
        }
    }
}
