using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleLauncher.Net
{
    public sealed class PacketException : System.Exception
    {
        public PacketException(string message) : base(message) { }
    }
}
