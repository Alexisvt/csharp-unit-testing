using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestDoublesSamples
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount _ba;

        [Test]
        public void DepositIntegrationTest()
        {
            _ba = new BankAccount(new ConsoleLog()) { Balance = 100};
            _ba.Deposit(100);
            Assert.That(_ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void DepositUnitTestWithFake()
        {
            var log = new NullLog();
            _ba = new BankAccount(log) { Balance = 100 };
            _ba.Deposit(100);
            Assert.That(_ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void DepositUnitTestWithDynamicFake()
        {
            var log = Null<ILog>.Instance;

            _ba = new BankAccount(log) { Balance = 100};
            _ba.Deposit(100);
            Assert.That(_ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void DepositUnitTestWithStub()
        {
            var log = new NullLogWithResult(true);

            _ba = new BankAccount(log) { Balance = 100};
            _ba.Deposit(100);
            Assert.That(_ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void DepositWithMock()
        {
            var log = new LogMock(true);
            _ba = new BankAccount(log) {Balance = 100} ;
            _ba.Deposit(100);
            Assert.Multiple(() =>
            {
               Assert.That(_ba.Balance, Is.EqualTo(200));
                Assert.That(log.MethodCallCount[nameof(LogMock.Write)], Is.EqualTo(1));
            });
        }
    }
}
