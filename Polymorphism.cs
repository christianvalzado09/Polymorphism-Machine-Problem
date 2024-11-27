using System;

namespace BankAccountExample
{
    public class Program
    {
        public static void Main()
        {
           
            CheckingAccount checkingAccount1 = GetCheckingAccount("Tababan");
            SavingsAccount savingsAccount1 = GetSavingsAccount("Tababan");

            CheckingAccount checkingAccount2 = GetCheckingAccount("Luzon");
            SavingsAccount savingsAccount2 = GetSavingsAccount("Luzon");

            CheckingAccount checkingAccount3 = GetCheckingAccount("Valzado");
            SavingsAccount savingsAccount3 = GetSavingsAccount("Valzado");

            PerformTransactions(checkingAccount1, savingsAccount1);
            PerformTransactions(checkingAccount2, savingsAccount2);
            PerformTransactions(checkingAccount3, savingsAccount3);
        }

  
        private static CheckingAccount GetCheckingAccount(string personName)
        {
            Console.WriteLine($"\nEnter details for {personName}'s checking account:");

            Console.Write("Enter initial balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter insufficient funds fee: ");
            decimal insufficientFundsFee = Convert.ToDecimal(Console.ReadLine());

            return new CheckingAccount(personName, balance, insufficientFundsFee);
        }

        
        private static SavingsAccount GetSavingsAccount(string personName)
        {
            Console.WriteLine($"\nEnter details for {personName}'s savings account:");

            Console.Write("Enter initial balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter annual interest rate (as a decimal, e.g., 0.05 for 5%): ");
            decimal annualInterestRate = Convert.ToDecimal(Console.ReadLine());

            return new SavingsAccount(personName, balance, annualInterestRate);
        }

        
        private static void PerformTransactions(CheckingAccount checking, SavingsAccount savings)
        {
            checking.Deposit(1000);  
            checking.Withdraw(2500);  

            savings.Deposit(500);  
            savings.DepositMonthlyInterest();  
        }
    }

    public class BankAccount
    {
        public string Owner { get; set; }
        public decimal Balance { get; set; }

        
        public BankAccount(string owner, decimal balance)
        {
            Owner = owner;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine($"\n{Owner}'s Account");
            Console.WriteLine($"Deposited: ${amount:F2}");
            Console.WriteLine($"New Balance: ${Balance:F2}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew: ${amount:F2}");
                Console.WriteLine($"New Balance: ${Balance:F2}");
            }
            else
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
            }
        }
    }

    public class CheckingAccount : BankAccount
    {
        public decimal InsufficientFundsFee { get; set; }

        public CheckingAccount(string owner, decimal balance, decimal insufficientFundsFee)
            : base(owner, balance)
        {
            InsufficientFundsFee = insufficientFundsFee;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Balance -= amount + InsufficientFundsFee;
                Console.WriteLine($"Insufficient funds. Charged an insufficient funds fee of: ${InsufficientFundsFee:F2}");
                Console.WriteLine($"New Balance: ${Balance:F2}");
            }
            else
            {
                base.Withdraw(amount);
            }
        }
    }

    public class SavingsAccount : BankAccount
    {
        public decimal AnnualInterestRate { get; set; }

        public SavingsAccount(string owner, decimal balance, decimal annualInterestRate)
            : base(owner, balance)
        {
            AnnualInterestRate = annualInterestRate;
        }

        public void DepositMonthlyInterest()
        {
            decimal interest = Balance * (AnnualInterestRate / 12);
            Balance += interest;
            Console.WriteLine($"\n{Owner}'s Savings Account");
            Console.WriteLine($"Annual Interest Rate: {AnnualInterestRate * 100}%");
            Console.WriteLine($"Monthly Interest: ${interest:F2}");
            Console.WriteLine($"New Balance: ${Balance:F2}");
        }
    }
}