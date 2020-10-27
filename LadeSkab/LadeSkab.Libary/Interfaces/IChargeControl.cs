using System;

namespace Ladeskab.Libary.interfaces
{
    public class ChargerConnectionValue : EventArgs
    {
        public bool ChargerConnected { set; get; }
    }
    public interface IChargeControl
    {
        event EventHandler<ChargerConnectionValue> ChargerConnectionValueEvent;
        public IDisplay Display { get; set; }
        public void StartCharge();
        public void StopCharge();
        public bool IsConnected { get; set; }
    }
}