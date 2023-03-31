using RvM2.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.UtilityClasses
{
    public class AStar
    {
        public Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
        public Dictionary<Tile, int> costSoFar = new Dictionary<Tile, int>();
        #region Constructor

        #endregion
        //Manhattan
        private int Heuristic(Tile start, Tile end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }

        public AStar(State state, Tile start, Tile end, int reach)
        {
            var frontier = new PriorityQueue<Tile>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(end)|| Heuristic(start, current) >= reach)
                {
                    break;
                }
                foreach (var next in current.neighbors(state.board))
                {
                    int newCost = costSoFar[current]
                        + 1;
                    if (validateNext(next, state))
                    {
                        if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                        {
                            costSoFar[next] = newCost;
                            int priority = newCost + Heuristic(next, end);
                            frontier.Enqueue(next, priority);
                            cameFrom[next] = current;
                            
                        }
                    }
                }
            }
        }

        public bool validateNext(Tile next, State state)
        {
            int MaxVal = state.board.tiles.Max(tile => tile.X);
            bool inBounds = (!(next.X == 0 || next.Y == 0) && !(next.X > MaxVal || next.Y > MaxVal));

            if (inBounds)
            {
                bool inBoard = next.Equals(state.board.tiles[state.board.tiles.IndexOf(next)]);
                bool occupied = state.board.tiles[state.board.tiles.IndexOf(next)].occupied;
                return (inBoard && !occupied);
            }
            else
            {
                return inBounds;
            }
        }
    }
}

