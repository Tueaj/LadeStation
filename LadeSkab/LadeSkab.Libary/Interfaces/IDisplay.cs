namespace Ladeskab.Libary.interfaces
{
    public interface IDisplay
    {
        public void PrintConnectPhone();
        public void PrintReadRFID();
        public void PrintConnectionFail();
        public void PrintStationOccupied();
        public void PrintUSBChargeDone();
        public void PrintUSBIsCharging();
        public void PrintErrorRemovePhone();

        public void PrintStationLockedUseID();

        public void PrintDoorIsOpen();

        public void PrintTakePhoneCloseDoor();
        public void PrintWrongRFidTag();
    }
}