namespace Ladeskab.Libary.interfaces
{
    public interface IDisplay
    {
        public void PrintConnectPhone();
        public void PrintReadRFID();
        public void PrintConnectionFail();
        public void PrintStationOccupied();

        public void PrintUSBChargeDone();
    }
}