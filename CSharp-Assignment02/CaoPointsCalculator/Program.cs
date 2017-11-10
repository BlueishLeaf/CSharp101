using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaoPointsCalculator
{
    /* @author Killian Kelly
     * @since 14/11/2016
     * 
     * This program calculates CAO points of a student based of 6 subjects.
     * This program also makes me very sad.
     */

    internal class PointsCalc
    {
        private static void AskUser(string[] subjectNames, string[] subjectGrades, string[] tables)
        {
            // The following will ask the user for the subject name and respective grade on a loop for 6 times.
            int x = 0;
            while (x <= 5)
            {
                Console.Write(tables[0], "Enter Subject ", (x + 1) + ":");
                subjectNames[x] = Console.ReadLine();
                if (string.IsNullOrEmpty(subjectNames[x]))
                {
                    Console.WriteLine("Cannot be empty, try again!");
                    continue;
                }
                Console.Write(tables[1], "Enter Grade");
                subjectGrades[x] = Console.ReadLine();
                if (string.IsNullOrEmpty(subjectGrades[x]))
                {
                    Console.WriteLine("Cannot be empty, try again!");
                    continue;
                }
                x++;
            }
        }

        private static void AssembleDic(Dictionary<string, int> pointsDic)
        {
            // Opens grades.csv, parses data, inputs it into dictionary.
            // Path to grades.csv. May need to change when move computer.
            var reader = new StreamReader(File.OpenRead(@"H:\Programming\git\CSharp\CaoPointsCalculator\CaoPointsCalculator\grades.csv"));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                var values = line.Split(',');
                pointsDic.Add(values[0], Convert.ToInt32(values[1]));
            }
        }

        private static void WriteResults(string[] subjectNames, string[] subjectGrades, int[] subjectPoints, string[] tables, Dictionary<string, int> pointsDic)
        {
            // Assigns point value to their respective grade and prints the table and adds 25 bonus points to Higher level maths.
            const int BONUS_POINTS = 25;
            Console.Clear();
            Console.WriteLine("CAO GRADE REPORT");
            Console.WriteLine();
            Console.WriteLine(tables[2], "Subject", "Grade", "Points");
            Console.WriteLine();
            int y = 0;
            while (y <= 5)
            {
                if (subjectNames[y] == "Maths" && subjectGrades[y].Contains("H") && subjectGrades[y] != "H7" && subjectGrades[y] != "H8")
                {
                    subjectPoints[y] = pointsDic[subjectGrades[y]] + BONUS_POINTS;
                    Console.WriteLine(tables[2] + " +", subjectNames[y], subjectGrades[y], subjectPoints[y]);
                }
                else if (pointsDic.ContainsKey(subjectGrades[y]))
                {
                    subjectPoints[y] = pointsDic[subjectGrades[y]];
                    Console.WriteLine(tables[2], subjectNames[y], subjectGrades[y], subjectPoints[y]);
                }
                y++;
            }
        }

        private static void Main()
        {
            // Repeats until restart = false.
            bool restart = true;
            do
            {
                string[] tables = { "{0, -10}{1, -10}", "{0, -24}", "{0, -15}{1, -15}{2, -5:d3}", "{0, -30}{1, -15}" };
                string[] subjectNames = new string[6];
                string[] subjectGrades = new string[6];
                int[] subjectPoints = new int[6];
                var pointsDic = new Dictionary<string, int>();

                Console.Clear();
                AskUser(subjectNames, subjectGrades, tables);

                AssembleDic(pointsDic);

                WriteResults(subjectNames, subjectGrades, subjectPoints, tables, pointsDic);

                // The following calculates the total points and their average.
                int pointsTotal = subjectPoints.Sum();
                int pointsAverage = pointsTotal / subjectNames.Length;

                // Displays result of previous calculations.
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(tables[3], "Total Points:", pointsTotal);
                Console.WriteLine(tables[3], "Average Points Per Subject:", pointsAverage);
                Console.WriteLine();
                Console.WriteLine("+ Includes 25 bonus points");
                Console.WriteLine();

                // If user inputs 'y' then the bool at the start will remain true and will thus restart the loop, otherwise, the program will close.
                Console.Write("Would you like to calculate again (y/n): ");
                string restartPrompt = Console.ReadLine();
                switch (restartPrompt)
                {
                    case "y":
                        break;
                    case "n":
                        restart = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice, Try again.");
                        break;
                }
            } while (restart);

        }
    }
}