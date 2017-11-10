using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCalculator
{
    class Program
    {
        static void Main()
        {
            //Initialise variables and layouts
            double vehicleVal = 0;
            const string inputLayout = "{0, -25}";
            const string outputLayout = "{0, -25}{1, -15:N}";

            //Executes indefinitely until user enters -999 as the vehicle value
            while (vehicleVal != -999)
            {
                //Prompts user for required information, checking each input's validity
                Console.Write(inputLayout, "Enter Vehicle Value:");
                bool checkVehicleVal = double.TryParse(Console.ReadLine(), out vehicleVal);

                Console.Write(inputLayout, "Enter Gender:");
                string gender = Console.ReadLine().ToLower();

                Console.Write(inputLayout, "Enter Age:");
                int age;
                bool checkAge = int.TryParse(Console.ReadLine(), out age);

                Console.Write(inputLayout, "Enter Penalty Points:");
                int penaltyPoints;
                bool checkPoints = int.TryParse(Console.ReadLine(), out penaltyPoints);

                int extraCharge = PenaltyPoints(penaltyPoints);

                double quote;
                if (checkPoints && checkAge && checkVehicleVal)
                {
                    quote = GetQuote(vehicleVal, extraCharge, age, gender);
                }
                else
                {
                    quote = 0;
                }


                Console.WriteLine(outputLayout, "Your Premium Quote is:", quote);

            }
            Console.Write("Sentinel value entered, press any key to close...");
            Console.ReadKey();
        }

        static int PenaltyPoints(int penaltyPoints)
        {
            //Constant and variable declarations
            int extraCharge = 0;
            const int PENALTY_1 = 100;
            const int PENALTY_2 = 200;
            const int PENALTY_3 = 300;
            const int PENALTY_4 = 400;

            //Switch statement with a case for each hour worked, then returns the value of initialPay
            switch (penaltyPoints)
            {
                case 0:
                    return 0;
                case 1:
                case 2:
                case 3:
                case 4:
                    extraCharge = PENALTY_1;
                    return extraCharge;
                case 5:
                case 6:
                case 7:
                    extraCharge = PENALTY_2;
                    return extraCharge;

                case 8:
                case 9:
                case 10:
                    extraCharge = PENALTY_3;
                    return extraCharge;

                case 11:
                case 12:
                    extraCharge = PENALTY_4;
                    return extraCharge;

                default:
                    Console.WriteLine("No Quote Possible.");
                    return extraCharge;
            }

        }

        static double GetQuote(double vehicleVal, int extraCharge, int age, string gender)
        {
            const double BASIC_PREMIUM = 0.03;
            const double MALE_PREMIUM_U25 = 0.1;
            const double FEMALE_PREMIUM_U21 = 0.06;
            double quote;
            double basicQuote = vehicleVal * BASIC_PREMIUM;

            if (gender == "female" && age < 21)
            {
                quote = basicQuote + (basicQuote * FEMALE_PREMIUM_U21) + extraCharge;
                return quote;
            }

            else if (gender == "male" && age < 25)
            {
                quote = basicQuote + (basicQuote * MALE_PREMIUM_U25) + extraCharge;
                return quote;
            }

            else
            {
                quote = basicQuote;
                return quote;
            }
        }
    }
}
