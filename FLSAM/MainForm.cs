using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BrightIdeasSoftware;
using FLAccountDB;
using FLAccountDB.NoSQL;
using FLSAM.AccountHelper;
using FLSAM.Forms;
using FLSAM.GD;

namespace FLSAM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //Universe.Parse(@"g:\Games\freelancer\fl-Disc487\dev");
            equipmentSearchBindingSource.DataSource = Universe.Gis.Equipment;
            systemsSearchBindingSource.DataSource = Universe.Gis.Systems;
            shipsBindingSource.DataSource = Universe.Gis.Ships;
            equipmentListBindingSource.DataSource = Universe.Gis.Equipment;
            //ObjectListView.EditorRegistry.Register(typeof(float), typeof(NumericUpDown));

            ObjectListView.EditorRegistry.Register(typeof(float), delegate(Object model, OLVColumn column, Object value)
            {
                var nu = new NudFloat
                {
                    Maximum = 1, 
                    Minimum = -1, 
                    DecimalPlaces = 3, 
                    Increment = (decimal)0.1,
                    Value = (float)value
                };
                return nu;
            });

            ObjectListView.EditorRegistry.Register(typeof(uint), delegate(Object model, OLVColumn column, Object value)
            {
                var nu = new NudUint
                {
                    Maximum = uint.MaxValue,
                    Minimum = uint.MinValue,
                    Increment = 1,
                    Value = (uint)value
                };
                return nu;
            });


            fastObjectListView1.GetColumn("Base").AspectToStringConverter =
                value =>
                {
                    if ((string) value == "") return "";
                    var val = Universe.Gis.Bases.FindByNickname((string) value);
                    return val != null ? val.Name : value.ToString();
                };

            olvRep.GetColumn("Faction").AspectToStringConverter =
                value =>
                {
                    if ((string)value == "") return "";
                    var tmp = Universe.Gis.Factions.FirstOrDefault(w => w.Nickname == (string) value);
                    if (tmp != null)
                        return tmp.FactionName;
                    return (string) value;
                };

            olvCargo.GetColumn("Item").AspectToStringConverter =
    
                value =>
                {
                    if (value.ToString() == "") return "";
                    var tmp = Universe.Gis.Equipment.FindByHash((uint)value);
                    return tmp != null ? tmp.Nickname : value.ToString();
                };


            fastObjectListView1.GetColumn("Ship").AspectToStringConverter =
                value =>
                {
                    if (value == null) return "";
                    //TODO: dropped once?
                    var val = Universe.Gis.Ships.FindByHash((uint) value);
                    return val != null ? val.Name : value.ToString();
                    //return val.Name ?? ;
                };

            DBiFace.DBPercentChanged += DBiFace_DBPercentChanged;
            DBiFace.DBStateChanged += DBiFace_DBStateChanged;
            DBiFace.OnReadyRequest += DBiFace_OnReadyRequest;
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            DBiFace.InitiateDB(Properties.Settings.Default.DBType,
                Properties.Settings.Default.DBPath,
                Properties.Settings.Default.FLDBPath);

            if (!DBiFace.IsDBAvailable()) return;

            DBiFace.UpdateDB();
        }

        #region "dbiface events"
        void DBiFace_DBStateChanged(DBStates state)
        {
            var str = "";
            switch (state)
            {
                case DBStates.Ready:
                        str = "DB Ready";
                        break;
                    case DBStates.Initiating:
                        str = "Rescanning...";
                        break;
                    case DBStates.Closed:
                        str = "Closed";
                        break;
                    case DBStates.UpdatingFormFiles:
                        str = "Searching for updates...";
                        break;
                    case DBStates.Updating:
                        str = "Updating...";
                        break;
            }

            Action action = () => toolStatus.Text = str;
            //TODO: update the main grid
            //if (toolProgress.Control.InvokeRequired)
            Invoke(action);
        }

        void DBiFace_OnReadyRequest(List<Metadata> meta)
        {
            fastObjectListView1.SetObjects(meta);
        }

        private void DBiFace_DBPercentChanged(int percent, int qcount)
        {
            Action action = () =>
            {
                toolProgress.Value = percent;
                toolDBQueue.Text = qcount.ToString(CultureInfo.InvariantCulture);
            };
            //if (toolProgress.Control.InvokeRequired)
            toolProgress.Control.Invoke(action);
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBiFace.CloseDB();
        }


        #region "menustrip events"
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBiFace.IsDBAvailable())
                DBiFace.GetOnlineTable();
            var set = new Settings();
            set.ShowDialog();
        }

        private void rescanDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBiFace.InitDB(Properties.Settings.Default.DBAggressiveScan);
        }
        #endregion


        #region "Searching"

        #region "name accid ccode search"
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!DBiFace.IsDBAvailable()) return;
// ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            700);

            if (radioCharname.Checked)
                DBiFace.AccDB.GetMetasByName(textBox1.Text);

            if (radioAccID.Checked)
                DBiFace.AccDB.GetAccountChars(textBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click_1(null, null);
            }
        }


        #endregion

        #region "item search"
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboSearchItem.DisplayMember = "Name";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboSearchItem.DisplayMember = "Nickname";
        }

        private void button2_Click(object sender, EventArgs e)
        {
// ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            1200);
            DBiFace.AccDB.GetMetasByItem((uint)comboSearchItem.SelectedValue);
        }
        #endregion

        #region "location search"

        private void radioSearchSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchSystem.Checked)
                systemsSearchBindingSource.DataSource = Universe.Gis.Systems;

        }

        private void radioSearchBase_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchBase.Checked)
                systemsSearchBindingSource.DataSource = Universe.Gis.Bases;
        }

        private void radioSearchLocName_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchLocName.Checked)
                comboSearchLocation.DisplayMember = "Name";
        }

        private void radioSearchLocNickname_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchLocNickname.Checked)
                comboSearchLocation.DisplayMember = "Nickname";
        }

        private void comboSearchLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                buttonSearchLocation_Click(null,null);
            }
        }

        private void buttonSearchLocation_Click(object sender, EventArgs e)
        {
            // ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            1200);

            if (radioSearchSystem.Checked)
                DBiFace.AccDB.GetMetasBySystem((string)comboSearchLocation.SelectedValue);
        }
        #endregion

        #endregion


        #region "Character tab"
        private void fastObjectListView1_SelectionChanged(object sender, EventArgs e)
        {
            FillPlayerData((Metadata)fastObjectListView1.SelectedObject);
        }

        private Character _curCharacter;
        private void FillPlayerData(Metadata md)
        {
            _curCharacter = md.GetCharacter(Properties.Settings.Default.FLDBPath);
            textBoxName.Text = _curCharacter.Name;
            textBoxMoney.Text = _curCharacter.Money.ToString(CultureInfo.InvariantCulture);
            //comboBoxSystem.SelectedValue = _curCharacter.System.ToLowerInvariant();
            comboBoxShip.SelectedValue = _curCharacter.ShipArch;


            var sysRow = Universe.Gis.Systems.FindByNickname(_curCharacter.System);
            var sysName = sysRow != null ? sysRow.Name : _curCharacter.System;


            if (_curCharacter.Base != null)
            {
                var baseRow = Universe.Gis.Bases.FindByNickname(_curCharacter.Base);
                var baseName = baseRow != null ? baseRow.Name : _curCharacter.Base;
                textLocation.Text = String.Format("{0} ({1}), docked at {2}",
                    sysName,
                    _curCharacter.System,
                    baseName
                    );
            }
            else
            {
                textLocation.Text = String.Format("{0} ({1}), in space",
                    sysName,
                    _curCharacter.System
                    );
            }




            var ship = Universe.Gis.Ships.FindByHash(_curCharacter.ShipArch);
            
            if (ship != null)
                labelHoldSize.Text = String.Format("Hold size: {0}", ship.HoldSize);
            RefreshCargoSpace();

            dateLastOnline.MaxDate = DateTime.Now;
            dateLastOnline.Value = _curCharacter.LastOnline;
            olvRep.SetObjects(_curCharacter.Reputation.ToList());
            olvCargo.SetObjects(_curCharacter.Cargo);


            var eqList = EquipTable.GetTable(_curCharacter);
            dlvEquipment.DataSource = eqList;
            if (DBiFace.IsHookAvailable())
                checkIsOnline.Checked = DBiFace.HookTransport.IsOnServer(_curCharacter.Name);

            textCreatedAt.Text = _curCharacter.Created.ToLongDateString();
        }

        private void RefreshCargoSpace()
        {
            var curHold = (from hold in _curCharacter.Cargo 
                           let item = Universe.Gis.Equipment.FindByHash(hold.Item1) 
                           where item != null 
                           select item.Volume*hold.Item2).Sum() 
                           + _curCharacter.EquipmentList.Select(
                           hold => Universe.Gis.Equipment.FindByHash(hold.Item1)
                           )
                           .Where(item => item != null)
                           .Sum(item => item.Volume);


            labelHoldCurrent.Text = String.Format("Current: {0:0.00}", curHold);
        }

        private void buttonLastOReset_Click(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            dateLastOnline.MaxDate = dt;
            dateLastOnline.Value = dt;
        }

        #region "reputation tab"
        private void numericRep_ValueChanged(object sender, EventArgs e)
        {

            ((ReputationItem) olvRep.SelectedObject).Value = (float)numericRep.Value;
        }

        private void olvRep_SelectionChanged(object sender, EventArgs e)
        {
            if (olvRep.SelectedObject == null) return;
            numericRep.Value = (decimal)((ReputationItem)olvRep.SelectedObject).Value; //olvRep.SelectedObject+		
        }
        #endregion






        #region "equipment tab"
        private void dlvEquipment_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.Column.Text == @"Equipment")
            {
                if ((string) ((DataRowView) e.RowObject).Row.ItemArray[3] == "")
                    equipmentListBindingSource.Filter = String.Format("Type = '{0}'",
                        ((DataRowView) e.RowObject).Row.ItemArray[1]);
                else
                {
                    
                    var hpTypes = ((string) ((DataRowView) e.RowObject).Row.ItemArray[3]).Split(' ');
                    string filterStr;
                    if (hpTypes.Length > 1)
                        //ladies ang gentlemen, the slowest and dirtiest piece of code there.
                        filterStr = String.Format(@"Hardpoint LIKE '%{0}%'",
                            String.Join(@"%' OR Hardpoint LIKE '%", hpTypes));
                    else
                        filterStr = String.Format(@"Hardpoint LIKE '*{0}*'", hpTypes[0]);
                    equipmentListBindingSource.Filter = filterStr;
                }
                    

                var cbEd = new ComboBox
                {
                    Bounds = e.CellBounds,
                    SelectedValue = e.Value,
                    DataSource = equipmentListBindingSource,
                    DisplayMember = "Nickname",
                    ValueMember = "Nickname",
                    AutoCompleteMode = AutoCompleteMode.SuggestAppend
                };
                e.Control = cbEd;
            }

            if (e.Column.Text == @"Type")
            {
                if ((string) ((DataRowView) e.RowObject).Row.ItemArray[0] != "")
                {
                    e.Cancel = true;
                    return;
                }

                var cbEd = new ComboBox
                {
                    Bounds = e.CellBounds,
                    DisplayMember = "ToString",
                    DropDownStyle = ComboBoxStyle.DropDownList,
                   
                };
                e.Control = cbEd;
                cbEd.DataSource = Enum.GetValues(typeof (EquipTypes));
                cbEd.SelectedText = (string)e.Value;

            }
        }


        #endregion


        #region "cargo tab"
        private void olvCargo_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.Column.Text != @"Item") return;
            var cbEd = new ComboBox
            {
                Bounds = e.CellBounds,
                DisplayMember = "Nickname",
                ValueMember = "Hash",
            };
            e.Control = cbEd;
            cbEd.DataSource = equipmentListBindingSource;
            cbEd.SelectedValue = e.Value;
        }

        private void olvCargo_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            RefreshCargoSpace();
        }

        #endregion





        #endregion

    }
}
