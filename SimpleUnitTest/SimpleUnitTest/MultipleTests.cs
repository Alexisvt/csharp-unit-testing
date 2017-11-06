using NUnit.Framework;

namespace SimpleUnitTest
{
    [TestFixture]
    public class MultipleTests
    {
        [Test]
        public void MultipleSampleTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(2, Is.EqualTo(2));
                Assert.That(5, Is.EqualTo(5));
            });
        }
    }
}