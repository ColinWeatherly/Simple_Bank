/* Name: Colin Weatherly
 * Date: 4/27/2022
 * File: UnitTest1.cs
 * IDE: Visual Studio 2019
 * Description: Used for Unit Testing of the Bank class library.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankAccountNS;

namespace BankTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        // unit test code  
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        //unit test method  
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 15.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // try
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            // fail if exception is not thrown
            Assert.Fail();
        }

        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // try  
            try
            {
                account.Debit(debitAmount);
            } 
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }

            // fail if exception is not thrown
            Assert.Fail();
        }

        [TestMethod]
        public void Credit_WhenAccountIsFrozen_ShouldThrowException()
        {
            // arrange
            double beginningBalance = 11.99;
            double creditAmount = 100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            account.ToggleFreeze();

            //try
            try
            {
                account.Credit(creditAmount);
            }
            catch (Exception e)
            {
                // assert
                StringAssert.Contains(e.Message, "Account frozen");
                return;
            }

            // fail if exception is not thrown
            Assert.Fail();
        }

        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 11.99;
            double creditAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // try
            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                StringAssert.Contains(e.Message, "amount");
                return;
            }

            // fail if exception is not thrown
            Assert.Fail();
        }

        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double creditAmount = 4.55;
            double expected = 16.54;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Credit(creditAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }
    }
}
