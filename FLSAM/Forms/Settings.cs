using System;
using System.Windows.Forms;
using FLSAM.GD;

namespace FLSAM.Forms
{
    public partial class Settings : Form
    {
        private readonly LogDispatcher.LogDispatcher _log;
        public Settings(LogDispatcher.LogDispatcher log)
        {
            _log = log;
            InitializeComponent();
            comboBackendSelector.SelectedIndex = Properties.Settings.Default.DBType;
            panelCleanupBackup.Enabled = (Properties.Settings.Default.CleanupBackup && (Properties.Settings.Default.DBType == 0));
        }

        private void comboBackendSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBackendSelector.SelectedIndex == 0)
            {
                tabFLDB.Enabled = true;
                tabHook.Enabled = true;
            }
            else
            {
                tabFLDB.Enabled = false;
                tabHook.Enabled = false;
            }
            panelCleanupBackup.Enabled = (checkBoxCleanBackup.Checked && (comboBackendSelector.SelectedIndex == 0));
        }

        private void buttonSQLPathSelector_Click(object sender, EventArgs e)
        {
            if (openFileSQLPicker.ShowDialog() == DialogResult.OK)
            {
                textBoxSQLPath.Text = openFileSQLPicker.FileName;
            }
        }

        private void buttonClearSQL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure you want to clear playerDB?", 
                @"DB initiation", 
                MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                DBiFace.PurgeDB();
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DBType = comboBackendSelector.SelectedIndex;
            Properties.Settings.Default.Save();

            DBiFace.ReloadDB(_log);

            if (DBiFace.AccDB == null) return;
            DBiFace.AccDB.Queue.SetThreshold((int)numericUpDown3.Value);
            DBiFace.AccDB.Queue.SetTimeout((int)numericUpDown4.Value);
        }

        private void buttonFLDBPathSelector_Click(object sender, EventArgs e)
        {
            if (folderBrowserFLDBPath.ShowDialog() == DialogResult.OK)
            {
                textBoxFLDBPath.Text = folderBrowserFLDBPath.SelectedPath;
            }
        }

        private void buttonFLPath_Click(object sender, EventArgs e)
        {
            var br = new FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };
            if (br.ShowDialog() == DialogResult.OK)
                textFLPath.Text = br.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var br = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
            if (br.ShowDialog() == DialogResult.OK)
                textCleanupBackup.Text = br.SelectedPath;
        }

        private void checkBoxCleanBackup_CheckedChanged(object sender, EventArgs e)
        {
            panelCleanupBackup.Enabled = (checkBoxCleanBackup.Checked && (comboBackendSelector.SelectedIndex == 0));
        }

        private void buttonSQOpen_Click(object sender, EventArgs e)
        {
            DBiFace.ReloadDB(_log);

            if (DBiFace.IsDBAvailable()) return;
            var myToolTip = new ToolTip {IsBalloon = true};
            myToolTip.Show("Please select the Freelancer DB path then open SQL DB again!", buttonSQOpen);
        }

        private void buttonFLRescan_Click(object sender, EventArgs e)
        {
// ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => Universe.DoneLoading += handler,
            handler => Universe.DoneLoading += handler,
            1000);
            Universe.Parse(Properties.Settings.Default.FLPath, _log,true);
        }
    }
}
