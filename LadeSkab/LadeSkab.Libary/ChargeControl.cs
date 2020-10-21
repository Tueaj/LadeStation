using System;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger UsbCharger
        {
            get
            {
                return UsbCharger;
            }
            set
            {
                UsbCharger = value;
                UsbCharger.CurrentValueEvent += HandleCurrentEvent;
            }
        }

        private void HandleCurrentEvent(object sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                isConnected = false;
            }
            else if (0<e.Current && e.Current<=5)
            {
                isConnected = true;

            }
            else if (5 < e.Current && e.Current <= 500)
            {
                isConnected = true;
            }
            else if (500 < e.Current)
            {
                isConnected = true;
            }

            ChargerConnectedChange();
        }

        private void ChargerConnectedChange()
        {
            ChargerConnectionValueEvent?.Invoke(this, new ChargerConnectionValue() { ChargerConnected = this.isConnected });
        }

        public event EventHandler<ChargerConnectionValue> ChargerConnectionValueEvent;
        public IDisplay Display { get; set; }
        
        private bool isConnected { get; set; }

        public bool IsConnected()
        {
            return isConnected;
        }

        public void StartCharge()
        {
            throw new System.NotImplementedException();
        }

        public void StopCharge()
        {
            throw new System.NotImplementedException();
        }
    }
}