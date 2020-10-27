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
            IDisplay display = new Display();
            IChargeControl chargeControl = new ChargeControl(usbCharger, display);
            IRfidReader riRfidReader = new FakeRfidReader();
            StationControl stationControl = new StationControl(door, chargeControl, riRfidReader, display);
            bool finish = false;
            do
            {
                System.Console.WriteLine("\n                                    Indtast E(xit), O(pen), C(Lose), R(eadKey), P(honeConnect), D(isconnectPhone): ");
                var input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.E:
                        finish = true;
                        break;

                    case ConsoleKey.O:
                        door.DoorOpen = true;
                        break;
                        
                    case ConsoleKey.C:
                        door.DoorOpen = false;
                        break;

                    case ConsoleKey.R:
                        System.Console.WriteLine("\n                                    Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        riRfidReader.ScanRFID(id);
                        break;
                    case ConsoleKey.P:
                        if ( stationControl.DoorState == true)
                            chargeControl.IsConnected = true;
                        else
                        {
                            System.Console.WriteLine("\n                                    Lågen er lukket, åben lågen før du tilslutter telefon");
                        }
                        break;
                    case ConsoleKey.D:
                        if (stationControl.DoorState == true)
                            chargeControl.IsConnected = false;
                        else
                        {
                            System.Console.WriteLine("\n                                    Lågen er lukket, åben lågen før du frakobler telefon");
                        }
                        break;

                    default:
                        break;
                }
            } while (!finish);
        }
    }
    }
