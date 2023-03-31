using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.GameClasses
{
    /// <summary>
    /// A State is:
    ///     A list of Armies armies.
    ///     A priority playerID
    ///     A Board of tiles.
    /// </summary>
    public class State
    {
        private int deployedUnits = 0;

        #region Overrides
        public override string ToString()
        {
            string retVal = "The state is: \r\n";
            foreach (Army a in armies)
            {
                retVal += "Army  - " + a.ToString() + " \r\n";
            }
            retVal += "Priority - " + this._priorityPlayer + " \r\n";
            retVal += "Board - " + this._board.ToString() + " \r\n";
            return retVal;
        }
        #endregion

        #region Fields_&_Properties
        private List<Army> _armies;

        public List<Army> armies
        {
            get { return this._armies; }
            set { this._armies = value; }
        }

        private int _priorityPlayer;

        public int priorityPlayer
        {
            get { return _priorityPlayer; }
            set { this._priorityPlayer = value; }
        }

        private Board _board;
        public Board board
        {
            get { return this._board; }
            set { this._board = value; }
        }

        private Random _rnd = new Random(DateTime.Now.Millisecond);

        #endregion

        #region Constructors
        public State(List<Army> armies, int priorityPlayer, Board board)
        {
            this.armies = armies;
            this._priorityPlayer = priorityPlayer;
            this.board = board;
        }

        public State() : this(new List<Army>(), new int(), new Board())
        { }
        #endregion

        #region Methods
        #endregion
    }
}