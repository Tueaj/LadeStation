﻿using System;
using System.IO;
using Ladeskab.Libary;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestDisplay
    {
        private Display _uut;
        private StringWriter SW;

        [SetUp]
        public void setup()
        {
            SW = new StringWriter();
            _uut = new Display();
            Console.SetOut(SW);
        }

        [Test]
        public void PrintConnectPhone_IsPrinted()
        {
            _uut.PrintConnectPhone();
            string stringRes = "Charger is ready\r\nConnect Phone to Charger\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintReadRFID_IsPrinted()
        {
            _uut.PrintReadRFID();
            string stringRes = "Scan your RFID key\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintConnectionFail_IsPrinted()
        {
            _uut.PrintConnectionFail();
            string stringRes = "Connection failed, unplug phone\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintStationOccupied_IsPrinted()
        {
            _uut.PrintStationOccupied();
            string stringRes = "Charger is Occupied\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintPrintUSBChargeDone_IsPrinted()
        {
            _uut.PrintUSBChargeDone();
            string stringRes = "Charging is done\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintUSBIsCharging_IsPrinted()
        {
            _uut.PrintUSBIsCharging();
            string stringRes = "Phone is now charging\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintErrorRemovePhone_IsPrinted()
        {
            _uut.PrintErrorRemovePhone();
            string stringRes = "An Error has occurred please remove phone\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintStationLockedUseID_IsPrinted()
        {
            _uut.PrintStationLockedUseID();
            string stringRes = "Station locked, use RFid to unlock\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintDoorIsOpen_IsPrinted()
        {
            _uut.PrintDoorIsOpen();
            string stringRes = "Door is open\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintTakePhoneCloseDoor_IsPrinted()
        {
            _uut.PrintTakePhoneCloseDoor();
            string stringRes = "Take phone, close door\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }

        [Test]
        public void PrintWrongRFidTag_IsPrinted()
        {
            _uut.PrintWrongRFidTag();
            string stringRes = "wrong RFid\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));
        }


    }
}