﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab.Libary;
using Ladeskab.Libary.interfaces;
using NSubstitute;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;

namespace Ladeskab.Test
{
    [TestFixture]
    class TestStationControl
    {
        private StationControl _uut;
        private  IDoor _doorSource;
        private IChargeControl _chargeControlSource;
        private IRfidReader _RfidReader;
        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();


            _RfidReader = Substitute.For<IRfidReader>();
            _chargeControlSource = Substitute.For<IChargeControl>();
            _uut = new StationControl(_doorSource, _chargeControlSource, _RfidReader);
        }

        [Test]

        public void DoorEvent_DoorIsOpenTrue_DoorStateIsTrue()
        {
            //Arrange

            //Act
            _doorSource.DoorValueEvent += Raise.EventWith(new DoorValueEventArgs { DoorOpen = true });

            //Assert

            Assert.IsTrue(_uut.DoorState);


        }
        [Test]
        public void DoorEvent_DoorIsOpenFalse_DoorStateIsFalse()
        {
            //Arrange

            //Act
            _doorSource.DoorValueEvent += Raise.EventWith(new DoorValueEventArgs { DoorOpen = false });

            //Assert

            Assert.IsFalse(_uut.DoorState);


        }

        [Test]
        public void ChargerEvent_ChargeIsConnectedFalse_ChargerStateIsFalse()
        {
            //Arrange

            //Act
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(new ChargerConnectionValue { ChargerConnected = false });

            //Assert

            Assert.IsFalse(_uut.ChargerIsConnected);


        }

        [Test]
        public void ChargerEvent_ChargeIsConnectedTrue_ChargerStateIsTue()
        {
            //Arrange

            //Act
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(new ChargerConnectionValue { ChargerConnected = true });

            //Assert

            Assert.IsTrue(_uut.ChargerIsConnected);


        }
        [Test]
        public void RFidReaderEvent_LadeskabsStateAvailable_OldIDsetToEventID()

        {
            //Arrange
            _uut._state = StationControl.LadeskabState.Available;
            _uut.ChargerIsConnected = true;
            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert

            Assert.AreEqual(_uut._oldId, args.RFID);
        }

        [Test]
        public void RFidReaderEvent_LadeskabsDoorOpen_NothingisDone()

        {
            //Arrange
            _uut._state = StationControl.LadeskabState.DoorOpen;
            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert

            _doorSource.DidNotReceive().LockDoor();
            _doorSource.DidNotReceive().UnlockDoor();
            _chargeControlSource.DidNotReceive().StartCharge();
            _chargeControlSource.DidNotReceive().StopCharge();
            
        }





    }
}
