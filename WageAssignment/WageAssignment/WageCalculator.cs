using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WageAssignment
{
   /* @author Killian Kelly
    * @since 15/2/2017
    * 
    * Wage Calculator Assignment
    * This programme calculates the take home wage of an employee based on their hours worked, qualifications and age.
    */

    internal class WageCalculator
    {
        private static void Main()
        {
            bool restart = true;
            do
            {
                //Declare variables
                int hoursWorked;
                const string inputLayout = "{0, -30}";
                const string outputLayout = "{0, -30}{1, -15:n2}";

                //Clears console and prompts user for information
                //Also checks the validity of each input and repeats the prompts if the input is invalid
                Console.Clear();
                Console.Write(inputLayout, "Enter hours worked:");
                bool checkHoursWorked = int.TryParse(Console.ReadLine(), out hoursWorked);
                if (checkHoursWorked && hoursWorked >=1 && hoursWorked <= 24)
                {
                    Console.Write(inputLayout, "Qualifications (yes/no):");
                    string qualPrompt = Console.ReadLine().ToLower();
                    if (qualPrompt == "yes" || qualPrompt == "no")
                    {
                        Console.Write(inputLayout, "Enter employee age:");
                        int empAge;
                        bool checkEmpAge = int.TryParse(Console.ReadLine(), out empAge);
                        if (checkEmpAge && empAge >= 18)
                        {
                            //Calls the HourlyRate and IncomeRefiner methods and returns their respective values to the initialPay and finalPay variables
                            double initialPay = HourlyRate(hoursWorked);
                            double finalPay = IncomeRefiner(initialPay, qualPrompt, empAge);

                            //Prints the final take home pay
                            Console.WriteLine(outputLayout, "Final take home pay is:", finalPay);

                            //Calls the method to prompt the user about restarting the programme
                            restart = PromptRestart(inputLayout, restart);
                        }
                    }
                }
                //If the user enters the wrong input at any point, they will be brought here
                Console.Write("Invalid Input, Press any key to continue...");
                Console.ReadKey();
            } while (restart);

        }

        static double HourlyRate(int hoursWorked)
        {
            //Constant and variable declarations
            double initialPay;
            const double HOURLY_RATE_1 = 5;
            const double HOURLY_RATE_2 = 6;
            const double HOURLY_RATE_3 = 7;
            const double HOURLY_RATE_4 = 9;
            const double HOURLY_RATE_5 = 10;

            //Switch statement with a case for each hour worked, then returns the value of initialPay
            switch (hoursWorked)
            {
                case 1:case 2:case 3:case 4:case 5:
                    initialPay = hoursWorked * HOURLY_RATE_1;
                    return initialPay;

                case 6:case 7:
                    initialPay = hoursWorked * HOURLY_RATE_2;
                    return initialPay;

                case 8:case 9:case 10:
                    initialPay = hoursWorked * HOURLY_RATE_3;
                    return initialPay;

                case 11:case 12:case 13:case 14:case 15:
                    initialPay = hoursWorked * HOURLY_RATE_4;
                    return initialPay;

                case 16:case 17:case 18:case 19:case 20:case 21:case 22:case 23:case 24:
                    initialPay = hoursWorked * HOURLY_RATE_5;
                    return initialPay;

                default:
                    initialPay = 0;
                    Console.WriteLine("Invalid number of hours worked!");
                    return initialPay;
            }

        }

        static double IncomeRefiner(double initialPay, string qualCheck, int empAge)
        {
            //Variable and constant declarations
            const int CUTOFF_POINT = 50;
            const double UNDER_21_NO_QUAL = 0.9;
            const double OVER_21_NO_QUAL = 1.5;
            const double OVER_21_YES_QUAL = 2;
            double grossPay = new double();
            double finalPay;

            //Checks user's info and shapes their income based on their age and qualifications
            if (empAge < 21 && qualCheck == "no")
            {
                grossPay = initialPay * UNDER_21_NO_QUAL;
            }
            else if (empAge >= 21 && qualCheck == "no")
            {
                grossPay = initialPay * OVER_21_NO_QUAL;
            }
            else if (empAge >= 21 && qualCheck == "yes")
            {
                grossPay = initialPay * OVER_21_YES_QUAL;
            }

            //Checks to see if the user's grossPay is larger than the cutoff, if it is then their final pay becomes the cutoff
            if (grossPay > CUTOFF_POINT || initialPay > CUTOFF_POINT)
            {
                finalPay = CUTOFF_POINT;
                return finalPay;
            }
            //If user is under 21 but has qualifications they will default to this
            finalPay = grossPay;
            return finalPay;
        }

        static bool PromptRestart(string inputLayout, bool restart)
        {
            //Asks user if they want to calculate again, then it returns result to the restart bool
            Console.Write(inputLayout, "Do you want to calculate again? (yes/no):");
            string restartPrompt = Console.ReadLine();
            switch (restartPrompt)
            {
                case "yes":
                    return true;

                case "no":
                    return false;

                default:
                    Console.WriteLine("Invalid option, closing programme...");
                    return false;
            }
        }
    }
}
