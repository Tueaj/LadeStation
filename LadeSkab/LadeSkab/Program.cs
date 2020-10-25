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
            IChargeControl chargeControl = new ChargeControl();
            IDisplay display = new Display();
            IRfidReader riRfidReader = new FakeRfidReader();
            StationControl stationControl = new StationControl(door, chargeControl, riRfidReader, display);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.DoorOpen = true;
                        break;
                        
                    case 'C':
                        door.DoorOpen = false;
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        riRfidReader.ScanRFID(id);
                        break;

                    default:
                        break;
                }
            } while (!finish);
        }
    }
    }
