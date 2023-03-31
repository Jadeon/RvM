using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.GameClasses
{
    /// <summary>
    /// An Army is:
    ///     A List of Units.
    /// 
    /// This class is the object to reify the concept of an army, as defined above, for use within this game. 
    /// </summary>

    [Serializable]
    public class Army
    {
        #region Overrides
        public override string ToString()
        {
            string retVal = "";
            foreach (Unit u in Units)
            {
                //retval += u.ToString()+ "\r\n";
                retVal += u.ToString();
            }
            return retVal;
        }

        #endregion

        #region Fields_&_Properties
        private List<Unit> _Units;
        public List<Unit> Units
        {
            get
            {
                return this._Units;
            }
            set
            {
                this._Units = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor for an Army with no(Read: empty list of) units.
        /// </summary>
        public Army()
        {
            this.Units = new List<Unit>();
        }

        /// <summary>
        /// Constructor for creating an Army from a list of units.
        /// </summary>
        /// <param name="units">List of units in the army</param>
        public Army(List<Unit> units)
        {
            this.Units = units;
        }
        #endregion
    }
}
