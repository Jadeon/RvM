using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.GameClasses
{
    /// <summary>
    /// A Board is a collection of Tiles which represents the game board.
    /// </summary>
    public class Board
    {
        public override string ToString()
        {
            return name;
        }

        private string _name;
        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private List<Tile> _tiles;
        public List<Tile> tiles
        {
            get
            {
                return this._tiles;
            }
            set
            {
                List<Tile> tempList = new List<Tile>();
                foreach (object o in value)
                {
                    var temp = o as Tile;
                    if (!tempList.Contains(temp))
                    {
                        tempList.Add(temp);
                    }
                }
                _tiles = tempList;
            }
        }

        public Board()
        {
            _name = "empty board";
            _tiles = new List<Tile>();
        }

        public Board(string name, List<Tile> tiles)
        {
            this.name = name;
            this.tiles = tiles;
        }

    }
}
