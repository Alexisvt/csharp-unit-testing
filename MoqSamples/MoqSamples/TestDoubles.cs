using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace MoqSamples
{
    public class Bar
    {
    }
    public interface IBaz
    {
        string Name { get; }
    }

    public interface IFoo
    {
        bool DoSomething(string value);
        string ProcessString(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int amount);
        string Name { get; set; }
        int SomeOtherProperty { get; set; }
        IBaz SomeBaz { get; }
    }

    [TestFixture]
    public class TestDoubles
    {
        private Mock<IFoo> mock;

        [SetUp]
        public void Setup()
        {
           mock = new Mock<IFoo>();
        }

        [Test]
        public void OrdinaryMethodCalls()
        {
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);
            mock.Setup(foo => foo.DoSomething("pong")).Returns(false);
            mock.Setup(foo => foo.DoSomething(It.IsIn("foo", "bar"))).Returns(false);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(mock.Object.DoSomething("ping"));
                Assert.IsFalse(mock.Object.DoSomething("pong"));
                Assert.IsFalse(mock.Object.DoSomething("foo"));
                Assert.IsFalse(mock.Object.DoSomething("bar"));
            });
        }

        [Test]
        public void ArgumentsDependentMatching()
        {

            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(false);

            mock.Setup(foo => foo.Add(It.Is<int>(x => x % 2 == 0))).Returns(true);

            mock.Setup(foo => foo.Add(It.IsInRange(1, 10, Range.Inclusive))).Returns(false);

            mock.Setup(foo => foo.DoSomething(It.IsRegex("[a-z]+"))).Returns(false);
        }

        [Test]
        public void OutArguments()
        {
            var requiredOutput = "ok";
            mock.Setup(foo => foo.TryParse("ping", out requiredOutput)).Returns(true);

            string result;

            Assert.Multiple(() =>
            {
                Assert.IsTrue(mock.Object.TryParse("ping", out result));
                Assert.That(result, Is.EqualTo(requiredOutput));
            });

        }

        [Test]
        public void RefArguments()
        {

            var bar = new Bar();
            var someOtherBar = new Bar();

            mock.Setup(foo => foo.Submit(ref bar)).Returns(true);

            Assert.IsTrue(mock.Object.Submit(ref bar));
            Assert.IsFalse(mock.Object.Submit(ref someOtherBar));
        }

        [Test]
        public void ChangeOrAddImplementation()
        {

            mock.Setup(foo => foo.ProcessString(It.IsAny<string>()))
                .Returns((string str) => str.ToLowerInvariant());

            Assert.Multiple(() =>
            {
                Assert.That(mock.Object.ProcessString("ABC"),
                    Is.EqualTo("abc"));
            });
        }

        [Test]
        public void AddCallbackToChangeResults()
        {
            var calls = 0;

            mock.Setup(foo => foo.GetCount())
                .Returns(() => calls)
                .Callback(() => ++calls);

            mock.Object.GetCount();
            mock.Object.GetCount();

            Assert.Multiple(() =>
            {
                Assert.That(mock.Object.GetCount(), Is.EqualTo(2));
                Assert.That(calls, Is.EqualTo(3));
            });
        }

        [Test]
        public void ThrowingExceptions()
        {
            mock.Setup(foo => foo.DoSomething("kill"))
                .Throws<InvalidOperationException>();

            Assert.Throws<InvalidOperationException>(() => mock.Object.DoSomething("kill"));
        }

        [Test]
        public void ThrowingExceptionsV2()
        {
            mock.Setup(foo => foo.DoSomething(null))
                .Throws(new ArgumentException("cmd"));

            Assert.Throws<ArgumentException>(
                () =>
                {
                    mock.Object.DoSomething(null);
                }, "cmd");
        }

        [Test]
        public void MockingWithGetters()
        {
            mock.Setup(foo => foo.Name)
                .Returns("bar");

            mock.Object.Name = "will not be assigned";

            Assert.That(mock.Object.Name, Is.EqualTo("bar"));

        }

        [Test]
        public void MockingPropertiesOfProperties()
        {
            mock.Setup(foo => foo.SomeBaz.Name).Returns("hello");

            Assert.That(mock.Object.SomeBaz.Name, Is.EqualTo("hello"));
        }

        [Test]
        public void MockingSetters()
        {
            bool setterCalled = false;

            mock.SetupSet(foo => foo.Name = It.IsAny<string>())
                .Callback<string>(value =>
                {
                    setterCalled = true;
                });

            mock.Object.Name = "def";

            mock.VerifySet(foo =>
            {
                foo.Name = "def";
            }, Times.AtLeastOnce);
            Assert.IsTrue(setterCalled);
        }

        [Test]
        public void MockingSettersv2()
        {
            // Activate persistence over one property
            //mock.SetupProperty(foo => foo.Name);

            // Activate persistence over all properties
            mock.SetupAllProperties();

            mock.Object.Name = "abc";
            mock.Object.SomeOtherProperty = 123;

            Assert.That(mock.Object.Name, Is.EqualTo("abc"));
            Assert.That(mock.Object.SomeOtherProperty, Is.EqualTo(123));

        }

    }
}
