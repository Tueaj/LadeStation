    using System;
    using Ladeskab.Libary.interfaces;
    using Ladeskab.Libary;

    namespace Ladeskab 
    {
    
    class Program
    {
        static void Main(string[] args)
        {
            IDoor door = new FakeDoor();
            IUsbCharger usbCharger = new UsbChargerSimulator();
            IChargeControl chargeControl = new ChargeControl(usbCharger);
            IDisplay display = new Display();
            IRfidReader riRfidReader = new FakeRfidReader();
            StationControl stationControl = new StationControl(door, chargeControl, riRfidReader, display);
            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast Exit, Open, CLose, ReadKey, PhoneConnect, PhoneDisconnect: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input)
                {
                    case "Exit":
                        finish = true;
                        break;

                    case "Open":
                        door.DoorOpen = true;
                        break;
                        
                    case "Close":
                        door.DoorOpen = false;
                        break;

                    case "ReadKey":
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        riRfidReader.ScanRFID(id);
                        break;
                    case "PhoneConnect":
                        chargeControl.IsConnected = true;
                        break;
                    case "PhoneDisconnect":
                        chargeControl.IsConnected = false;
                        break;

                    default:
                        break;
                }
            } while (!finish);
        }
    }
    }
