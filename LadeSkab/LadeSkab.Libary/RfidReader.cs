using System;

namespace Ladeskab
{

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

    }
}