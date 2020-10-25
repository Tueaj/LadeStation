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
        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();



            _chargeControlSource = Substitute.For<IChargeControl>();
            _uut = new StationControl(_doorSource, _chargeControlSource);
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






    }
}
