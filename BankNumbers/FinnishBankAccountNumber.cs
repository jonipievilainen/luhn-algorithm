using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BankNumbers
{
    public class FinnishBankAccountNumber
    {
        private string accountNumber;

        /*
         * Constructor
         */
        public FinnishBankAccountNumber(string accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        /**
         * Validate account number
         */
        public bool validateAccountNumber()
        {
            string accountNumber = this.Reverse(this.GetLongFormat());

            int validationNumber = 9999999; // No validation number

            int summary = 0;
            int multiplier = 1;
            int index = 0;
            foreach (char c in accountNumber)
            {
                int number = Convert.ToInt32(new string(c, 1));

                if (index == 0)
                {
                    validationNumber = number;  // set validation number
                }
                else
                {
                    int total = multiplier * number;

                    if (total > 9)
                    {
                        int ones = total % 10;
                        int tens = (total / 10) % 10;
                        summary = summary + ones + tens;
                    }
                    else
                    {
                        summary += total;
                    }
                }

                if (multiplier == 2)
                {
                    multiplier = 1;
                }
                else
                {
                    multiplier = 2;
                }

                index++;
            }

            int versusValue = ((int)Math.Round(summary / 10.0)) * 10;

            int calucatedValidationNumber = versusValue - summary;

            if (calucatedValidationNumber == validationNumber)
            {
                return true;
            }


            return false;
        }

        /**
         * Convert old account number to 14-character number
         */
        public string GetLongFormat()
        {
            int group = this.BankGroup();
            int firsPartLength;

            if (group == 0)
            {
                firsPartLength = 6;
            }
            else if (group == 1)
            {
                firsPartLength = 7;
            }
            else
            {
                return null; // No valid bank group found
            }

            string strCleanAccountNumber = Regex.Replace(this.accountNumber, "-", "");

            int accountNumberLength = strCleanAccountNumber.Length;

            int lastPartCharLength = accountNumberLength - firsPartLength;

            int neededZeroes = 14 - lastPartCharLength - firsPartLength;

            string firstPart = strCleanAccountNumber.Substring(0, firsPartLength);

            string lastPart = strCleanAccountNumber.Substring(firsPartLength, lastPartCharLength);

            string newAccountNumber = firstPart;

            for (int i = 0; i < neededZeroes; i++)
            {
                newAccountNumber += "0";
            }

            newAccountNumber += lastPart;

            return newAccountNumber;
        }

        /**
         * Check account number bank group
         */
        private int BankGroup()
        {
            int[][] groups = new int[2][];

            // A group
            groups[0] = new int[] {
                1,  // Nordea
                2,  // Nordea
                31, // SHB
                33, // SEB
                34, // Danske Bank
                36, // Tapiola
                37, // DnB NOR
                38, // Swedbank
                39, // S-Pankki
                6,  // ÅAB
                8   // Sampo
            };

            // B group
            groups[1] = new int[] {
                4,  // Säästöpankit, paikallisosuuspankit and Aktia
                5   // osuuspankit, OKO and Okopankki
            };

            int[] accountNumbersTypes = new int[] {
                Int32.Parse(this.accountNumber.Substring(0, 1)),    // First letter of accountNumber
                Int32.Parse(this.accountNumber.Substring(0, 2))     // First two letter of accountNumber
            };

            foreach (int types in accountNumbersTypes)
            {
                for (int group = 0; group < groups.Length; group++)
                {
                    for (int k = 0; k < groups[group].Length; k++)
                    {
                        int bankId = groups[group][k];

                        if (bankId == types)
                        {
                            return group;
                        }
                    }
                }
            }

            return 99999; // Non bank group
        }

        /**
         * Reverse string
         */
        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
