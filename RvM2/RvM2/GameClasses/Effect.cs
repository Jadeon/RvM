using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RvM2.GameClasses
{
    [Serializable]
    public class Effect
    {
        #region Overrides
        public override string ToString()
        {
            return EffectName + " : " + Duration;
        }
        #endregion

        #region Fields
        private int _Duration = -1;
        public int Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                try
                {
                    if (value < 0)
                    {
                        this._Duration = 0;
                        //throw new ArgumentOutOfRangeException("duration",  "duration of " + value.ToString() + " was negative, using default of 0");                      
                    }
                    else
                    {
                        this._Duration = value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private int _Magnitude = 0;
        public int Magnitude
        {
            get
            {
                return this._Magnitude;
            }
            set
            {
                this._Magnitude = value;
            }
        }

        private string _EffectName = "Undefined Effect";
        public string EffectName
        {
            get
            {
                return this._EffectName;
            }
            set
            {
                this._EffectName = value;
            }
        }
        #endregion

        #region constructors
        public Effect() : this(-1, 0, "default")
        {

        }

        public Effect(int d, string n) : this(d, 0, n)
        {

        }

        public Effect(int d, int m, string n)
        {
            Duration = d;
            Magnitude = m;
            EffectName = n;
        }
        #endregion

        #region Methods
        public void tick()
        {
            Duration--;
        }

        public bool active()
        { return (Duration > 0); }
        #endregion

    }
}
