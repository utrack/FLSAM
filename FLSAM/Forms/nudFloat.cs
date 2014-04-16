using System.Windows.Forms;

namespace FLSAM.Forms
{
    public class NudFloat : NumericUpDown
    {
        public new float Value
        {
            get { return (float)base.Value; }
            set { base.Value = (decimal)value; }
        }
    }


    public class NudUint : NumericUpDown
    {
        public new uint Value
        {
            get { return (uint)base.Value; }
            set { base.Value = value; }
        }
    }
}
