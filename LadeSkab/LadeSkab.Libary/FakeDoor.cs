using System;

namespace Ladeskab
{
    public class FakeDoor : IDoor
    {
        public event EventHandler<DoorValueEventArgs> DoorValueEvent;

        public FakeDoor()
        {
            DoorOpen = false;
            DoorLocked = false;
        }

        public bool DoorOpen { get; set; }
        public bool DoorLocked { get; set; }

        public void OpenDoor()
        {
            if (DoorLocked == false)
            {
                DoorOpen = true;
                DoorValueEvent?.Invoke(this, new DoorValueEventArgs() {DoorOpen = this.DoorOpen});
            }
            else
            {
                Console.WriteLine("Message from door: Door is locked, and cant be opened");
            }
        }

        public void CloseDoor()
        {
            DoorOpen = false;
            DoorValueEvent?.Invoke(this, new DoorValueEventArgs() { DoorOpen = this.DoorOpen });
        }

        public void LockDoor()
        {
            DoorLocked = true;
            Console.WriteLine("Message from door: Door is now LOCKED");
        }

        public void UnlockDoor()
        {
            DoorLocked = false;
            Console.WriteLine("Message from door: Door is now UNLOCKED");
        }
    }
}