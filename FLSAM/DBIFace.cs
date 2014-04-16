using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FLAccountDB.NoSQL;
using FLHookTransport;
using FLSAM.Forms;

namespace FLSAM
{
    public static class DBiFace
    {
        //TODO: select between FOS and NoSQL
        public static NoSQLDB AccDB;
        public static Transport HookTransport;

        private static string _path1;

        #region "DB jobs"

        public static bool IsDBAvailable()
        {
            if (AccDB == null) return false;
            return AccDB.IsReady();
        }
        //TODO: select between FOS and NoSQL
        public static void InitiateDB(decimal dbType, string path1, string path2)
        {
            if (path1 == "" || path2 == "") return;

            if (AccDB != null)
            {
                AccDB.ProgressChanged -= _accDB_ProgressChanged;
                AccDB.StateChanged -= _accDB_StateChanged;
            }
                

            AccDB = new NoSQLDB(path1, path2);
            AccDB.ProgressChanged += _accDB_ProgressChanged;
            AccDB.StateChanged += _accDB_StateChanged;
            AccDB.OnGetFinish += AccDB_OnGetFinish;
            AccDB.Queue.SetThreshold((int)Properties.Settings.Default.TuneQThreshold);
            AccDB.Queue.SetTimeout((int)Properties.Settings.Default.TuneQTimer);
            _path1 = path1;


            if (!IsDBAvailable())
            {
                //Database not found/set up
                var set = new Settings();
                set.ShowDialog();
                return;
            }

            if (DBCountRows("Accounts") != 0) return;
            if (MessageBox.Show(
                @"DB is not initialized. Initialize and scan now?",
                @"Database is empty",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            //Database is empty
            InitDB(Properties.Settings.Default.DBAggressiveScan);
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
        public static void ReloadDB(string path1,string path2)
        {
            //var path = _accDB.AccPath;
            if (IsDBAvailable())
                AccDB.CloseDB();

            InitiateDB(0,path1,path2);
        }
        public static void ReloadDB()
        {
            ReloadDB(Properties.Settings.Default.DBPath,Properties.Settings.Default.FLDBPath);
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
            return IsDBAvailable() ? AccDB.CountRows(table) : 0;
        }

        /// <summary>
        /// Recreates the database's contents.
        /// </summary>
        /// <param name="aggro"></param>
        public static void InitDB(bool aggro)
        {
            if (!IsDBAvailable()) return;
            AccDB.LoadDB(aggro);
            Properties.Settings.Default.LastDBUpdate = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Updates the DB.
        /// </summary>
        public static void UpdateDB()
        {
            AccDB.Update(Properties.Settings.Default.LastDBUpdate);
            Properties.Settings.Default.LastDBUpdate = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        #endregion


        #region "DB events"

        public static event NoSQLDB.PercentageChanged DBPercentChanged;
        //public delegate void ProgressChanged(int percent, int qCount);
        private static void _accDB_ProgressChanged(int percent, int qcount)
        {
            if (DBPercentChanged != null)
                DBPercentChanged(percent, qcount);
        }


        public static event NoSQLDB.StateChange DBStateChanged;
        //public delegate void StateChange(DBStates state);

        //TODO if you see me wipe dis
        public static event EventHandler DBRenew;
        static void _accDB_StateChanged(DBStates state)
        {
            if (DBStateChanged != null)
                DBStateChanged(state);
            if (state != DBStates.Ready) return;
            if (DBRenew != null)
                DBRenew(null,null);
        }

        public static event NoSQLDB.RequestReady OnReadyRequest;
        static void AccDB_OnGetFinish(System.Collections.Generic.List<FLAccountDB.Metadata> meta)
        {
            if (OnReadyRequest != null)
                OnReadyRequest(meta);
        }

        #endregion

        public static void GetOnlineTable()
        {
            if (AccDB == null || HookTransport == null) return;

            if (HookTransport.IsSocketOpen())
                AccDB.GetMetasByNames(
                    HookTransport.GetPlayersOnline()
                        .Select(w => w.CharName)
                        .ToList()
                    );
        }



        public static bool IsHookAvailable()
        {
            return HookTransport != null && HookTransport.IsSocketOpen();
        }

        #region "Hook jobs"
        /// <summary>
        /// Connects Hook's transport.
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="port"></param>
        /// <param name="password"></param>
        public static void InitiateHook(string addr, int port, string password)
        {
            HookTransport = new Transport();
            HookTransport.OpenSocket(addr, port, password);
        }
        #endregion

    }
}
