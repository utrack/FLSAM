using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace FLHookTransport
{
    class Socket
    {
        private readonly NetworkStream _stream;
        private readonly StreamReader _sreader;

        public event EventLine UnexpectedMessage;
        public delegate void EventLine(string message);
        private readonly TcpClient _client;
        /// <summary>
        /// Initiate and open socket and reader.
        /// </summary>
        /// <param name="addr">Hook hostname or IP.</param>
        /// <param name="port">Hook socket's port.</param>
        /// <param name="login">Password.</param>
        public Socket(string addr, int port, string login)
        {
            
            try
            {
                _client = new TcpClient(addr, port);
                _stream = _client.GetStream();
            }
            catch (Exception e)
            {
                throw new Exception("Cannot connect to FLHook, " + e.Message);
            }

            _client.ReceiveTimeout = 5000;
            _stream.ReadTimeout = 250;
            _sreader = new StreamReader(_stream);

            // Wait for the welcome message.
            var reply = GetMessage();
            if (reply != "Welcome to FLHack, please authenticate")
                throw new Exception("no login message '" + reply + "'");

            // Send the pass and wait for OK
            SendCommand(String.Format("pass {0}", login));
            reply = GetMessage();
            if (reply != "OK")
                throw new Exception("no pass ok message '" + reply + "'");


        }

        public bool IsAlive()
        {
            return (_client.Connected && (_sreader != null) && (_stream.CanRead));
        }

        /// <summary>
        /// Send a command without waiting for a response.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>True if succeeds, otherwise false.</returns>
        public bool SendCommand(string command)
        {
            // _sreader.ReadToEnd(); //navigate to end of a stream
            if (_stream == null)
                return false;
            //if (unicode)
            //    txBuf = Encoding.Unicode.GetBytes(command + "\n");
            //else
            var txBuf = Encoding.ASCII.GetBytes(command + "\n");
            try
            {
                _stream.Write(txBuf, 0, txBuf.Length);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

            //return GetMessage() == "OK";
        }

        /// <summary>
        /// Send a command and wait for "OK".
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>True if succeeds, otherwise false.</returns>
        public bool SendScalar(string command)
        {
            SendCommand(command);
            return GetMessage() == "OK";
        }

        public string SendReader(string command)
        {
            SendCommand(command);
            var ret = GetMessage();
            if (ret.StartsWith("ERR")) return null;
            GetMessage(); // "OK"
            return ret;
        }

        public string GetMessage()
        {
            if (!_stream.DataAvailable) return null;
            string str;
            try
            {
                str = _sreader.ReadLine();
            }
            catch (Exception)
            {
                return null;
            }
            return str;
        }

    }
}
