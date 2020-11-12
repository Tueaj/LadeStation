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
        public void DoorEvent_DoorIsOpenTrue_PrintConnectPhoneCalledOnDisplay()
        {
            //Arrange

            //Act
            _doorSource.DoorValueEvent += Raise.EventWith(new DoorValueEventArgs { DoorOpen = true });

            //Assert

            _display.Received().PrintConnectPhone();


        }
        [Test]
        public void DoorEvent_DoorIsOpenFalse_PrintReadRFIDCalledOnDisplay()
        {
            //Arrange

            //Act
            _doorSource.DoorValueEvent += Raise.EventWith(new DoorValueEventArgs { DoorOpen = false });

            //Assert

            _display.Received().PrintReadRFID();


        }

        [Test]
        public void ChargerEvent_ChargeIsConnectedFalse_PrintConnectionFailCalledOnDisplay()
        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = false };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 54321 };

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);

            //Act            

            //Assert            
            _display.Received().PrintConnectionFail();
        }



        [Test]
        public void ChargerEvent_ChargeIsConnectedTrue_StartChargeCalledOnChargeControl()
        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = true };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 54321 };

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);

            //Act

            //Assert

            _chargeControlSource.Received().StartCharge();
        }

        [Test]
        public void ChargerEvent_ChargeIsConnectedTrue_LockDoorCalledOnDoor()
        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = true };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 54321 };

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);

            //Act

            //Assert
            _doorSource.Received().LockDoor();
        }

        [Test]
        public void ChargerEvent_ChargeIsConnectedTrue_LogDoorLockedCalledOnLogfile()
        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = true };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 54321 };

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);

            //Act

            //Assert
            _logFile.ReceivedWithAnyArgs().LogDoorLocked(default);
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
        public void LadeskabsDoorOpen_DoorOpenDisplayMethodCalled()
        {
            //Arrange
            DoorValueEventArgs args0 = new DoorValueEventArgs { DoorOpen = true };
            _doorSource.DoorValueEvent += Raise.EventWith(args0);

            RFIDDetectedEventArgs args = new RFIDDetectedEventArgs { RFID = 12345 };

            //Act

            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args);

            //Assert
            _display.Received().PrintDoorIsOpen();
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
        public void RFidReaderEvent_LadeskabsStateLockedWrongID_PrintStationOccupiedDisplayMethodCalled()
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

            _display.Received().PrintStationOccupied();

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
        public void RFidReaderEvent_LadeskabsStateLockedRightID_LogDoorUnlockedCalledOnLogfile()
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
            _logFile.ReceivedWithAnyArgs().LogDoorUnlocked(default);
        }
        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedRightID_PrintTakePhoneCloseDoorCalledOnDoor()
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
            _display.Received().PrintTakePhoneCloseDoor();
        }

        [Test]
        public void RFidReaderEvent_LadeskabsStateLockedRightID_UnlockDoorCalled()
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
            _doorSource.Received().UnlockDoor();
        }
       
        [Test]
        public void RFidReaderEvent_LadeskabsStateAvailableChargerConnectedFalse()

        {
            //Arrange
            ChargerConnectionValue args0 = new ChargerConnectionValue { ChargerConnected = false };
            _chargeControlSource.ChargerConnectionValueEvent += Raise.EventWith(args0);

            DoorValueEventArgs args1 = new DoorValueEventArgs { DoorOpen = false };
            _doorSource.DoorValueEvent += Raise.EventWith(args1);

            RFIDDetectedEventArgs args2 = new RFIDDetectedEventArgs { RFID = 12345 };
            _RfidReader.RFIDDetectedEvent += Raise.EventWith(args2);
                   

            //Act
         

            //Assert

            _display.Received().PrintConnectionFail();
        }
        
    }
}
