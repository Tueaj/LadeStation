using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        public bool DoorState = true;
        public bool ChargerIsConnected = false;
        public LadeskabState _state;
        private IChargeControl _charger;
        private IDoor _door;
        public int _oldId = 0;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IChargeControl charger)
        {
            _door = door;
            _door.DoorValueEvent += HandleDoorChangeEvent;
            _charger = charger;
            _charger.ChargerConnectionValueEvent += HandleChargerChangeEvent;
        }
       

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (ChargerIsConnected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", _oldId);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _oldId = 0;
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        
        
       private void  RFidDetectedEvent(object sender, RFIDDetectedEventArgs e)
        {

           
            RfidDetected(e.RFID);
        }

        private void HandleDoorChangeEvent(object sender, DoorValueEventArgs e)
        {
            DoorState = e.DoorOpen;
            setLadeskabState();
        }
        // Her mangler de andre trigger handlere

        private void HandleChargerChangeEvent(object sender, ChargerConnectionValue e)
        {

            ChargerIsConnected = e.ChargerConnected;
            setLadeskabState();
        }

        private void setLadeskabState()
        {

            if( DoorState)
            {
                _state = LadeskabState.DoorOpen;
            }
            else if(_oldId == 0 && DoorState == false && ChargerIsConnected == true)
            {
                _state = LadeskabState.Available;
            }
            else
            {
                _state = LadeskabState.DoorOpen;
            }
           
        }
    }
}
