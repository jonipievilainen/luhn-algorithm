using System;

namespace BankNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nAccount number: 423456-781");
            // BankNumbers.FinnishBankAccountNumber sourceAccountNumber = new FinnishBankAccountNumber("110335-1537");  // Not valid number
            // BankNumbers.FinnishBankAccountNumber sourceAccountNumber = new FinnishBankAccountNumber("123456-785");   // A bank group
            BankNumbers.FinnishBankAccountNumber sourceAccountNumber = new FinnishBankAccountNumber("423456-781");      // B bank group

            Console.WriteLine("Getting long format of account number...");
            string longFormatOfAccountNumber = sourceAccountNumber.GetLongFormat();
            Console.WriteLine("Long format account number: {0}", longFormatOfAccountNumber);

            Console.WriteLine("Validationg account number...");
            bool isValid = sourceAccountNumber.validateAccountNumber();
            Console.WriteLine("Validation result: {0}", isValid);

            Console.WriteLine("\nPress any key to close the application");
            Console.ReadKey();
        }
    }
}
