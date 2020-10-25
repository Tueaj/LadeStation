using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab.Libary;
using Ladeskab.Libary.interfaces;

namespace Ladeskab.Test
{
    class TestStationControl
    {
        private StationControl _uut;
        [SetUp]
        public void Setup()
        {
            IChargeControl charger = new ChargeControl();
            IDoor door = new Door();
            _uut = new StationControl(door,charger);
        }
    }
}
