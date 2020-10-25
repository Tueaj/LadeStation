using System;

namespace Ladeskab
{
    public class RFIDDetectedEventArgs : EventArgs
    {
        public int RFID { set; get; }
    }
    public interface IRfidReader
    {
        event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

        public void ScanRFID(int id);
    }
}