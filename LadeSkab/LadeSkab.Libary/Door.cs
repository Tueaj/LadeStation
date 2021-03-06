﻿using System;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Libary
{
    public class Door : IDoor
    {
        public event EventHandler<DoorValueEventArgs> DoorValueEvent;
        public bool DoorOpen { get; set; }
        public bool DoorLocked { get; set; }

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