using System;
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

    }
}