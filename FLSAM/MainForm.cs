﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using FLAccountDB;
using FLAccountDB.Data;
using FLAccountDB.LoginDB;
using FLSAM.AccountHelper;
using FLSAM.Forms;
using FLSAM.GD;

//using LogDispatcher = LogDispatcher.LogDispatcher;
using FLSAM.Properties;
using LogDispatcher;
using Settings = FLSAM.Forms.Settings;

namespace FLSAM
{


    public partial class MainForm : Form
    {
        private readonly LogDispatcher.LogDispatcher _log = new LogDispatcher.LogDispatcher();
        private readonly List<LogMessage> _logMessages = new List<LogMessage>();
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DBPath != "") return;
            var set = new Settings(_log);
            set.ShowDialog();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {


            DBiFace.InitiateDB(Properties.Settings.Default.DBType,
                Properties.Settings.Default.DBPath,
                Properties.Settings.Default.FLDBPath, _log);

            if (!DBiFace.IsDBAvailable()) return;

            DBiFace.UpdateDB();
        }

        public MainForm()
        {
            InitializeComponent();

            comboSearchLocation.DisplayMember = "Name";

            olvLog.SetObjects(_logMessages);
            _log.SetLogLevel(LogType.Debug);
            _log.LogMessage += _log_LogMessage;

            Universe.StartedLoading += Universe_StartedLoading;
            Universe.DoneLoading += Universe_DoneLoading;

            Universe.Parse(Properties.Settings.Default.FLPath,_log);
            
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


            

            DBiFace.DBPercentChanged += DBiFace_DBPercentChanged;
            DBiFace.DBStateChanged += DBiFace_DBStateChanged;
            DBiFace.OnReadyRequest += DBiFace_OnReadyRequest;
            DBiFace.PlayerDBCommitted += DBiFace_PlayerDBCommitted;
        }

        void DBiFace_PlayerDBCommitted(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Action<object, EventArgs> action = DBiFace_PlayerDBCommitted;
                Invoke(action, null,null);
                return;
            }
            toolDBQueue.Text = @"0";
        }

        void _log_LogMessage(LogMessage message)
        {
            if (InvokeRequired)
            {
                Action<LogMessage> action = _log_LogMessage;
                Invoke(action, message);
                return;
            }
            _logMessages.Add(message);
            olvLog.AddObject(message);
            if (message.Type <= LogType.Error)
                tabControl1.SelectTab(2);
        }

        void Universe_StartedLoading(object sender, EventArgs e)
        {
            equipmentSearchBindingSource.DataSource = null;
            systemsSearchBindingSource.DataSource = null;
            shipsBindingSource.DataSource = null;
            equipmentListBindingSource.DataSource = null;

            fastObjectListView1.GetColumn("Base").AspectToStringConverter =
                value => value.ToString();

            olvRep.GetColumn("Faction").AspectToStringConverter =
                value => value.ToString();

            olvCargo.GetColumn("Item").AspectToStringConverter =
                value => value.ToString();


            fastObjectListView1.GetColumn("Ship").AspectToStringConverter =
                value => value.ToString();

        }

        void Universe_DoneLoading(object sender, EventArgs e)
        {
            equipmentSearchBindingSource.DataSource = Universe.Gis.Equipment;
            systemsSearchBindingSource.DataSource = Universe.Gis.Systems;
            shipsBindingSource.DataSource = Universe.Gis.Ships;
            equipmentListBindingSource.DataSource = Universe.Gis.Equipment;


            fastObjectListView1.GetColumn("Base").AspectToStringConverter =
                value =>
                {
                    if ((string)value == "") return "";
                    if ((string)value == null) return null;
                    var val = Universe.Gis.Bases.FindByNickname((string)value);
                    return val != null ? val.Name : value.ToString();
                };

            olvRep.GetColumn("Faction").AspectToStringConverter =
                value =>
                {
                    if ((string)value == "") return "";
                    var tmp = Universe.Gis.Factions.FirstOrDefault(w => w.Nickname == (string)value);
                    if (tmp != null)
                        return tmp.FactionName;
                    return (string)value;
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
                    var val = Universe.Gis.Ships.FindByHash((uint)value);
                    return val != null ? val.Name : value.ToString();
                    //return val.Name ?? ;
                };

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
            //if (toolProgress.Control.InvokeRequired)
            Invoke(action);
        }

        void DBiFace_OnReadyRequest(List<Metadata> meta)
        {
            fastObjectListView1.SetObjects(meta);
            _log.NewMessage(LogType.Info, "Displayed {0} players",meta.Count);
        }

        private void DBiFace_DBPercentChanged(int percent, int qcount)
        {
            Action action = () =>
            {
                toolProgress.Value = percent;
                toolDBQueue.Text = qcount.ToString(CultureInfo.InvariantCulture);
            };
            if (toolProgress.Control != null)
                toolProgress.Control.Invoke(action);
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBiFace.PlayerDBCommitted -= DBiFace_PlayerDBCommitted;
            DBiFace.CloseDB();
        }


        #region "menustrip events"
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBiFace.IsDBAvailable())
                DBiFace.GetOnlineTable();
            var set = new Settings(_log);
            set.ShowDialog();
        }

        private void rescanDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBiFace.RescanDB(Properties.Settings.Default.DBAggressiveScan);
        }

        private void updateDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBiFace.UpdateDB();
        }

        private void stopScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBiFace.IsDBAvailable())
                DBiFace.AccDB.Scan.Cancel();
        }

        private void saveCurrentCharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveChar();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


        #region "Searching"

        #region "name accid ccode search"
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!DBiFace.IsDBAvailable()) return;
// ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.Retriever.GetFinishWindow += handler,
            handler => DBiFace.AccDB.Retriever.GetFinishWindow += handler,
            700);

            if (radioCharname.Checked)
                DBiFace.AccDB.Retriever.GetMetasByName(textBox1.Text);

            if (radioAccID.Checked)
                DBiFace.AccDB.Retriever.GetAccountChars(textBox1.Text,checkSearchDeleted.Checked);

            if (radioCharCode.Checked)
                DBiFace.AccDB.Retriever.GetMetasByCharID(textBox1.Text);
            if (radioIP.Checked)
                DBiFace.AccDB.Retriever.GetAccountsByIP(textBox1.Text);
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

            if (comboSearchItem.SelectedValue == null) return;

            if (DBiFace.AccDB == null) return;
// ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.Retriever.GetFinishWindow += handler,
            handler => DBiFace.AccDB.Retriever.GetFinishWindow += handler,
            1200);
            DBiFace.AccDB.Retriever.GetMetasByItem((uint)comboSearchItem.SelectedValue);
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
            if (DBiFace.AccDB == null) return;
            // ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.Retriever.GetFinishWindow += handler,
            handler => DBiFace.AccDB.Retriever.GetFinishWindow += handler,
            1200);

            if (radioSearchSystem.Checked)
                DBiFace.AccDB.Retriever.GetMetasBySystem((string)comboSearchLocation.SelectedValue);

            if (radioSearchBase.Checked)
                DBiFace.AccDB.Retriever.GetMetasByBase((string)comboSearchLocation.SelectedValue);
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
            if (md == null) return;
            var ch = md.GetCharacter(Properties.Settings.Default.FLDBPath,_log);
            
            if (ch != null)
            {
                //TODO: that's a breach mate
                ch.AdminRights = DBiFace.AccDB.Scan.IsAdmin(ch.AccountID);
                FillPlayerData(ch);
                return;
            }


            // Fill what we can from metadata
            _log.NewMessage(LogType.Info, "Problem reading account {0} (archived?)", md.Name);

            FillArchivedPlayerData(md);


        }

        private void FillArchivedPlayerData(Metadata md)
        {

            foreach (var b in GetAll(tabPage2,typeof(Button)))
            {
                b.Enabled = false;
            }
            buttonGetAccChars.Enabled = true;
            tabPage5.Enabled = false;

            DBiFace.AccDB.LoginDB.IPDataReady.Add((sender, e) => olvIP.SetObjects((List<IPData>)sender));
            DBiFace.AccDB.LoginDB.GetIPByAccID(md.AccountID);

            DBiFace.AccDB.LoginDB.IDDataReady.Add((sender, e) => olvID.SetObjects((List<IDData>)sender));
            DBiFace.AccDB.LoginDB.GetIDByAccID(md.AccountID);

            textBoxName.Text = md.Name;
            textBoxMoney.Text = md.Money.ToString(CultureInfo.InvariantCulture);

            comboBoxShip.SelectedValue = md.ShipArch;
            checkBanned.Checked = md.IsBanned;
            checkBanned2.Checked = checkBanned.Checked;
            rtbBanReason.Text = Resources.MainForm_Archived_account;

            textAccID.Text = md.AccountID;
            textAdminRights.Text = Resources.MainForm_Archived_account;
            

            FillLocationBox(md.System,md.Base);

            labelHoldSize.Text = "";
            labelHoldCurrent.Text = Resources.MainForm_Archived_account;

            dateLastOnline.MaxDate = md.LastOnline;
            dateLastOnline.Value = md.LastOnline;
            olvRep.Clear();
            dlvEquipment.Clear();
            olvCargo.SetObjects(EquipTable.GetTableFallback(md.Equipment));

            checkIsOnline.Checked = false;

        }

        private void FillLocationBox(string sysNick,string baseNick)
        {
            var sysRow = Universe.Gis.Systems.FindByNickname(sysNick);
            var sysName = sysRow != null ? sysRow.Name : sysNick;

            if (baseNick != null)
            {
                var baseRow = Universe.Gis.Bases.FindByNickname(baseNick);
                var baseName = baseRow != null ? baseRow.Name : baseNick;
                textLocation.Text = String.Format("{0} ({1}), docked at {2}",
                    sysName,
                    sysNick,
                    baseName
                    );
            }
            else
            {
                textLocation.Text = String.Format("{0} ({1}), in space",
                    sysName,
                    sysNick
                    );
            }
        }

        private void FillPlayerData(Character ch)
        {


            _curCharacter = ch;

            foreach (var b in GetAll(tabPage2, typeof(Button)))
            {
                b.Enabled = true;
            }
            tabPage5.Enabled = true;
            //TODO: background
            DBiFace.AccDB.LoginDB.IPDataReady.Add((sender, e) => olvIP.SetObjects((List<IPData>)sender));
            DBiFace.AccDB.LoginDB.GetIPByAccID(_curCharacter.AccountID);
            DBiFace.AccDB.LoginDB.IDDataReady.Add((sender, e) => olvID.SetObjects((List<IDData>)sender));
            DBiFace.AccDB.LoginDB.GetIDByAccID(_curCharacter.AccountID);

            //olvIP.SetObjects();
            textBoxName.Text = _curCharacter.Name;
            textBoxMoney.Text = _curCharacter.Money.ToString(CultureInfo.InvariantCulture);
            //comboBoxSystem.SelectedValue = _curCharacter.System.ToLowerInvariant();
            comboBoxShip.SelectedValue = _curCharacter.ShipArch;
            checkBanned.Checked = ch.IsBanned;
            checkBanned2.Checked = checkBanned.Checked;
            rtbBanReason.Text = DBiFace.AccDB.Bans.GetAccBanReason(_curCharacter.AccountID);

            textAccID.Text = ch.AccountID;
            textAdminRights.Text = ch.AdminRights;

            FillLocationBox(ch.System, ch.Base);




            var ship = Universe.Gis.Ships.FindByHash(_curCharacter.ShipArch);

            if (ship != null)
                labelHoldSize.Text = String.Format("Hold size: {0}", ship.HoldSize);
            RefreshCargoSpace();

            dateLastOnline.MaxDate = DateTime.Now;
            dateLastOnline.Value = _curCharacter.LastOnline;
            olvRep.SetObjects(_curCharacter.Reputation.ToList());
            olvCargo.SetObjects(null);
            olvCargo.SetObjects(_curCharacter.Cargo);


            var eqList = EquipTable.GetTable(_curCharacter, _log);
            dlvEquipment.DataSource = eqList;
            if (DBiFace.IsHookAvailable())
                checkIsOnline.Checked = DBiFace.HookTransport.IsOnServer(_curCharacter.Name);

            textCreatedAt.Text = _curCharacter.Created.ToLongDateString();
        }

        private void RefreshCargoSpace()
        {
            if (_curCharacter == null) return;
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

        private void SaveChar()
        {
            if (_curCharacter == null) return;

            
            _curCharacter.Cargo = new List<WTuple<uint, uint>>();
            _curCharacter.Cargo.AddRange((IEnumerable<WTuple<uint,uint>>)olvCargo.Objects);


            _curCharacter.EquipmentList = EquipTable.GetEquipmentList((uiTables.ShipEquipDataTable)dlvEquipment.DataSource);

            _curCharacter.SaveCharacter(Properties.Settings.Default.FLDBPath,_log);
            ((Metadata) fastObjectListView1.SelectedObject).Money = _curCharacter.Money;
            ((Metadata)fastObjectListView1.SelectedObject).Name = _curCharacter.Name;
            ((Metadata)fastObjectListView1.SelectedObject).Base = _curCharacter.Base;
            ((Metadata)fastObjectListView1.SelectedObject).Rank = _curCharacter.Rank;
            ((Metadata)fastObjectListView1.SelectedObject).ShipArch = _curCharacter.ShipArch;
            ((Metadata)fastObjectListView1.SelectedObject).LastOnline = _curCharacter.LastOnline;
            fastObjectListView1.RefreshObject(_curCharacter);
            DBiFace.AccDB.Scan.LoadAccountDirectory(Properties.Settings.Default.FLDBPath + "/" + _curCharacter.AccountID);
        }




        #region "main tab"

        private void buttonLocation_Click(object sender, EventArgs e)
        {
            if (_curCharacter == null) return;
            var locDlg = new CharLocation(_curCharacter);

            if (locDlg.ShowDialog(this) != DialogResult.OK) return;

            FillPlayerData(locDlg.Char);

        }

        private void textBoxMoney_TextChanged(object sender, EventArgs e)
        {
            if (_curCharacter == null) return;
            _curCharacter.Money = uint.Parse(textBoxMoney.Text);
        }

        private void buttonLastOReset_Click(object sender, EventArgs e)
        {
            if (_curCharacter == null) return;

            var dt = DateTime.Now;
            dateLastOnline.MaxDate = dt;
            dateLastOnline.Value = dt;

            _curCharacter.LastOnline = dateLastOnline.Value;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveChar();
        }


        #endregion



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

        private void buttonAddHP_Click(object sender, EventArgs e)
        {
            ((uiTables.ShipEquipDataTable) dlvEquipment.DataSource).AddShipEquipRow("", EquipTypes.Engine.ToString(), "", "");
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

        private void buttonAddCargo_Click(object sender, EventArgs e)
        {
            _curCharacter.Cargo.Add(new WTuple<uint, uint>(0,1));
            olvCargo.SetObjects(_curCharacter.Cargo);
        }

        #endregion





        #endregion

        private void olvLog_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void buttonGetAdmins_Click(object sender, EventArgs e)
        {
            DBiFace.AccDB.Retriever.GetMetasOfAdmins();
        }

        private void radioCharname_CheckedChanged(object sender, EventArgs e)
        {
            labelWildcards.Visible = radioCharname.Checked;
        }

        private void radioIP_CheckedChanged(object sender, EventArgs e)
        {
            labelWildcards.Visible = radioIP.Checked;
        }

        private void olvIP_CellRightClick(object sender, CellRightClickEventArgs e)
        {

            if (e.Model != null)
            {
                olvIP.SelectedIndex = e.RowIndex;
                e.MenuStrip = cmsOlvIP;
            }
                
        }

        private void searchThisIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            radioIP.Checked = true;
            textBox1.Text = ((IPData) olvIP.SelectedObject).IP;
            button1_Click_1(null,null);
        }

        private void buttonGetBanned_Click(object sender, EventArgs e)
        {
            DBiFace.AccDB.Retriever.GetBanned();
        }

        private void buttonGetAccChars_Click(object sender, EventArgs e)
        {
            if (textAccID.Text == "") return;
            tabControl1.SelectTab(0);
            radioAccID.Checked = true;
            textBox1.Text = textAccID.Text;
            button1_Click_1(null, null);
        }

        private void buttonBan_Click(object sender, EventArgs e)
        {
            if (_curCharacter == null) return;
            DBiFace.AccDB.Bans.AccountBan(_curCharacter.AccountID,rtbBanReason.Text);
        }

        private void buttonUnban_Click(object sender, EventArgs e)
        {
            if (_curCharacter == null) return;
            DBiFace.AccDB.Bans.AccountUnban(_curCharacter.AccountID);
        }





        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            var enumerable = controls as IList<Control> ?? controls.ToList();
            return enumerable.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(enumerable)
                                      .Where(c => c.GetType() == type);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DBiFace.AccDB == null) return;
            DBiFace.AccDB.RemoveAccount(_curCharacter.CharPath,_curCharacter.AccountID,_curCharacter.CharID);
            fastObjectListView1.RemoveObject(_curCharacter);
            
        }

        private void olvIP_FormatRow(object sender, FormatRowEventArgs e)
        {
            var ipData = (IPData)e.Model;
            if (!DBiFace.AccDB.BansIP.IsBanned(ipData.IP)) return;

            e.Item.BackColor = Color.DarkRed;
            e.Item.ForeColor = Color.WhiteSmoke;
        }

        private void olvID_FormatRow(object sender, FormatRowEventArgs e)
        {
            var ipData = (IDData)e.Model;
            if (!DBiFace.AccDB.BansIP.IsBanned(ipData.ID1) 
                && 
                !DBiFace.AccDB.BansIP.IsBanned(ipData.ID2)) 
                return;

            e.Item.BackColor = Color.DarkRed;
            e.Item.ForeColor = Color.WhiteSmoke;
        }

        



    }
}
