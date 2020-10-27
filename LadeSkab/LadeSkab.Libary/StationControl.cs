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
        private IRfidReader _reader;
        private IDoor _door;
        private IDisplay _display;
        public int _oldId = 0;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IChargeControl charger, IRfidReader reader, IDisplay display)
        {
            _display = display;
            _door = door;
            _reader = reader;
            _door.DoorValueEvent += HandleDoorChangeEvent;
            _charger = charger;
            _charger.ChargerConnectionValueEvent += HandleChargerChangeEvent;
            _reader.RFIDDetectedEvent += RFidDetectedEvent;
        }

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

                        _display.PrintStationLockedUseID();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.PrintConnectionFail();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    _display.PrintDoorIsOpen();
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

                        _display.PrintTakePhoneCloseDoor();
                        _oldId = 0;
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.PrintWrongRFidTag();
                        _display.PrintStationOccupied();
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
        // Her mangler de andre trigger handlere --- Hvilke ?? - Tue

        private void HandleChargerChangeEvent(object sender, ChargerConnectionValue e)
        {

            ChargerIsConnected = e.ChargerConnected;
            setLadeskabState();
        }

        private void setLadeskabState()
        {
            //Fatter ikke helt det her, hvornår er skabet lukket men ikke låst ? - Tue
            if(_oldId == 0 && DoorState == false && ChargerIsConnected == false)
            {
                _state = LadeskabState.Available;
                _display.PrintConnectPhone();
                _display.PrintReadRFID();
            }
            else
            {
                _state = LadeskabState.DoorOpen;
            }
           
        }
    }
}
