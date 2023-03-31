using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.GameClasses
{
    /// <summary>
    /// An Ability is:
    ///     A string name,
    ///     A string tooltip,
    ///     An integer range,
    ///     An integer evasion_modifier,
    ///     An integer mitigation_modifier,
    ///     An integer cooldown,
    ///     A list of integers costs,
    ///     A list of Units targets,
    ///     And a list of outcomes.
    ///     
    /// 
    /// This class is the object to reify the concept of an ability, as defined above, for use within this game. 
    /// </summary>
    [Serializable]
    public class Ability
    {
        private static readonly Unit nonunit = new Unit();

        #region Overrides
        public override string ToString()
        {
            return Name + ": " + Tooltip;
        }
        #endregion        

        #region Fields_&_Properties
        private string _Name;
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        private string _Tooltip;
        public string Tooltip
        {
            get
            {
                return this._Tooltip;
            }
            set
            {
                this._Tooltip = value;
            }
        }

        private int _Range;
        public int Range
        {
            get
            {
                return this._Range;
            }
            set
            {
                if (value >= 0)
                {
                    this._Range = value;
                }
                else
                { this._Range = 0; }
            }
        }

        private int _EVMod;
        public int EVMod
        {
            get
            {
                return this._EVMod;
            }
            set
            {
                if (value >= -20 && value <= 20)
                {
                    this._EVMod = value;
                }
                else
                { this._EVMod = 0; }
            }
        }

        private int _MITMod;
        public int MITMod
        {
            get
            {
                return this._MITMod;
            }
            set
            {
                if (value >= -20 && value <= 20)
                {
                    this._MITMod = value;
                }
                else
                { this._MITMod = 0; }
            }
        }

        private int _Cooldown;
        public int Cooldown
        {
            get
            {
                return this._Cooldown;
            }
            set
            {
                if (value >= 0)
                {
                    this._Cooldown = value;
                }
                else
                { this._Cooldown = 0; }
            }
        }

        private List<int> _Costs;
        public List<int> Costs
        {
            get { return this._Costs; }
            set { this._Costs = value; }
        }

        private List<Unit> _Targets;
        public List<Unit> Targets
        {
            get { return this._Targets; }
            set { this._Targets = value; }
        }

        private List<Outcome> _Outcomes;
        public List<Outcome> Outcomes
        {
            get { return this._Outcomes; }
            set { this._Outcomes = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor to construct the default GENERIC UNINITIALIZED ABILITY.
        /// </summary>
        public Ability() : this("NON_ABILITY", "This is a non-initialized ability", 0, 0, 0, 0, new List<int>(), new List<Unit>(), new List<Outcome>())
        {

        }

        /// <summary>
        /// Ability constructor which uses default costs and targets.
        /// </summary>
        /// <param name="name">Ability Name</param>
        /// <param name="tooltip">Ability Tooltip</param>
        /// <param name="range">Range of the Ability</param>
        /// <param name="eva">Evasion Modifier</param>
        /// <param name="mit">Mitigation Modifier</param>
        /// <param name="cd">Cooldown</param>
        /// <param name="outcomes">List of Outcomes of Ability Use</param>        
        public Ability(string name, string tooltip, int range, int eva, int mit, int cd, List<Outcome> outcomes) : this()
        {

        }

        /// <summary>
        /// Ability constructor which uses default costs and targets and only allows a single outcome.
        /// </summary>
        /// <param name="name">Ability Name</param>
        /// <param name="tooltip">Ability Tooltip</param>
        /// <param name="range">Range of the Ability</param>
        /// <param name="eva">Evasion Modifier</param>
        /// <param name="mit">Mitigation Modifier</param>
        /// <param name="cd">Cooldown</param>
        /// <param name="outcome">Unique Outcome of the ability</param>    
        public Ability(string name, string tooltip, int range, int eva, int mit, int cd, Outcome outcome) : this()
        {
            this.Name = name;
            this.Tooltip = tooltip;
            this.Range = range;
            this.EVMod = eva;
            this.MITMod = mit;
            this.Cooldown = cd;

            List<Outcome> outList = new List<Outcome>() { outcome };
            this.Outcomes = outList;
        }

        /// <summary>
        /// Ability constructor which allows explicit definition for all parameters.
        /// </summary>
        /// <param name="name">Ability Name</param>
        /// <param name="tooltip">Ability Tooltip</param>
        /// <param name="range">Range of the Ability</param>
        /// <param name="eva">Evasion Modifier</param>
        /// <param name="mit">Mitigation Modifier</param>
        /// <param name="cd">Cooldown</param>
        /// <param name="costs">Ability Cost</param>
        /// <param name="targets">List of Targets the Ability will affect</param>
        /// <param name="outcomes">List of Outcomes of Ability Use</param> 
        public Ability(string name, string tooltip, int range, int eva, int mit, int cd, List<int> costs, List<Unit> targets, List<Outcome> outcomes)
        {
            this.Name = name;
            this.Tooltip = tooltip;
            this.Range = range;
            this.EVMod = eva;
            this.MITMod = mit;
            this.Cooldown = cd;
            this.Costs = costs;
            this.Targets = targets;
            this.Outcomes = outcomes;
        }

        #endregion
    }
}
