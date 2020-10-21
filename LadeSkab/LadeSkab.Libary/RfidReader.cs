using System;

namespace Ladeskab.Libary
{
    public class RFIDDetectedEventArgs : EventArgs
    {
        public int RFID { set; get; }
    }

    public class RfidReader
    {
        event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;



    }
}