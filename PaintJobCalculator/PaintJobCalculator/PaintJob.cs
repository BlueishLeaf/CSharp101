using System;

namespace PaintJobCalculator
{
    /*
    * @author Killian Kelly
    * @since 07/11/2016
    */

    internal class PaintJob
    {
        private static void Main()
        {
            //Ask customer for initial essential information
            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();
            Console.Write("Enter Number of Rooms: ");
            int roomAmount = int.Parse(Console.ReadLine());
            Console.Write("Enter Size of Each Room in Sqr Feet: ");
            double wallSqrFeet = double.Parse(Console.ReadLine());
            Console.Write("Enter Cost of one Gallon: ");
            double gallonCost = double.Parse(Console.ReadLine());

            //Calculate costs of the paint
            double gallonAmount = Math.Ceiling((roomAmount * wallSqrFeet) / 150);
            double paintGrossCost = gallonAmount * gallonCost;
            double paintVat = paintGrossCost * 0.2;
            double paintTotCost = paintGrossCost + paintVat;

            //Calculate costs of the labour
            double labourHours = gallonAmount * 8;
            double labourGrossCost = labourHours * 20;
            double labourVat = labourGrossCost * 0.1;
            double labourTotCost = labourGrossCost + labourVat;

            //Calculate the total job cost
            double jobCost = paintTotCost + labourTotCost;
            double jobCostSterling = jobCost * 0.85;

            //Output invoice in a structured table format
            string myTable = "{0, -30}{1, -15}";
            string myTableNumbers = "{0, -30}{1, -15:n2}";
            Console.Clear();
            Console.WriteLine("JOB QUOTE");
            Console.WriteLine("===========");
            Console.WriteLine(myTable, "Date: ", DateTime.Now.ToString("dd/MM/yyy"));
            Console.WriteLine(myTable, "Customer Name: ", customerName);
            Console.WriteLine();
            Console.WriteLine(myTable, "Total Number of Gallons: ", gallonAmount);
            Console.WriteLine(myTable, "Total Hours of Labour: ", labourHours);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(myTableNumbers, "Cost of Paint: ", paintGrossCost);
            Console.WriteLine(myTableNumbers, "Paint VAT: ", paintVat);
            Console.WriteLine(myTableNumbers, "Total Cost of Paint: ", paintTotCost);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(myTableNumbers, "Cost of Labour: ", labourGrossCost);
            Console.WriteLine(myTableNumbers, "Labour VAT: ", labourVat);
            Console.WriteLine(myTableNumbers, "Total Cost of Labour: ", labourTotCost);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(myTableNumbers, "Total Cost of Job: ", jobCost);
            Console.WriteLine(myTableNumbers, "Total Cost of Job Sterling: ", jobCostSterling);
            Console.WriteLine("--------------------------------------------------");

            //Ask customer to close the console
            Console.Write("Press any key to continue:>>");
            Console.ReadKey();


        }
    }
}
