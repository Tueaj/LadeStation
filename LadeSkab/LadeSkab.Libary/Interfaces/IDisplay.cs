namespace Ladeskab
{
    public interface IDisplay
    {
        public void PrintConnectPhone();
        public void PrintReadRFID();
        public void PrintConnectionFail();
        public void PrintStationOccupied();
    }
}