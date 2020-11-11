using NUnit.Framework;
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
        private IDisplay _display;
        private ILogFile _logFile;

        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();

            _display = Substitute.For<IDisplay>();
            _RfidReader = Substitute.For<IRfidReader>();
            _chargeControlSource = Substitute.For<IChargeControl>();
            _logFile = Substitute.For<ILogFile>();
            _uut = new StationControl(_doorSource, _chargeControlSource, _RfidReader, _display, _logFile);
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
            DoorValueEventArgs args0 = new DoorValueEventArgs { DoorOpen = false};
            _doorSource.DoorValueEvent += Raise.EventWith(args0);
          
            _uut.ChargerIsConnected = true;
            RFIDDetectedEventArgs args1 = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args1);

            //Assert

            Assert.AreEqual(_uut._oldId, args1.RFID);
        }

        [Test]
        public void RFidReaderEvent_LadeskabsDoorOpen_DoorRecivedNoCalls()
        {
            //Arrange
            DoorValueEventArgs args0 = new DoorValueEventArgs { DoorOpen = true };
            _doorSource.DoorValueEvent += Raise.EventWith(args0);

            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That((_doorSource.ReceivedCalls().Count()),Is.EqualTo(1));
        }

        [Test]
        public void RFidReaderEvent_LadeskabsDoorOpen_ChargeControlRecivedNoCalls()
        {
            //Arrange
            DoorValueEventArgs args0 = new DoorValueEventArgs { DoorOpen = true };
            _doorSource.DoorValueEvent += Raise.EventWith(args0);

            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs {RFID = 12345};

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That((_chargeControlSource.ReceivedCalls().Count()), Is.EqualTo(1));
        }

        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedWrongID_WrongRFidTagDisplayMethodCalled()
        {
            //Arrange

            ChargerConnectionValue args0 = new ChargerConnectionValue {ChargerConnected = true};
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 54321 };

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);


            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
           
            _display.Received().PrintWrongRFidTag();
           
        }
        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedWrongID_ChargeControlRecivedNoCallsButArrangeAndContruct()
        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = true };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 54321 };
            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);


            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };
            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.That((_chargeControlSource.ReceivedCalls().Count()), Is.EqualTo(2));
        }

        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedRightID_StopChargeCalled()
        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = true };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 12345 };
            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);

            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };
            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            _chargeControlSource.Received().StopCharge();
        }
        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedRightID_UnlockDoorCalled()
        {
            //Arrange
           // _uut._state = StationControl.LadeskabState.Locked;
            _uut._oldId = 12345;
            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };
            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            _doorSource.Received().UnlockDoor();
        }
        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedRightID_oldIdIsZero()
        {
            //Arrange
          //  _uut._state = StationControl.LadeskabState.Locked;
            _uut._oldId = 12345;
            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };
            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            Assert.Zero(_uut._oldId);
        }
        [Test]
        public void RFidReaderEvent_LadeskabsStateAvailableChargerConnectedFalse()

        {
            //Arrange
          //  _uut._state = StationControl.LadeskabState.Available;
            _uut.ChargerIsConnected = false;
            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert

            _display.Received().PrintConnectionFail();
        }
        
    }
}
