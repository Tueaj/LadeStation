using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab.Libary;

namespace Ladeskab.Test
{
    class TestChargeControl
    {
        private ChargeControl _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new ChargeControl();
        }
    }
}
