using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NiceHashMinerLegacy.Common.Interfaces
{
    public interface INHProcess
    {
        int Id { get; }
        ProcessStartInfo StartInfo { get; }
        Action ExitEvent { set; }

        void Kill();
        void Close();
        bool Start();
    }
}
