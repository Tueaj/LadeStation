﻿using System;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class FakeDoor : IDoor
    {
        public event EventHandler<DoorValueEventArgs> DoorValueEvent;

        public FakeDoor()
        {
            DoorOpen = false;
            DoorLocked = false;
        }

        private bool doorOpen;
        public bool DoorOpen
        {
            get => doorOpen;
            set
            {
                if (DoorLocked == false)
                {
                    doorOpen = value;
                    DoorValueChanged();
                }
                else
                {
                    Console.WriteLine("Message from door: Door is locked, and cant be opened");
                }
            }
        }

        public bool DoorLocked { get; set; }

        private void DoorValueChanged()
        {
            DoorValueEvent?.Invoke(this, new DoorValueEventArgs() {DoorOpen = this.DoorOpen});
        }

        public void OpenDoor()
        {
            if (DoorLocked == false)
            {
                DoorOpen = true;
                DoorValueChanged();
            }
            else
            {
                Console.WriteLine("Message from door: Door is locked, and cant be opened");
            }
        }

        public void CloseDoor()
        {
            DoorOpen = false;
            DoorValueChanged();
        }

        public void LockDoor()
        {
            if (DoorOpen == false)
            {
                DoorLocked = true;
                Console.WriteLine("Message from door: Door is now LOCKED");
            }
            else
            {
                Console.WriteLine("Message from door: Door can't be locked while it's not closed");
            }
            
            
        }

        public void UnlockDoor()
        {
            DoorLocked = false;
            Console.WriteLine("Message from door: Door is now UNLOCKED");
        }
    }
}