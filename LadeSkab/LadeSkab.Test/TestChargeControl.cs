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

namespace Ladeskab.Test
{
    class TestChargeControl
    {
        private ChargeControl _uut;

        private IUsbCharger usbCharger;

        private IDisplay display;
        private bool ChargerConnectionValueEventRaised { get; set; }

        [SetUp]
        public void Setup()
        {
            usbCharger = Substitute.For<IUsbCharger>();
            display = Substitute.For<IDisplay>();

            _uut = new ChargeControl(usbCharger,display);
            _uut.ChargerConnectionValueEvent += (sender, args) =>
            {
                ChargerConnectionValueEventRaised = true;
            };
        }
        [Test]
        public void TestStartChargeIsCalled()
        {
            _uut.StartCharge();
            usbCharger.Received().StartCharge();
        }

        [Test]
        public void TestStopChargeIsCalled()
        {
            _uut.StopCharge();
            usbCharger.Received().StopCharge();
        }
        [Test]
        public void TestUsbChargerCurrentIsMinus1AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = -1 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
        
        [Test]
        public void TestUsbChargerCurrentIs0AndIsConnectedIsFalse()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = 0});

            Assert.False(_uut.IsConnected);
        }
        [Test]
        public void TestUsbChargerCurrentIs0AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
        [Test]
        public void TestUsbChargerCurrentIs1AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 1 });

            Assert.True(_uut.IsConnected);
        }
        [Test]
        public void TestUsbChargerCurrentIs1AndDisplayPrintUsbChargeDoneIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 1 });

            display.Received().PrintUSBChargeDone();
        }
        [Test]
        public void TestUsbChargerCurrentIs1AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 1 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
        [Test]
        public void TestUsbChargerCurrentIs5AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 5 });

            Assert.True(_uut.IsConnected);
        }
        [Test]
        public void TestUsbChargerCurrentIs5AndDisplayPrintUsbChargeDoneIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 5 });

            display.Received().PrintUSBChargeDone();
        }
        [Test]
        public void TestUsbChargerCurrentIs5AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 5 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
        [Test]
        public void TestUsbChargerCurrentIs6AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 6 });

            Assert.True(_uut.IsConnected);
        }
        [Test]
        public void TestUsbChargerCurrentIs6AndDisplayPrintUSBIsChargingIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 6 });

            display.Received().PrintUSBIsCharging();
        }
        [Test]
        public void TestUsbChargerCurrentIs6AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 6 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
        [Test]
        public void TestUsbChargerCurrentIs500AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 500 });

            Assert.True(_uut.IsConnected);
        }
        [Test]
        public void TestUsbChargerCurrentIs500AndDisplayPrintUSBIsChargingIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 500 });

            display.Received().PrintUSBIsCharging();
        }
        [Test]
        public void TestUsbChargerCurrentIs500AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 500 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
        [Test]
        public void TestUsbChargerCurrentIs501AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });

            Assert.True(_uut.IsConnected);
        }
        [Test]
        public void TestUsbChargerCurrentIs501AndDisplayPrintUSBIsChargingIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });

            display.Received().PrintErrorRemovePhone();
        }
        [Test]
        public void TestUsbChargerCurrentIs501AndEventRaised()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });

            Assert.IsTrue(ChargerConnectionValueEventRaised);
        }
    };
}
