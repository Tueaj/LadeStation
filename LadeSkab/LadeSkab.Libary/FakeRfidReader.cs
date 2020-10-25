using System;

namespace Ladeskab
{
    public class FakeRfidReader : IRfidReader
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

        public void ScanRFID(int id)
        {
            RFIDDetected(id);
        }

        private void RFIDDetected(int id)
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs() { RFID = id });
        }
    }
}