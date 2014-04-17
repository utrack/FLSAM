using System;
using System.Windows.Forms;
using FLAccountDB;
using FLAccountDB.Data;
using FLSAM.GD;

namespace FLSAM.Forms
{
    public partial class CharLocation : Form
    {

        public Character Char { get; set; }

        public CharLocation(Character ch)
        {
            InitializeComponent();
            systemsBindingSource.DataSource = Universe.Gis.Systems;
            basesBindingSource.DataSource = Universe.Gis.Bases;
            Char = ch;
            textNick.Text = Char.Name;

            var formheight = Height - ClientSize.Height;
            Height = groupBox1.Height + groupDocked.Height + formheight;

            if (ch.Base == null) checkSpace.Checked = true;

            numX.Value = (decimal)ch.Position[0];
            numY.Value = (decimal)ch.Position[1];
            numZ.Value = (decimal)ch.Position[2];
        }

        private void checkNick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNick.Checked)
            {
                comboBase.DisplayMember = "Nickname";
                comboSystem.DisplayMember = "Nickname";
            }
            else
            {
                comboBase.DisplayMember = "Name";
                comboSystem.DisplayMember = "Name";
            }
        }

        private void CharLocation_Shown(object sender, EventArgs e)
        {
            comboBase.SelectedValue = Char.Base ?? Char.LastBase;
            comboSystem.SelectedValue = Char.System;
            basesBindingSource.Filter = String.Format("System LIKE '{0}' And System <> ''", comboSystem.SelectedValue);
        }

        private void comboSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            basesBindingSource.Filter = String.Format("System LIKE '{0}' And System <> ''", comboSystem.SelectedValue);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Char.System = (string)comboSystem.SelectedValue;
            if (!checkSpace.Checked)
            {
                Char.Base = (string) comboBase.SelectedValue;
                Char.LastBase = (string)comboBase.SelectedValue;
                Char.Position = null;
                Char.Rotation = null;
                return;
            }

            Char.Base = null;
            Char.LastBase = (string)comboBase.SelectedValue;
            Char.Position = new[]
            {
                (float) numX.Value,
                (float) numY.Value,
                (float) numZ.Value
            };
        }

        private void checkSpace_CheckedChanged(object sender, EventArgs e)
        {
            var formheight = Height - ClientSize.Height;
            Height = groupBox1.Height + groupDocked.Height + formheight;
            if (checkSpace.Checked)
                Height += groupSpace.Height;
        }
    }
}
