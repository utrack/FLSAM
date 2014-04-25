using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FLAccountDB;
using FLAccountDB.Data;
using FLAccountDB.NoSQL;
using FLHookTransport;
using FLSAM.Forms;
using FLSAM.GD;
using FLSAM.GD.DB;
using LogDispatcher;

namespace FLSAM
{
    public static class DBiFace
    {
        //TODO: select between FOS and NoSQL
        public static NoSQLDB AccDB;
        public static Transport HookTransport;

        public static void GetOnlineTable()
        {
            if (AccDB == null || HookTransport == null) return;

            if (HookTransport.IsSocketOpen())
                AccDB.Retriever.GetMetasByNames(
                    HookTransport.GetPlayersOnline()
                        .Select(w => w.CharName)
                        .ToList()
                    );
        }

        private static string _path1;

        #region "DB jobs"

        public static bool IsDBAvailable()
        {
            return AccDB != null && AccDB.IsReady();
        }

        private static LogDispatcher.LogDispatcher _log;

        //TODO: select between FOS and NoSQL
        public static void InitiateDB(decimal dbType, string path1, string path2,LogDispatcher.LogDispatcher log)
        {
            _log = log;
            if (path1 == "" || path2 == "") return;

            if (AccDB != null)
            {
                AccDB.Scan.ProgressChanged -= _accDB_ProgressChanged;
                AccDB.StateChanged -= _accDB_StateChanged;
                AccDB.CloseDB();
            }
                

            AccDB = new NoSQLDB(path1, path2,log);
            AccDB.Scan.ProgressChanged += _accDB_ProgressChanged;
            AccDB.StateChanged += _accDB_StateChanged;
            AccDB.Retriever.GetFinish += AccDB_OnGetFinish;
            AccDB.Queue.Committed += Queue_Committed;
            AccDB.Queue.SetThreshold((int)Properties.Settings.Default.TuneQThreshold);
            AccDB.Queue.SetTimeout((int)Properties.Settings.Default.TuneQTimer);
            AccDB.Scan.AccountScanned += Scan_AccountScanned;
            _path1 = path1;


            if (!IsDBAvailable())
            {
                //Database not found/set up
                var set = new Settings(log);
                set.ShowDialog();
                return;
            }

            if (DBCountRows("Accounts") != 0) return;
            if (MessageBox.Show(
                @"DB is not initialized. Initialize and scan now?",
                @"Database is empty",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            //Database is empty
            RescanDB(Properties.Settings.Default.DBAggressiveScan);
        }


        /// <summary>
        /// Handles account checking.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        static Character Scan_AccountScanned(Character ch, System.ComponentModel.CancelEventArgs e)
        {
            if (!Universe.IsAttached)
            {
                e.Cancel = true;
                return ch;
            }



            var changedAcc = false;
            var shipdata = Universe.Gis.Ships.FindByHash(
                ch.ShipArch
                );

            if (shipdata == null)
            {
                _log.NewMessage(LogType.Warning, "Unknown shiparch: {0} for {1}",
                    ch.ShipArch,
                ch.Name);
                e.Cancel = true;
                //TODO: add to non-parsed userlist
                return ch;
            }

            //var acc = meta.GetCharacter(Properties.Settings.Default.FLDBPath,_log);
            var defaults = shipdata.GetShipDefaultInternalsRows();

            if (defaults.Length == 0)
            {
                _log.NewMessage(LogType.Error,"No default loadout for {0} {1} ({2})",ch.Name,shipdata.Nickname,shipdata.Name);
                return ch;
            }

            GameInfoSet.HardpointsRow[] hpData = null;
            if (Properties.Settings.Default.FLDBCheckIncompatibleHardpoints)
                hpData = Universe.Gis.Ships.FindByHash(ch.ShipArch).GetHardpointsRows();
                
            

            var foundEngine = false;
            var foundPower = false;
            Tuple<uint, string, float> powerToReplace = null;
            var equipToRemove = new List<Tuple<uint, string, float>>();

            foreach (var equip in ch.EquipmentList)
            {
                var eqItem = Universe.Gis.Equipment.FindByHash(equip.Item1);

                if (eqItem == null)
                {
                    _log.NewMessage(LogType.Error, "Unknown equipment: {0} {1} on {2}", ch.Name, equip.Item1, equip.Item2);
                    continue;
                }
                    

                if (eqItem.Type == EquipTypes.Engine.ToString())
                    foundEngine = true;
                else if (eqItem.Type == EquipTypes.Powerplant.ToString())
                {
                    foundPower = true;
                    if (eqItem.Hash != defaults[0].DPower)
                    {
                        _log.NewMessage(LogType.Warning,
                        "Non-standard powerplant for {0}: {1}, should be {2}",
                        ch.Name,
                        //TODO:change to eqItem
                        Universe.Gis.Equipment.FindByHash(equip.Item1).Nickname,
                        Universe.Gis.Equipment.FindByHash(defaults[0].DPower).Nickname
                        );
                        if (Properties.Settings.Default.FLDBGoForDefaultPPlant)
                            powerToReplace = equip;

                    }
                }

                if (!Properties.Settings.Default.FLDBCheckIncompatibleHardpoints) continue;

                if (equip.Item2 == "") continue;
                //var tmp = ;
                var firstOrDefault = hpData.FirstOrDefault(row => row.Name == equip.Item2);
                if (firstOrDefault == null) continue;
                if (firstOrDefault.HPType.Contains(eqItem.Hardpoint)) continue;

                //Unmount incompatible equip
                var tmp1 = equip;
                equipToRemove.Add(equip);
                _log.NewMessage(LogType.Info, "Unmounting {0} on {1} ({2}), ship {3} ({4})...",eqItem.Nickname,equip.Item2,ch.Name,shipdata.Nickname,shipdata.Name);
                ch.Cargo.Add(new WTuple<uint, uint>(tmp1.Item1,1));
                changedAcc = true;
            }

            if (!foundEngine)
            {
                _log.NewMessage(LogType.Warning,"No engine for char {0}! Adding default...",ch.Name);
                // Get first engine HP available
                var engineHp = shipdata.GetHardpointsRows().FirstOrDefault(w => w.EquipType == EquipTypes.Engine.ToString());

                //fallback to internal if none found
                var hp = "";

                if (engineHp != null)
                    hp = engineHp.Name;
                ch.EquipmentList.Add(new Tuple<uint, string, float>(defaults[0].DEngine,hp,1));
                changedAcc = true;
            }

            if (!foundPower | (powerToReplace != null))
            {
                if (powerToReplace != null)
                    ch.EquipmentList.Remove(powerToReplace);

                _log.NewMessage(LogType.Warning, "Adding default powerplant for char {0}...", ch.Name);

                var powerHp =
                    shipdata.GetHardpointsRows().FirstOrDefault(w => w.EquipType == EquipTypes.Powerplant.ToString());
                var hp = "";

                if (powerHp != null)
                    hp = powerHp.Name;
                ch.EquipmentList.Add(new Tuple<uint, string, float>(defaults[0].DPower, hp, 1));
                changedAcc = true;
            }


            if (!changedAcc | Properties.Settings.Default.FLDBReadOnlyChecks) return ch;

            foreach (var rEq in equipToRemove)
                ch.EquipmentList.Remove(rEq);

            ch.SaveCharacter(Properties.Settings.Default.FLDBPath, _log);
            return ch;
        }

        


        public static event EventHandler PlayerDBCommitted;
        static void Queue_Committed(object sender, EventArgs e)
        {
            if (PlayerDBCommitted != null)
                PlayerDBCommitted(null, null);
        }

        
        public static void PurgeDB()
        {

            if (IsDBAvailable())
                AccDB.CloseDB();
            if (File.Exists(_path1))
                File.Delete(_path1);

        }

        /// <summary>
        /// Cleans DB queue, closes the connection and attempts to initiate new database @ specified location.
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="log"></param>
        public static void ReloadDB(string path1,string path2,LogDispatcher.LogDispatcher log)
        {
            //var path = _accDB.AccPath;
            if (IsDBAvailable())
                AccDB.CloseDB();

            InitiateDB(0,path1,path2,log);
        }
        public static void ReloadDB(LogDispatcher.LogDispatcher log)
        {
            ReloadDB(Properties.Settings.Default.DBPath,Properties.Settings.Default.FLDBPath,log);
        }

        public static void CloseDB()
        {
            if (AccDB != null)
            {
                AccDB.CloseDB();
            }    
        }

        public static int DBCountRows(string table)
        {
            return IsDBAvailable() ? AccDB.Retriever.CountRows(table) : 0;
        }

        /// <summary>
        /// Recreates the database's contents.
        /// </summary>
        /// <param name="aggro"></param>
        public static void RescanDB(bool aggro)
        {
            if (!IsDBAvailable())
            {
                _log.NewMessage(LogType.Info, "Tried to initiate playerDB when none present.");
            }

            if (!Universe.IsAttached)
            {
                _log.NewMessage(LogType.Warning, "Scanning aborted! Universe not loaded.");
                return;
            }
                
            
            AccDB.Scan.LoadDB(aggro);
            Properties.Settings.Default.LastDBUpdate = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Updates the DB.
        /// </summary>
        public static void UpdateDB()
        {
            if (!Universe.IsAttached)
            {
                _log.NewMessage(LogType.Warning, "Update aborted! Universe not loaded.");
                return;
            }
            AccDB.Scan.Update(Properties.Settings.Default.LastDBUpdate);
            
        }

        #endregion


        #region "DB events"

        public static event Scanner.PercentageChanged DBPercentChanged;
        //public delegate void ProgressChanged(int percent, int qCount);
        private static void _accDB_ProgressChanged(int percent, int qcount)
        {
            if (DBPercentChanged != null)
                DBPercentChanged(percent, qcount);
        }


        public static event NoSQLDB.StateChange DBStateChanged;
        //public delegate void StateChange(DBStates state);

        //TODO if you see me wipe dis
        //wait, huh... what did i meant when i wrote it.
        //past me, yer really,really unhelpful
        //TODO: figure this out
        public static event EventHandler DBRenew;
        static void _accDB_StateChanged(DBStates state)
        {
            if (DBStateChanged != null)
                DBStateChanged(state);
            if (state == DBStates.Ready)
            {
                Properties.Settings.Default.LastDBUpdate = DateTime.Now;
                Properties.Settings.Default.Save();
                return;
            }
            if (DBRenew != null)
                DBRenew(null,null);
        }

        public static event DBCrawler.RequestReady OnReadyRequest;
        static void AccDB_OnGetFinish(List<Metadata> meta)
        {
            if (OnReadyRequest != null)
                OnReadyRequest(meta);
        }




        #endregion



       

        #region "Hook jobs"

        /// <summary>
        /// Connects Hook's transport.
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="password"></param>
        /// <param name="log"></param>
        public static void InitiateHook(string addr, int port, string password,LogDispatcher.LogDispatcher log)
        {
            HookTransport = new Transport(log);
            HookTransport.OpenSocket(addr, port, password);
        }

        public static bool IsHookAvailable()
        {
            return HookTransport != null && HookTransport.IsSocketOpen();
        }

        #endregion

    }
}
