using Ladeskab.Libary;
using Ladeskab.Libary.interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test
{
    public class TestRfidReader
    {
        private IRfidReader _uut;
        private bool RfidEventRaised{ get; set; }

        private int IDFromEvent{ get; set; }

        [SetUp]
        public void Setup()
        {
            _uut = new FakeRfidReader();
            RfidEventRaised = false;
            _uut.RFIDDetectedEvent += (sender, args) =>
            {
                RfidEventRaised = true;
                IDFromEvent = args.RFID;
            };
        }

        [Test]
        public void RFIDDetectedAndEventRaised()
        {
            int TestId = 1234;
            _uut.ScanRFID(TestId);
            Assert.IsTrue(RfidEventRaised);
        }

        [Test] public void RFIDDetectedAndIdSendWithEvent()
        {
            int TestId = 1234;
            _uut.ScanRFID(TestId);
            Assert.That(TestId.Equals(IDFromEvent));
        }

    }
}