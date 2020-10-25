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

        [SetUp]
        public void Setup()
        {
            usbCharger = Substitute.For<IUsbCharger>();
            display = Substitute.For<IDisplay>();

            _uut = new ChargeControl();
            _uut.UsbCharger = usbCharger;
            _uut.Display = display;

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
        public void TestUsbChargerCurrentIs0AndIsConnectedIsFalse()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = 0});

            Assert.False(_uut.IsConnected());
        }
        [Test]
        public void TestUsbChargerCurrentIs1AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 1 });

            Assert.True(_uut.IsConnected());
        }
        [Test]
        public void TestUsbChargerCurrentIs1AndDisplayPrintUsbChargeDoneIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 1 });

            display.Received().PrintUSBChargeDone();
        }
        [Test]
        public void TestUsbChargerCurrentIs5AndIsConnectedIsTrue()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 5 });

            Assert.True(_uut.IsConnected());
        }
        [Test]
        public void TestUsbChargerCurrentIs5AndDisplayPrintUsbChargeDoneIsCalled()
        {
            usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 5 });

            display.Received().PrintUSBChargeDone();
        }


    };
}
