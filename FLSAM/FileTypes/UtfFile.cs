using System;
using System.Collections.Generic;
using System.IO;

namespace FLSAM.FileTypes
{

    /// <summary>
    /// Rather minimalistic UTF parser retrieving hardpoint info only. 
    /// Throw it at the files rather carefully as it copies the whole content into memory.
    /// </summary>
    class UtfFile
    {
        public List<string> Names = new List<string>();
        public List<string> Hardpoints = new List<string>();

        public UtfFile(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var buf = new byte[fs.Length];
            fs.Read(buf, 0, (int)fs.Length);
            fs.Close();

            var pos = 0;
            var sig = BitConverter.ToInt32(buf, pos); pos+=4;
            var ver = BitConverter.ToInt32(buf, pos); pos+=4;
	        if (sig != 0x20465455 || ver != 0x101)
                throw new Exception("Unsupported UTF version " + sig + ": " + filePath);

            // get node chunk info
            var nodeBlockOffset = BitConverter.ToInt32(buf, pos); pos+=4;
            //var nodeSize = BitConverter.ToInt32(buf, pos); 
            pos+=12;

            // get string chunk info
            var stringBlockOffset = BitConverter.ToInt32(buf, pos); 
            //pos+=4;
            //var stringBlockSize = BitConverter.ToInt32(buf, pos); 
            //pos+=8;

            // get data chunk info
            //var dataBlockOffset = BitConverter.ToInt32(buf, pos); 
            //pos+=4;
            //var dataBlockSize = buf.Length - dataBlockOffset;

            // Start at the root node.
            ParseNode(buf, nodeBlockOffset, 0, stringBlockOffset, "+");
        }

        void ParseNode(byte[] buf, int nodeBlockStart, int nodeStart, int stringBlockOffset, string depth)
        {
            var offset = nodeBlockStart + nodeStart;

            while (true)
            {
                //int nodeOffset = offset;

                var peerOffset = BitConverter.ToInt32(buf, offset); offset += 4;
                var nameOffset = BitConverter.ToInt32(buf, offset); offset += 4;
                var flags = BitConverter.ToInt32(buf, offset); offset += 4;
                //var zero = BitConverter.ToInt32(buf, offset); 
                offset += 4;
                var childOffset = BitConverter.ToInt32(buf, offset); 
                //offset += 4;
                //int allocated_size = BitConverter.ToInt32(buf, offset); 
                //offset += 4;
                //int size = BitConverter.ToInt32(buf, offset); 
                //offset += 4;
                //int size2 = BitConverter.ToInt32(buf, offset); 
                //offset += 4;
                //int u1 = BitConverter.ToInt32(buf, offset); 
                //offset += 4;
                //int u2 = BitConverter.ToInt32(buf, offset); 
                //offset += 4;
                //int u3 = BitConverter.ToInt32(buf, offset); 
                //offset += 4;

                // Extract the node name
                var len = 0;
                //for (int i = stringBlockOffset + nameOffset; i < buf.Length && buf[i] != 0; i++, len++) ;
                while (buf[len] != 0 && len < buf.Length) len++;
                var name = System.Text.Encoding.ASCII.GetString(buf, stringBlockOffset + nameOffset, len);

                if (depth.EndsWith("Hardpoints+Fixed+") || depth.EndsWith("Hardpoints+Revolute+"))
                {
                    Hardpoints.Add(name);
                }

                if (childOffset > 0 && flags==0x10)
                    ParseNode(buf, nodeBlockStart, childOffset, stringBlockOffset, depth + name + "+");

                if (peerOffset > 0)
                    ParseNode(buf, nodeBlockStart, peerOffset, stringBlockOffset, depth);

                //if ((flags & 0x80) == 0x80)
                    //break;

                break;
            }
        }

    }

}
