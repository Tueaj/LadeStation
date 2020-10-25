using System;

namespace Ladeskab.Libary.interfaces
{
    public class DoorValueEventArgs : EventArgs
    {
        public bool DoorOpen { set; get; }
    }
    public interface IDoor
    {
        event EventHandler<DoorValueEventArgs> DoorValueEvent;
        public bool DoorOpen { get; set; }
        public bool DoorLocked { get; set; }
        public void LockDoor();
        public void UnlockDoor();
    }
}