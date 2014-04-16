using System;
using System.Collections.Generic;
using System.Globalization;
using LogDispatcher;

namespace FLHookTransport
{
    public class Transport
    {

        private readonly LogDispatcher.LogDispatcher _log;
        private Socket _socket;

        public Transport(LogDispatcher.LogDispatcher log)
        {
            _log = log;
        }

        public bool OpenSocket(string addr, int port, string password)
        {
            if (port == 0) return false;
            try
            {
                _socket = new Socket(addr, port, password);
            }
            catch (Exception ex)
            {
                _log.NewMessage(LogType.Error, "Can't connect to hook: " + ex.Message);
                return false;
            }

            return true;
        }

        public bool IsSocketOpen()
        {
            return _socket != null && _socket.IsAlive();
        }

        /// <summary>
        /// Tries to get the current playerlist.
        /// </summary>
        /// <returns>List of players if succeeds, null otherwise.</returns>
        public List<PlayerInfo> GetPlayersOnline()
        {
            if (_socket == null) return null;

            if (!_socket.SendCommand("getplayers"))
            {
                _log.NewMessage(LogType.Warning, "Can't get online playerlist! Socket down");
                return null;
            }

            var ret = new List<PlayerInfo>();
            string tString;

            while ((tString = _socket.GetMessage()) != "OK")
            {
                if (tString.StartsWith("ERR")) return null;
                var p = new PlayerInfo();
                // charname=? clientid=? loss=? lag=? sat=? ping_fluct=?
                string[] keys;
                string[] vals;
                ParseLine(tString,out keys, out vals);
                p.CharName = vals[0];
                p.ID = Convert.ToInt32(vals[1]);
                p.IP = vals[2];
                p.Ping = Convert.ToInt32(vals[4]);
                p.System = vals[6];
                ret.Add(p);
            }
            return ret;
        }

        public bool IsOnServer(string name)
        {
            if (!IsSocketOpen()) return false;
            var ret = _socket.SendReader(String.Format("isonserver {0}", name));
            if (ret != null)
                return ret.Substring(ret.IndexOf('=')) == "yes";
            return false;
        }

        #region "kicks bans"
        public bool KickPlayer(string name,string reason = "")
        {
            return _socket.SendScalar(String.Format("kick {0} {1}", name,reason));
        }

        public bool KickBan(string name, string reason = "")
        {
            return _socket.SendScalar(String.Format("kickban {0} {1}", name, reason));
        }

        public bool BanPlayer(string name)
        {
            return _socket.SendScalar(String.Format("ban {0}", name));
        }

        public bool UnbanPlayer(string name)
        {
            return _socket.SendScalar(String.Format("unban {0}", name));
        }
        #endregion

        #region "cash manipulation"
        /// <summary>
        /// Get player's current cash.
        /// </summary>
        /// <param name="name">Player name</param>
        /// <returns>Null if failed.</returns>
        public string GetCash(string name)
        {
            var ret = _socket.SendReader(String.Format("getcash {0}", name));
            if (ret == null) return null;
            return ret.Substring(ret.IndexOf('=')+1);
        }

        /// <summary>
        /// Add to player's current cash.
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="amount"></param>
        /// <returns>Null if failed, otherwise new cash value.</returns>
        public string AddCash(string name, uint amount)
        {
            var ret = _socket.SendReader(String.Format("addcash {0} {1}", name, amount));
            if (ret == null) return null;
            return ret.Substring(ret.IndexOf('=') + 1);
        }

        /// <summary>
        /// Set player's current cash.
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="amount"></param>
        /// <returns>Null if failed, otherwise new cash value.</returns>
        public string SetCash(string name, uint amount)
        {
            var ret = _socket.SendReader(String.Format("setcash {0} {1}", name, amount));
            if (ret == null) return null;
            return ret.Substring(ret.IndexOf('=') + 1);
        }
        #endregion

        public bool SetRep(string name, string faction, float value)
        {
            return _socket.SendScalar(String.Format(new NumberFormatInfo 
            { NumberDecimalSeparator = "."}
            , "setrep {0} {1} {2:0.00}", 
            name,faction,value));
        }

        public bool KillPlayer(string name)
        {
            return _socket.SendScalar(String.Format("kill {0}", name));
        }

        public bool BeamPlayer(string name, string baseName)
        {
            return _socket.SendScalar(String.Format("beam {0} {1}", name, baseName));
        }





        //TODO: git fasta.
        static private void ParseLine(string line, out string[] keys, out string[] values)
        {
            // example: charname=? clientid=? loss=? lag=? sat=? ping_fluct=?
            var items = line.Split(new[] { ' ' });
            keys = new string[items.Length];
            values = new string[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var valueStartIndex = items[i].IndexOf('=');
                if (valueStartIndex >= 0)
                {
                    keys[i] = items[i].Substring(0, valueStartIndex);
                    values[i] = items[i].Substring(valueStartIndex + 1);
                }
                else
                {
                    keys[i] = items[i];
                    values[i] = "";
                }
            }
        }

    }
}
