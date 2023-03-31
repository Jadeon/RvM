using RvM2.GameClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.UtilityClasses
{
    public class BreadthFirst
    {
        private int Heuristic(Tile start, Tile end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }

        public Dictionary<Tile, Tile> visited = new Dictionary<Tile, Tile>();
        Queue<Tile> frontier = new Queue<Tile>();
        Tile current = new Tile();

        public BreadthFirst(State state, Tile start, int reach)
        {
            frontier.Enqueue(start);

            visited[start] = start;

            while (frontier.Count > 0) 
            {
                current = frontier.Dequeue();
                if (Heuristic(start, current) < reach)
                {
                    foreach (Tile next in current.neighbors(state.board))
                    {
                        if (validateNext(next, state))
                        {
                            List<Tile> validator = visited.Keys.ToList();
                            if (!validator.Contains(next))
                            {
                                frontier.Enqueue(next);
                                visited[next] = current;
                            }                            
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
