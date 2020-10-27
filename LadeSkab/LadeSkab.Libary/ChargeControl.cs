using System;
using System.Dynamic;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class ChargeControl : IChargeControl
    {
        public ChargeControl(IUsbCharger _usbCharger)
        {
            UsbCharger = _usbCharger;
        }

        private IUsbCharger usbCharger;
        public IUsbCharger UsbCharger
        {
            get
            {
                return usbCharger;
            }
            set
            {
                usbCharger = value;
                usbCharger.CurrentValueEvent += HandleCurrentEvent;
            }
        }

        private void HandleCurrentEvent(object sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                IsConnected = false;
            }
            else if (0<e.Current && e.Current<=5)
            {
                IsConnected = true;
                Display.PrintUSBChargeDone();
            }
            else if (5 < e.Current && e.Current <= 500)
            {
                IsConnected = true;
                Display.PrintUSBIsCharging();
            }
            else if (500 < e.Current)
            {
                IsConnected = true;
                Display.PrintErrorRemovePhone();
            }

            ChargerConnectedChange();
        }

        private void ChargerConnectedChange()
        {
            ChargerConnectionValueEvent?.Invoke(this, new ChargerConnectionValue() { ChargerConnected = this.IsConnected });
        }

        public event EventHandler<ChargerConnectionValue> ChargerConnectionValueEvent;
        public IDisplay Display { get; set; }

        private bool isConnected;

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                isConnected = value;
                ChargerConnectedChange();
            }
        }


        public void StartCharge()
        {
            UsbCharger.StartCharge();
        }

        public void StopCharge()
        {
            UsbCharger.StopCharge();
        }
    }
}