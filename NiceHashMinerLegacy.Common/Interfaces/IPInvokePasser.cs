namespace NiceHashMinerLegacy.Common.Interfaces
{
    public interface IPInvokePasser
    {
        bool IsConnectedToInternet();
        void PreventSleep();
        void AllowMonitorPowerdownAndSleep();
    }
}
