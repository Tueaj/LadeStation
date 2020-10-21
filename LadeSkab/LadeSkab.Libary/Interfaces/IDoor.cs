using System;

namespace Ladeskab
{
    public class DoorValueEventArgs : EventArgs
    {
        public bool DoorOpen { set; get; }
    }
    public interface IDoor
    {
        event EventHandler<DoorValueEventArgs> DoorValueEvent;

        public void LockDoor();
        public void UnlockDoor();
    }
}