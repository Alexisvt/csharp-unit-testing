using System;

namespace SimpleUnitTest
{
    public class BankAccount
    {
        public int  Balance { get; set; }

        public BankAccount(int startingBalance)
        {
            Balance = startingBalance;
        }

        public void Deposit(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive", nameof(amount));
            }
            Balance += amount;
        }

        public bool Withdraw(int amount)
        {
            if (amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }
    }
}