using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoublesSamples
{
    public interface ILog
    {
        bool Write(string msg);
    }

    public class ConsoleLog: ILog
    {
        public bool Write(string msg)
        {
            Console.WriteLine(msg);
            return true;
        }
    }

    public class BankAccount
    {
        public int Balance { get; set; }
        private readonly ILog _log;

        public BankAccount(ILog log)
        {
            this._log = log;
        }

        public void Deposit(int amount)
        {
            if (_log.Write($"Depositing {amount}"))
            {
                Balance += amount;
            }
        }
    }
}
