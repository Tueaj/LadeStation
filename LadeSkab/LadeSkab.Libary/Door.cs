using System;

namespace Ladeskab
{
    public class Door : IDoor
    {
        public event EventHandler<DoorValueEventArgs> DoorValueEvent;

        public void LockDoor()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new System.NotImplementedException();
        }
    }
}