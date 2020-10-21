using System;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class Display : IDisplay
    {
        public void PrintConnectPhone()
        {
            Console.WriteLine("Charger is ready");
            Console.WriteLine("Connect Phone to Charger");
        }

        public void PrintReadRFID()
        {
            Console.WriteLine("Scan your RFID key");
        }

        public void PrintConnectionFail()
        {
            Console.WriteLine("Connection failed, unplug phone");
        }

        public void PrintStationOccupied()
        {
            Console.WriteLine("Charger is Occupied");
        }
    }
}