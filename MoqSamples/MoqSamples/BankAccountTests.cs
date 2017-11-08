using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace MoqSamples
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount ba;

        [Test]
        public void DepositTest()
        {
            var log = new Mock<ILog>();
            ba = new BankAccount(log.Object) { Balance = 100 };
            ba.Deposit(100);
            Assert.That(ba.Balance, Is.EqualTo(200));
        }
    }
}
