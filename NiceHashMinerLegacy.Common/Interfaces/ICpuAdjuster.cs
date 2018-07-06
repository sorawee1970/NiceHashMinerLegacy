using System;
using System.Collections.Generic;
using System.Text;

namespace NiceHashMinerLegacy.Common.Interfaces
{
    public interface ICpuAdjuster
    {
        void AdjustAffinity(int id, ulong mask);
    }
}
