using System;
using NUnit.Framework;

namespace SimpleUnitTest
{
    [TestFixture]
    public class SimpleTests
    {
        private BankAccount ba;
        
        [SetUp]
        public void Setup()
        {
            ba = new BankAccount(100);
        }

        [Test]
        public void BankAccountShouldIncreaseOnPositiveDeposite()
        {
            // act
            ba.Deposit(100);

            // assert
            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        // Sample of test over Exceptions
        [Test]
        public void BankAccountShouldThrowOnNonPositiveAmount()
        {
            var ex = Assert.Throws<ArgumentException>(() => ba.Deposit(-1));


            StringAssert.StartsWith("Deposit amount must be positive", ex.Message);
        }

    }
}
