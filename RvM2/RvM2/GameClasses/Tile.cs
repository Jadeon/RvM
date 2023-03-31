using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.GameClasses
{
    /// <summary>
    /// A tile is the smallest unit on a game board.
    /// </summary>
    public class Tile
    {

        #region Overrides
        public override bool Equals(Object obj)
        {
            var T = obj as Tile;
            return (this.X == T.X && this.Y == T.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "{" + this.X + ", " + this.Y + "}";
        }
        #endregion

        private int _X;
        public int X
        { get { return this._X; } set { this._X = value; } }

        private int _Y;
        public int Y
        { get { return this._Y; } set { this._Y = value; } }

        private int _elevation;
        public int elevation
        {
            get { return this._elevation; }
            set
            {
                if (value >= 0)
                { this._elevation = value; }
                else
                { this._elevation = -1; }
            }
        }

        private bool _occupied;
        public bool occupied
        {
            get { return this._occupied; }
            set { this._occupied = value; }
        }

        public Tile()
        {
            X = 0;
            Y = 0;
            elevation = 0;
            _occupied = false;
        }

        public Tile(Point P, int e)
        {
            X = P.X;
            Y = P.Y;
            this.elevation = e;
        }

        public Tile(string X, string Y) : this()
        {
            int x;
            Int32.TryParse(X, out x);
            int y;
            Int32.TryParse(Y, out y);
            this.X = x;
            this.Y = y;
        }

        public Tile(List<int> p, int e)
        {
            if (p == null || p.Count == 0)
            {
                X = 0;
                Y = 0;
            }
            else
            {
                X = p[0];
                Y = p[1];
            }
            elevation = e;
        }

        public Tile(int X, int Y, int e)
        {
            this.X = X;
            this.Y = Y;
            elevation = e;
        }

        /// <summary>
        /// Given another tile, determines if it is adjacent to this one.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns>True or false</returns>
        public bool adjacent(Tile A)
        {
            return A != this && Math.Abs(A.X - this.X) <= 1 && Math.Abs(A.Y - this.Y) <= 1
                && !(Math.Abs(A.X - this.X) == 1 && Math.Abs(A.Y - this.Y) == 1);
        }

        /// <summary>
        /// Given a direction, gets the position of the tile in that direction.
        /// Directions: 1 is north, 2 is east, 3 is south, 4 is west
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>Next tile</returns>
        public Tile neighbor(int dir)
        {
            int newX = this.X;
            int newY = this.Y;
            switch (dir)
            {
                case 1:
                    newY++;
                    break;
                case 2:
                    newX++;
                    break;
                case 3:
                    newY--;
                    break;
                case 4:
                    newX--;
                    break;
                default:
                    newX = 0;
                    newY = 0;
                    break;
            }
            return new Tile(newX, newY, 0);
        }

        public List<Tile> neighbors(Board board)
        {
            List<Tile> neighbors = new List<Tile>();
            for (int i = 1; i <= 4; i++)
            {                
                    neighbors.Add(this.neighbor(i));
            }
            return neighbors;
        }

        public int distance(Tile T)
        {
            int retVal = (Math.Abs(X - T.X) + Math.Abs(Y - T.Y));
            return retVal;
        }

        public List<int> position()
        {
            List<int> position = new List<int>() { this.X, this.Y };
            return position;
        }

    }
}

