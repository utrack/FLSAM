using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FLAccountDB;
using FLAccountDB.Data;
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
        public static void InitiateDB(decimal dbType, string path1, string path2,LogDispatcher.LogDispatcher log)
        {
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
            if (!IsDBAvailable()) return;
            AccDB.Scan.LoadDB(aggro);
            Properties.Settings.Default.LastDBUpdate = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Updates the DB.
        /// </summary>
        public static void UpdateDB()
        {
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
        static void AccDB_OnGetFinish(System.Collections.Generic.List<Metadata> meta)
        {
            if (OnReadyRequest != null)
                OnReadyRequest(meta);
        }

        #endregion

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
        /// <param name="log"></param>
        public static void InitiateHook(string addr, int port, string password,LogDispatcher.LogDispatcher log)
        {
            HookTransport = new Transport(log);
            HookTransport.OpenSocket(addr, port, password);
        }
        #endregion

    }
}
