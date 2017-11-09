using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace MoqSamples
{
    public delegate void AlienAbductionEventHandler(int galaxy, bool returned);

    public interface IAnimal
    {
        event EventHandler FallsIll;
        void Stumble();

        event AlienAbductionEventHandler AbductedByAliens;
    }
    public class Doctor
    {
        public int TimesCured;
        public int AbductionsObserved;

        public Doctor(IAnimal animal)
        {
            animal.FallsIll += (sender, args) =>
            {
                Console.WriteLine("I will cure you!");
                TimesCured++;
            };

            animal.AbductedByAliens += (galaxy, returned) =>
            {
                AbductionsObserved++;
            };
        }
    }

    [TestFixture]
    class DoctorTests
    {
        private Mock<IAnimal> mock;

        [SetUp]
        public void SetUp()
        {
            mock = new Mock<IAnimal>();

        }

        [Test]
        public void RaiseEventWithMoq()
        {
            var doctor = new Doctor(mock.Object);

            mock.Raise(a => a.FallsIll += null, new EventArgs());

            Assert.That(doctor.TimesCured, Is.EqualTo(1));

            mock.Setup(a => a.Stumble())
                .Raises(a => a.FallsIll += null, new EventArgs());

            mock.Object.Stumble();

            Assert.That(doctor.TimesCured, Is.EqualTo(2));

            mock.Raise(a => a.AbductedByAliens += null, 42, true);

            Assert.That(doctor.AbductionsObserved, Is.EqualTo(1));
        }
    }
}
