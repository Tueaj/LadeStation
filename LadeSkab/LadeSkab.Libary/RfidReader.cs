using System;

namespace Ladeskab.Libary
{

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public void ScanRFID(int id)
        {
            throw new NotImplementedException();
        }
    }
}