using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RvM2.GameClasses
{
    /// <summary>
    /// A Unit is:
    ///     A string UnitName,
    ///     A string FlavorText,
    ///     A string ImageFile,
    ///     A string Type,
    ///     An integer for each:
    ///         Range
    ///         HP
    ///         Accuracy
    ///         Armor Class
    ///         Speed
    ///         Command
    ///     A list of integers Position.
    ///     A list of Abilities Abilities.
    ///     A list of Effects.
    ///     A string Alive. This will only ever be “alive”, “killed”, or “banished”.
    ///     
    /// This class is the object to reify the concept of a Unit, as defined above, for use within this game.     
    /// </summary>
    [Serializable]
    public class Unit
    {

        private static readonly string[] status = { "uninitialized", "alive", "killed", "banished" };

        #region Overrides
        public override string ToString()
        {
            string brk = "\r\n";
            string retVal = "Name: " + UnitName.ToString() + brk;
            retVal += " Type: " + Type + brk;
            retVal += " HP: " + HP.ToString() + brk;
            retVal += " Accuracy: " + Accuracy.ToString() + brk;
            retVal +=  "ArmorClass: " + ArmorClass.ToString() + brk;
            retVal += " Speed: " + Speed.ToString() + brk;
            retVal += " Command: " + Command.ToString() + brk;
            retVal += " Position: (" + Position.ToString()+ ")" + brk;
            retVal +=  "Abilities: " + brk;
            foreach (Ability a in Abilities)
            {
                retVal += "    " + a.ToString() + brk;
            }
            retVal += "Effects: " + brk;
            foreach (Effect e in Effects)
            {
                retVal += "    " + e.ToString() + brk;
            }
            retVal += "Alive: " + Alive + brk;

            return retVal;
        }
        #endregion

        #region Fields_&_Properties
        private string _UnitName;
        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }

        private int _UnitID;
        public int UnitID
        {
            get
            {
                return this._UnitID;
            }
            set
            {
                this._UnitID = value;
            }
        }

        private string _FlavorText;
        public string FlavorText
        {
            get
            {
                return this._FlavorText;
            }
            set
            {
                this._FlavorText = value;
            }
        }

        private string _ImageFile;
        public string ImageFile
        {
            get { return this._ImageFile; }
            set { this._ImageFile = value; }
        }

        private string _ActiveImageFile;
        public string ActiveImageFile
        {
            get { return this._ActiveImageFile; }
            set { this._ActiveImageFile = value; }
        }

        private string _Type;
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        #region stats

        private int _HP;
        public int HP
        {
            get
            {
                return this._HP;
            }
            set
            {
                if (value >= 1)
                {
                    this._HP = value;
                }
                else
                { this._HP = 0; }
            }
        }

        private int _Accuracy;
        public int Accuracy
        {
            get
            {
                return this._Accuracy;
            }
            set
            {
                if (value >= 0)
                {
                    this._Accuracy = value;
                }
                else
                { this._Accuracy = 0; }
            }
        }

        private int _ArmorClass;
        public int ArmorClass
        {
            get
            {
                return this._ArmorClass;
            }
            set
            {
                if (value >= 0)
                {
                    this._ArmorClass = value;
                }
                else
                { this._ArmorClass = 0; }
            }
        }

        private int _Speed;
        public int Speed
        {
            get
            {
                return this._Speed;
            }
            set
            {
                if (value >= 0)
                {
                    this._Speed = value;
                }
                else
                { this._Speed = 0; }
            }
        }

        [XmlIgnoreAttribute]
        public int movePts { get; set; }

        [XmlIgnoreAttribute]
        public int attackPts { get; set; }

        private int _Command;
        public int Command
        {
            get
            {
                return this._Command;
            }
            set
            {
                if (value >= 0)
                {
                    this._Command = value;
                }
                else
                { this._Command = 0; }
            }
        }
        #endregion

        private Tile _Position;
        public Tile Position
        {
            get { return this._Position; }
            set { this._Position = value; }
        }

        private List<Ability> _Abilities;
        public List<Ability> Abilities
        {
            get { return this._Abilities; }
            set { this._Abilities = value; }
        }

        private List<Effect> _Effects;
        public List<Effect> Effects
        {
            get { return this._Effects; }
            set { this._Effects = value; }
        }

        private string _Alive;
        public string Alive
        {
            get
            {
                return this._Alive;
            }
            set
            {
                if (status.Any(s => s.Equals(value)))
                {
                    this._Alive = value;
                }
                else
                {
                    this._Alive = "uninitialized";
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor to construct the default GENERIC UNINITIALIZED UNIT.
        /// </summary>
        public Unit() : this("NON_UNIT", -1, "this unit is not initialized", "nonunit", -1, 0, 0, 0, 0, new List<Ability>(), new List<int>(), new List<Effect>(), "uninitialized", @"..\..\..\images\swordsman.png", @"..\..\..\images\swordsman_Active.png")
        {
        }

        /// <summary>
        /// Used for defining a unit with 1 ability, no real position, and no effects on it.
        /// </summary>
        /// <param name="name">Unit Name</param>
        /// <param name="id">Unit IDno</param>
        /// <param name="flav">Flavor Text</param>
        /// <param name="type">Unit Type</param>
        /// <param name="hp">HP</param>
        /// <param name="acc">Accuracy</param>
        /// <param name="ac">Armor Class</param>
        /// <param name="spd">Speed</param>
        /// <param name="cmd">Command</param>
        /// <param name="ability">Single Ability</param>
        public Unit(string name, int id, string flav, string type, int hp, int acc, int ac, int spd, int cmd, Ability ability) : this()
        {
            this.UnitID = id;
            this.UnitName = name;
            this.FlavorText = flav;
            this.Type = type;
            this.HP = hp;
            this.Accuracy = acc;
            this.ArmorClass = ac;
            this.Speed = spd;
            this.Command = cmd;


            List<Ability> aList = new List<Ability>() { ability };
            this.Abilities = aList;
        }

        /// <summary>
        /// Used for defining a unit with 1 ability, a position, and no effects on it.
        /// </summary>
        /// <param name="name">Unit Name</param>
        /// <param name="id">Unit IDno</param>
        /// <param name="flav">Flavor Text</param>
        /// <param name="type">Unit Type</param>
        /// <param name="hp">HP</param>
        /// <param name="acc">Accuracy</param>
        /// <param name="ac">Armor Class</param>
        /// <param name="spd">Speed</param>
        /// <param name="cmd">Command</param>
        /// <param name="ability">Single Ability</param>
        /// <param name="pos">Position of the Unit</param>
        public Unit(string name, int id, string flav, string type, int hp, int acc, int ac, int spd, int cmd, Ability ability, List<int> pos) : this()
        {
            this.UnitName = name;
            UnitID = id;
            this.FlavorText = flav;
            this.Type = type;
            this.HP = hp;
            this.Accuracy = acc;
            this.ArmorClass = ac;
            this.Speed = spd;
            this.Command = cmd;
            this.Position = new Tile(pos[0], pos[1], 1);

            List<Ability> aList = new List<Ability>() { ability };
            this.Abilities = aList;
        }

        /// <summary>
        /// Used for explicitly defining a unit.
        /// </summary>
        /// <param name="name">Unit Name</param>
        /// <param name="id">Unit IDno</param>
        /// <param name="flav">Flavor Text</param>
        /// <param name="type">Unit Type</param>
        /// <param name="hp">HP</param>
        /// <param name="acc">Accuracy</param>
        /// <param name="ac">Armor Class</param>
        /// <param name="spd">Speed</param>
        /// <param name="cmd">Command</param>
        /// <param name="ability">Single Ability</param>
        /// <param name="pos">Position of the Unit</param>
        /// <param name="effects">List of active Effects</param>
        /// <param name="alive">Choose 1: Alive / Dead / Banished</param>
        /// <param name="filename">Relative/Absolute path to graphic file</param>
        /// <param name="afilename">Relative/Absolute path to graphic file</param>
        public Unit(string name, int id, string flav, string type, int hp, int acc, int ac, int spd, int cmd, List<Ability> abilities, List<int> pos, List<Effect> effects, string alive, string filename, string afilename)
        {
            this.UnitName = name;
            UnitID = id;
            FlavorText = flav;
            this.Type = type;
            this.HP = hp;
            Accuracy = acc;
            ArmorClass = ac;
            Speed = spd;
            Command = cmd;
            Position = new Tile(pos,0);
            this.Abilities = abilities;
            this.Effects = effects;
            this.Alive = alive;
            this._ImageFile = filename;
            this._ActiveImageFile = afilename;
        }
        #endregion
    }
}
