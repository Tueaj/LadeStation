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

namespace Ladeskab.Test
{
    [TestFixture]
    class TestStationControl
    {
        private StationControl _uut;
        private readonly TestDoorSource _doorSource;
        private readonly TestChargerSource _chargeControlSource;
        [SetUp]
        public void Setup()
        {
            _doorSource = Substitute.For<IDoor>();
            _chargeControlSource = Substitute.For<IChargeControl>();
            _uut = new StationControl(_doorSource,_chargeControlSource);
        }



        
    }
}
