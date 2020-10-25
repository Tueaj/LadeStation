using System;
using System.IO;
using Ladeskab.Libary;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestLogFile
    {
        private LogFile _uut;
        private StringWriter SW;
        
        [SetUp]
        public void setup()
        {
            SW = new StringWriter();
            _uut = new LogFile(SW);
            Console.SetOut(SW);
            _uut.DT = DateTime.MinValue;
        }

        [Test]
        public void LogDoorLocked_IsPrintedCorrect()
        {
            _uut.LogDoorLocked(123);
            string stringRes = "01-01-0001 00:00:00: Time for door locked with RFid: 123\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));

        }

        [Test]
        public void LogDoorUnlocked_IsPrintedCorrect()
        {
            _uut.LogDoorUnlocked(123);
            string stringRes = "01-01-0001 00:00:00: Time for door Unlocked with RFid: 123\r\n";
            Assert.That(stringRes, Is.EqualTo(SW.ToString()));

        }
    }
}