using Ladeskab.Libary;
using Ladeskab.Libary.interfaces;
using NUnit.Framework;

namespace Ladeskab.Test
{
    [TestFixture]
    public class TestFakeDoor
    {
        private FakeDoor _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new FakeDoor();
        }

        [Test]
        public void DoorLockedIsFalse()
        {
            Assert.That(_uut.DoorLocked, Is.False);
        }

        [Test]
        public void DoorOpenIsFalse()
        {
            Assert.That(_uut.DoorOpen, Is.False);
        }


        [Test]
        public void OpenDoor_DoorOpenIsFalse_IsTrue()
        {
            _uut.DoorOpen = false;
            _uut.DoorLocked = true;
            _uut.OpenDoor();

            Assert.That(_uut.DoorOpen, Is.False);
        }

        [Test]
        public void OpenDoor_DoorOpenIsTrue_IsTrue()
        {
            _uut.DoorOpen = true;
            _uut.DoorLocked = false;
            _uut.OpenDoor();

            Assert.That(_uut.DoorOpen, Is.True);
        }

        [Test]
        public void CloseDoor_DoorOpenIsFalse_IsFalse()
        {
            _uut.DoorOpen = false;
            _uut.CloseDoor();

            Assert.That(_uut.DoorOpen, Is.False);
        }

        [Test]
        public void CloseDoor_DoorOpenIsTrue_IsFalse()
        {
            _uut.DoorOpen = true;
            _uut.CloseDoor();

            Assert.That(_uut.DoorOpen, Is.False);
        }

        [Test]
        public void LockDoor_DoorIsNotOpen_IsTrue()
        {
            _uut.DoorLocked = false;
            _uut.DoorOpen = false;
            _uut.LockDoor();

            Assert.That(_uut.DoorLocked, Is.True);
        }

        [Test]
        public void LockDoor_DoorIsNotClosed_IsFalse()
        {
            _uut.DoorLocked = false;
            _uut.DoorOpen = true;
            _uut.LockDoor();

            Assert.That(_uut.DoorLocked, Is.False);
        }

        [Test]
        public void UnlockDoor_DoorIsLocked_IsFalse()
        {
            _uut.DoorLocked = true;
            _uut.UnlockDoor();

            Assert.That(_uut.DoorLocked, Is.False);
        }

        [Test]
        public void UnlockDoor_DoorIsNotLocked_IsFalse()
        {
            _uut.DoorLocked = false;
            _uut.UnlockDoor();

            Assert.That(_uut.DoorLocked, Is.False);
        }

        [Test]
        public void DoorvalueChanged()
        {
            bool WasRaise = false;
            _uut.DoorValueEvent += delegate { WasRaise = true; };
            _uut.OpenDoor();
            Assert.That(WasRaise, Is.EqualTo(true));
        }
    }
}