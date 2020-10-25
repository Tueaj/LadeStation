﻿using System;
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
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private bool DoorState = true;
        private bool ChargerIsConnected = false;
        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private IDoor _door;
        private int _oldId = 0;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor

       public StationControl ()
        {
            IDoor _door = new Door();
            IChargeControl _charger = new ChargeControl();

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
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
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

        
        
        public StationControl(IDoor door)
        {
            door.DoorValueEvent += HandleDoorChangeEvent;
        }

        private void HandleDoorChangeEvent(object sender, DoorValueEventArgs e)
        {
            DoorState = e.DoorOpen;
            setLadeskabState();
        }
        // Her mangler de andre trigger handlere

        public StationControl (IChargeControl charger)
        {
            charger.ChargerConnectionValueEvent += HandleChargerChangeEvent;
        }

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
