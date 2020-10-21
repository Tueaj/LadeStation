using System;

namespace Ladeskab
{
    public class FakeDoor : IDoor
    {
        public event EventHandler<DoorValueEventArgs> DoorValueEvent;

        public FakeDoor()
        {
            DoorOpen = false;
        }

        public bool DoorOpen { get; set; }

        public void OpenDoor()
        {
            DoorOpen = true;
            DoorValueEvent?.Invoke(this, new DoorValueEventArgs() { DoorOpen = this.DoorOpen });
        }

        public void CloseDoor()
        {

        }

        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }
    }
}