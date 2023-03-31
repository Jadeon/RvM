using RvM2.GameClasses;
using RvM2.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RvM2.LogicClasses
{
    public class LogicHandler
    {
        static Random _r = new Random();
        State gameState = new State();
        int moveCounter = 0;
        public State initializeState(State state)
        {

            Tile t1 = new Tile(getRandomPoint(), 0);
            Tile t2 = new Tile(getRandomPoint(), 0);
            while (t1.Equals(t2))
            {
                t2 = new Tile(getRandomPoint(), 0);
            }
            t1.occupied = t2.occupied = true;
            if (state.board.tiles.Contains(t1))
            {
                state.armies[0].Units[0].Position = t1;
                state.armies[0].Units[0].Alive = "alive";
                state.board.tiles[state.board.tiles.IndexOf(t1)] = t1;
            }
            if (state.board.tiles.Contains(t2))
            {
                state.armies[1].Units[0].Position = t2;
                state.armies[1].Units[0].Alive = "alive";
                state.board.tiles[state.board.tiles.IndexOf(t2)] = t2;
            }
            resetUnitSpeed(state);
            return new State(state.armies, 0, state.board);
        }

        public Point getRandomPoint()
        {
            return new Point(_r.Next(1, 20), _r.Next(1, 20));
        }

        public void gameInteraction(Point mouseClick, State state, RvM_AI parent, MouseEventArgs e)
        {
            Tile t = new Tile(mouseClick, 0);
            gameState = state;
            int i = state.priorityPlayer;

            #region Occupied tile clicked
            if (state.board.tiles[state.board.tiles.IndexOf(t)].occupied)
            {
                try
                {
                    int j = state.armies[i].Units.Select((x, ind) => new { unit = x, index = ind })
                                    .First(x => x.unit.Position.Equals(t)).index;
                    //Check if unit on occupied tile is from the priority players army.
                    Unit selUnit = state.armies[i].Units[j];

                    #region Unit @ tile belongs to priority player
                    if (selUnit.Position.Equals(t))
                    {
                        parent.enemyUnit = false;
                        //Check if this unit is active.
                        if (parent.activeUnit.Equals(selUnit))
                        {
                            if (e.Button == MouseButtons.Right && parent.activeUnit == selUnit)
                            {
                                parent.showConsoleMessage(parent.activeUnit.UnitName + " is now deactivated.");
                                parent.drawUnits();
                                parent.activeUnit = new Unit();
                                parent.activeUnitSet = false;
                                parent.gridInteraction(false);
                                parent.enemyUnit = true;
                                return;
                            }
                            else
                            {
                                parent.showConsoleMessage(parent.activeUnit.UnitName + " is already active.\r\nPlease choose to move, perform an action, pass, or right-click to deactivate.");
                                return;
                            }
                        }
                        else
                        {
                            parent.activeUnit = selUnit;
                            parent.activeUnitSet = true;
                            if (state.armies[i].Units[j].movePts == 0)
                            {
                                parent.move_btn.Enabled = false;
                            }
                            else
                            {
                                parent.move_btn.Enabled = true;
                            }
                            parent.showConsoleMessage(selUnit.UnitName + " is now the active unit.");
                            parent.drawActiveUnits(state.armies[i].Units[j]);
                            parent.gridInteraction(true);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    parent.enemyUnit = true;
                }
                #endregion

                #region Unit @tile is an enemy
                if (parent.enemyUnit)
                {
                    //We have an activeUnit
                    if (parent.activeUnitSet && parent.attack_btn.Enabled)
                    {
                        if (parent.attackClicked)
                        {
                            parent.showConsoleMessage(attackUnit(parent.state,t,parent.activeUnit));
                            if (i == 0)
                            {
                                parent.playerAttacked = true;                                
                            }
                            else
                            {
                                parent.aiAttacked = true;
                            }
                            parent.activeUnit.attackPts -= 1;
                            if (parent.activeUnit.attackPts == 0)
                            {
                                parent.attack_btn.Enabled = false;
                            }
                            parent.drawUnits();
                        }
                    }
                    else
                    {
                        parent.showConsoleMessage("The unit you are selecting does not belong to you, please try your selection again.");
                    }
                }
                #endregion
            }
            #endregion
            //Tile is unoccupied.
            else
            {
                //We have an activeUnit
                if (parent.activeUnitSet && parent.move_btn.Enabled)
                {
                    try
                    {
                        int k = state.armies[i].Units.Select((x, ind) => new { unit = x, index = ind })
                                  .First(x => x.unit.UnitName.Equals(parent.activeUnit.UnitName)).index;
                        if (parent.moveClicked)
                        {
                            if (parent.moveRange.Contains(t) && !t.occupied)
                            {
                                moveCounter = 0;
                                moveUnit(state, t, parent.state.armies[i].Units[k]);
                                parent.drawUnits();
                                parent.state.armies[i].Units[k].movePts -= moveCounter;
                                if (i == 0)
                                {
                                    parent.playerPassed = false;
                                    parent.playerMoved = true;
                                }
                                else
                                {
                                    parent.aiPassed = false;
                                    parent.aiMoved = true;
                                }
                                parent.showConsoleMessage(string.Format("{0} has {1} movement point(s) remaining.", parent.state.armies[i].Units[k].UnitName, parent.state.armies[i].Units[k].movePts));
                                if (parent.state.armies[i].Units[k].movePts == 0)
                                {
                                    parent.move_btn.Enabled = false;
                                }
                            }
                        }
                    }
                    catch { }

                }
                else
                {

                }

            }
        }
        public List<Tile> reconstructAStar(Dictionary<Tile, Tile> came_from, Tile start, Tile end,int reach)
        {
            Tile current = end;
            List<Tile> path = new List<Tile>() { current };
            List<Tile> keyList = came_from.Keys.ToList();
            List<Tile> valList = came_from.Values.ToList();
            if (current.distance(start) > reach)
            {
                path = new List<Tile>();
                path.Add(start);
                return path;
            }
            else
            {
                while (current != start)
                {
                    int index = keyList.IndexOf(current);
                    current = valList[index];
                    path.Add(current);
                }
                path.Reverse();
                return path;
            }
        }

        public List<Tile> reconstructBreadthFirst(Dictionary<Tile, Tile> visited)
        {
            return visited.Keys.Distinct().ToList();
        }

        public void moveUnit(State state, Tile t, Unit activeUnit)
        {
            int i = state.priorityPlayer;
            int j = state.armies[i].Units.Select((x, ind) => new { unit = x, index = ind })
                                         .First(x => x.unit.UnitName.Equals(activeUnit.UnitName)).index;
            if (activeUnit.UnitName == state.armies[i].Units[j].UnitName)
            {
                moveCounter += t.distance(activeUnit.Position);
                state.board.tiles[state.board.tiles.IndexOf(activeUnit.Position)].occupied = false;
                state.armies[i].Units[j].Position = t;
                state.board.tiles[state.board.tiles.IndexOf(t)].occupied = true;
            }
        }

        public string attackUnit(State state, Tile t, Unit activeUnit)
        {
            int h = state.priorityPlayer;
            int i = state.armies[h].Units.Select((x,ind) => new { unit = x, index = ind })
                .First(x => x.unit.UnitName.Equals(activeUnit.UnitName)).index;
            int k;
            int j;
            string message="";
            if (h == 0)
            {
                k = 1;
                j = state.armies[k].Units.Select((x, ind) => new { unit = x, index = ind })
                    .First(x => x.unit.Position.Equals(t)).index;
                
            }else
            {
                k = 0;
                j = state.armies[k].Units.Select((x, ind) => new { unit = x, index = ind })
                    .First(x => x.unit.Position.Equals(t)).index;

            }
            Unit attacker = state.armies[h].Units[i];
            Unit defender = state.armies[k].Units[j];
            Random rnd = new Random();
            int evaRoll = rnd.Next(20);
            bool evaded = evaRoll == 1 ? false : evaRoll == 20 ? true : (defender.Speed + evaRoll > attacker.Accuracy + attacker.Abilities[0].EVMod);
            double mitigation = 0;
            string mitString = "";
            string evaString = "";
            int netHPChange = 0;

            if (evaded)
            {
                message = defender.UnitName + " rolled a " + evaRoll + " and evaded " + attacker.UnitName + "'s " + attacker.Abilities[0].Name + "!";
            }
            else
            {
                if (evaRoll == 1)
                {
                    evaString = defender.UnitName + " rolled a 1! Automatic failure to evade!";
                }
                else
                {
                    evaString = defender.UnitName + " rolled a " + evaRoll + "! It was hit!";
                }
                int mitRoll = rnd.Next(20);
                int mitThreshold = defender.ArmorClass - (attacker.Accuracy + attacker.Abilities[0].MITMod);
                if (mitRoll == 20)
                {
                    mitigation = 1;
                }
                else if (mitRoll == 1)
                {
                    mitigation = 0;
                }
                else
                {
                    mitigation = mitRoll > mitThreshold ? 1 : (mitRoll > Math.Ceiling(mitThreshold / 2.0) ? 0.5 : 0);
                }
                mitigation = 1 - mitigation;
                if (mitigation == 0)
                {
                    mitString = activeUnit.UnitName + " rolled a " + mitRoll + " against a threshold of " + mitThreshold + "! " + attacker.Abilities[0].Name + " hits for full damage!";
                }
                else if (mitigation == 0.5)
                {
                    mitString = activeUnit.UnitName + " rolled a " + mitRoll + " against a threshold of " + mitThreshold + "! " + attacker.Abilities[0].Name + " hits for half damage!";
                }
                else if (mitigation == 1)
                {
                    mitString = activeUnit.UnitName + " rolled a " + mitRoll + " against a threshold of " + mitThreshold + "! " + attacker.Abilities[0].Name + " hits for no damage!";
                }
            }
            foreach (Outcome O in attacker.Abilities[0].Outcomes)
            {
                double prob = rnd.Next(100) * 1.0;
                if (O.OutcomeType == "damage")
                {
                    if (prob >= 100 - O.Prob * 100)
                    {
                        netHPChange -= Convert.ToInt16(O.Mag * 1 - mitigation);
                    }
                }
                else if (O.OutcomeType == "healing")
                {
                    if (prob >= 100 - O.Prob * 100)
                    {
                        netHPChange += Convert.ToInt16(O.Mag);
                    }
                }
                defender.HP += netHPChange;
                if (defender.HP <= 0)
                {
                    defender.HP = 0;
                    defender.Alive = "killed";
                    message = "Evasion: " + evaString + "\r\nMitigation: " + mitString + "\r\nOutcome: " + defender.UnitName + " has been killed!";
                }
                //defender.Effects.AddRange(newEffects);
                message = "Evasion: " + evaString + "\r\nMitigation: " + mitString + "\r\nOutcome: " + defender.UnitName + " now has " + defender.HP + "HP remaining!";
            }
            return message;
        }

        public void passTurn(RvM_AI parent)
        {
            if (parent.state.priorityPlayer == 0)
            {
                parent.state.priorityPlayer = 1;
                parent.aiMoved = false;
                parent.aiAttacked = false;
                if (!parent.playerAttacked && !parent.playerMoved)
                {
                    parent.playerFullPass = true;
                }
            }
            else
            {
                parent.state.priorityPlayer = 0;
                parent.playerMoved = false;
                parent.playerAttacked = false;
                if (!parent.aiAttacked && !parent.aiMoved)
                {
                    parent.aiFullPass = true;
                }
            }
            if (parent.playerFullPass && parent.aiFullPass)
            {
                resetUnitSpeed(parent.state);
                parent.move_btn.Enabled = true;
                parent.attack_btn.Enabled = true;
            }
            if (!parent.activeUnit.UnitName.Equals("NON_UNIT"))
            {
                parent.showConsoleMessage(parent.activeUnit.UnitName + " is now deactivated.");
            }
            parent.activeUnit = new Unit();
            parent.showConsoleMessage(parent.cleanArmyPrint());
        }

        public void resetUnitSpeed(State state)
        {
            for (int i = 0; i < state.armies.Count; i++)
            {
                for (int j = 0; j < state.armies[i].Units.Count; j++)
                {
                    state.armies[i].Units[j].movePts = state.armies[i].Units[j].Speed;
                    state.armies[i].Units[j].attackPts = 1;
                }
            }
        }
    }
}