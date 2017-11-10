using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdFine
{
    class Program
    {
        static void Main(string[] args)
        {
            bool restart = true;
            do
            {
                Console.Clear();
                int daysLate;
                Console.Write("Enter days late: ");
                bool daysLateCheck = int.TryParse(Console.ReadLine(), out daysLate);
                if (daysLateCheck && daysLate >= 0)
                {
                    Console.Write("enter dvd type: ");
                    string dvdType = Console.ReadLine().ToLower();
                    if (dvdType == "new release" || dvdType == "old release")
                    {
                        Console.Write("Enter dvd retail value: ");
                        double retailVal;
                        bool retailValCheck = double.TryParse(Console.ReadLine(), out retailVal);
                        if (retailValCheck && retailVal > 0)
                        {
                            Console.Write("Enter member age: ");
                            int age;
                            bool checkAge = int.TryParse(Console.ReadLine(), out age);
                            if (checkAge && age > 0)
                            {
                                double initCharge = Switch(daysLate);
                                double finalCharge = RefineCharge(initCharge, age, dvdType, retailVal);

                                if (finalCharge > retailVal)
                                {
                                    finalCharge = retailVal;
                                }

                                Console.WriteLine("Your fine is: {0:N}", finalCharge);

                                restart = PromptRestart(restart);
                            }
                        }
                    }
                }
                Console.Write("Invalid input, press any key to restart...");
                Console.ReadKey();

            } while (restart);

        }

        static double Switch(int daysLate)
        {
            double extraCharge;
            const double ADDITION_1 = 0.5;
            const double ADDITION_2 = 0.75;
            const double ADDITION_3 = 1;
            const double ADDITION_4 = 2;
            const double ADDITION_5 = 2.5;

            switch (daysLate)
            {
                case 0:
                    extraCharge = 0;
                    return extraCharge;

                case 1:
                case 2:
                case 3:
                case 4:
                    extraCharge = daysLate * ADDITION_1;
                    return extraCharge;
                case 5:
                case 6:
                case 7:
                    extraCharge = daysLate * ADDITION_2;
                    return extraCharge;
                case 8:
                case 9:
                case 10:
                    extraCharge = daysLate * ADDITION_3;
                    return extraCharge;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    extraCharge = daysLate * ADDITION_4;
                    return extraCharge;
                default:
                    extraCharge = daysLate * ADDITION_5;
                    return extraCharge;

            }
        }

        static double RefineCharge(double initCharge, int age, string dvdType, double retailVal)
        {
            double finalCharge = 0;
            const double NEW_RELEASE_U18 = 0.1;
            const double OLD_RELEASE_U18 = 0.05;
            const double NEW_RELEASE_O18 = 0.12;
            const double OLD_RELEASE_O18 = 0.07;
            if (age < 18)
            {
                if (dvdType == "new release")
                {
                    finalCharge = initCharge + (retailVal * NEW_RELEASE_U18);
                    return finalCharge;
                }
                else if (dvdType == "old release")
                {
                    finalCharge = initCharge + (retailVal * OLD_RELEASE_U18);
                    return finalCharge;
                }
            }
            else if (age > 18)
            {
                if (dvdType == "new release")
                {
                    finalCharge = initCharge + (retailVal * NEW_RELEASE_O18);
                    return finalCharge;
                }
                else if (dvdType == "old release")
                {
                    finalCharge = initCharge + (retailVal * OLD_RELEASE_O18);
                    return finalCharge;
                }
            }
            return finalCharge;
        }

        static bool PromptRestart(bool restart)
        {
            Console.Write("want to calculate again?(yes/no): ");
            string restartPrompt = Console.ReadLine().ToLower();
            switch (restartPrompt)
            {
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    Console.WriteLine("invalid option, closing...");
                    return false;
            }
        }
    }
}
