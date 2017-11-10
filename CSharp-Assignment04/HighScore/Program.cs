using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HighScore
{
    /*  
     * @author Killian Kelly
     * @since 20/03/17
     * 
     * The following programme reads in a file containing information on various players of a certain game
     * and gives the user options for various forms of analysis of said players.
     */

    class Program
    {
        // Array declarations
        static string[,] playerStrings = new string[3,4];
        static int[] playerScores = new int[3];
        static string[,] playerInitials = new string[3,2];
        static string[] playerSurnames = new string[3];
        static string[] playerStars = new string[3];
        static string[] scoreRanges = {"Under 400", "400-599", "600-799", "800-999", "1000+"};


        static void Main()
        {
            Console.Clear();
            // Calls all file reading and formatting methods in the beggining
            ReadFile();
            ConvertInt();
            FormatNames();
            // Calls Menu method to create menu interface
            int userChoice = MainMenu();
            switch (userChoice)
            {
                case 1:
                    PlayerReport();
                    break;
                case 2:
                    ScoreAnalysis();
                    break;
                case 3:
                    SearchPlayer();
                    break;
                case 4:
                    break;
            }
        }

        static int MainMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine();
            Console.WriteLine("1. Player Report");
            Console.WriteLine("2. Score Analysis Report");
            Console.WriteLine("3. Search For A Player");
            Console.WriteLine("4. Exit");
            Console.WriteLine();
            Console.Write("Enter Choice: ");
            int userChoice = int.Parse(Console.ReadLine());
            switch (userChoice)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 4;
                default:
                    return 0;
            }

        }

        static void PlayerReport()
        {
            Console.Clear();

            // Format strings
            const string tableFormat1 = "{0, -25}{1, -20}{2, -25}";
            const string tableFormat2 = "{0, -25}{1, -20}";

            Console.WriteLine(tableFormat1, "Player Name", "Score", "Star Rating");
            Console.WriteLine();
            // Iterates through various arrays and displays the desired element for the player report
            for (int x = 0; x < playerSurnames.Length; x++)
            {
                Console.WriteLine(tableFormat1, playerInitials[x,0] + playerSurnames[x], playerScores[x], playerStars[x]);
            }
            Console.WriteLine();
            int averageScore = GetAverage();
            Console.WriteLine(tableFormat2, "Average Score", averageScore);
            double standardDev = GetStd(averageScore);
            Console.WriteLine(tableFormat2, "Pop Standard Deviation", standardDev);
            string topPlayer = GetTopPlayer();
            Console.WriteLine(tableFormat2, "Top Player", topPlayer);

            // Calls main method to restart the program
            Console.Write("Press enter to return to main menu...");
            Console.ReadLine();
            Main();

        }

        static void ScoreAnalysis()
        {
            // Method-specific arrays
            int[] generalCount = new int[5];
            int[] nonIrishCount = new int[5];
            int[] irishCount = new int[5];
            Console.Clear();

            const string tableFormat1 = "{0, -25}{1, -25}{2, -25}{3, -25}";

            Console.WriteLine(tableFormat1, "Score Range", "Count", "Non-Irish", "Irish");
            Console.WriteLine();
            // GetStarRating also keeps tab on the counters for this method
            GetStarRating(generalCount, nonIrishCount, irishCount);

            for (int x = 0; x < scoreRanges.Length; x++)
            {
                Console.WriteLine(tableFormat1, scoreRanges[x], generalCount[x], nonIrishCount[x], irishCount[x]);
            }
            Console.WriteLine();
            // Adds each element in each array and displays the total
            Console.WriteLine(tableFormat1, "Totals", generalCount.Sum(), nonIrishCount.Sum(), irishCount.Sum());

            Console.Write("Press enter to return to main menu...");
            Console.ReadLine();
            Main();
        }

        static void SearchPlayer()
        {
            Console.Clear();

            const string tableFormat = "{0, -20}{1, -20}";
            Console.Write("Enter Player Number:");
            string searchTarget = Console.ReadLine();
            // Looks through each name in the array and tries to find the player
            for (int x = 0; x < playerScores.Length; x++)
            {
                if (searchTarget == playerStrings[x, 0])
                {
                    Console.WriteLine();
                    Console.WriteLine(tableFormat, "Player Name:", playerStrings[x,1]);
                    Console.WriteLine(tableFormat, "Player Score:", playerScores[x]);
                    Console.WriteLine("Press enter to return to menu...");
                    Console.ReadLine();
                    Main();
                }
            }
            // If not found, displays this
            Console.WriteLine("No Match Found, returning to main menu...");
            Main();
        }

        static void ReadFile()
        {
            int x = 0;
            // Reads in scores.txt and splits each line by commas
            // Each division is assigned a spot in the playerStrings 2D array
            foreach (string line in File.ReadLines(@"H:\Programming\CSharp\HighScore\HighScore\scores.txt"))
            {
                string[] values = line.Split(',');
                playerStrings[x,0] = values[0];
                playerStrings[x,1] = values[1];
                playerStrings[x,2] = values[2];
                playerStrings[x,3] = values[3];
                x++;

            }
        }

        static void ConvertInt()
        {
            // Converts the score elements of playerStrings to its own seperate integer array
            for (int x = 0; x < playerScores.Length; x++)
            {
                playerScores[x] = Convert.ToInt32(playerStrings[x, 3]);
            }
        }

        static void FormatNames()
        {
            // Splits each name in playerStrings into a first initial and last initial
            for (int x = 0; x < playerSurnames.Length; x++)
            {
                string[] splitNames = playerStrings[x, 1].Split(' ');
                playerSurnames[x] = splitNames[1];
                playerInitials[x, 0] = splitNames[0][0] + ".";
                playerInitials[x, 1] = splitNames[1][0] + ".";
            }


        }

        static void GetStarRating(int[] generalCount, int[] nonIrishCount, int[] irishCount)
        {
            int y;
            // Runs through each player's score and assigns them a corresponding star rating for the player report
            // Also keeps tab on the counters for the score analysis
            // Uses variable Y to decide which element of the score analysis arrays to increment
            for (int x = 0; x < playerScores.Length; x++)
            {
                if (playerScores[x] < 400)
                {
                    y = 0;
                    playerStars[x] = "*";
                    generalCount[y]++;
                    if (playerStrings[x, 2] == "Irish")
                    {
                        irishCount[y]++;
                    }
                    else
                    {
                        nonIrishCount[y]++;
                    }
                }
                else if (playerScores[x] < 600)
                {
                    y = 1;
                    playerStars[x] = "**";
                    generalCount[y]++;
                    if (playerStrings[x, 2] == "Irish")
                    {
                        irishCount[y]++;
                    }
                    else
                    {
                        nonIrishCount[y]++;
                    }
                }
                else if (playerScores[x] < 700)
                {
                    y = 2;
                    playerStars[x] = "***";
                    generalCount[y]++;
                    if (playerStrings[x, 2] == "Irish")
                    {
                        irishCount[y]++;
                    }
                    else
                    {
                        nonIrishCount[y]++;
                    }
                }
                else if (playerScores[x] < 1000)
                {
                    y = 3;
                    playerStars[x] = "****";
                    generalCount[y]++;
                    if (playerStrings[x, 2] == "Irish")
                    {
                        irishCount[y]++;
                    }
                    else
                    {
                        nonIrishCount[y]++;
                    }
                }
                else if (playerScores[x] >= 1000)
                {
                    y = 4;
                    playerStars[x] = "*****";
                    generalCount[y]++;
                    if (playerStrings[x, 2] == "Irish")
                    {
                        irishCount[y]++;
                    }
                    else
                    {
                        nonIrishCount[y]++;
                    }
                }
            }
        }

        static int GetAverage()
        {
            // Simply adds all elements in playerScores and divides by its number of elements
            int sum = 0;
            foreach (int x in playerScores)
            {
                sum += x;
            }
            int average = sum / playerScores.Length;
            return average;
        }

        static double GetStd(int averageScore)
        {
            // Does math stuff to get standard deviation of scores
            double[] newVals = new double[3];
            double sum = 0;
            for (int x = 0; x < playerScores.Length; x++)
            {
                newVals[x] = Math.Pow(playerScores[x] - averageScore, 2);
                sum += newVals[x];
            }
            double newAverage = sum / playerScores.Length;
            double standardDev = Math.Round(Math.Sqrt(newAverage), 2);
            return standardDev;
        }

        static string GetTopPlayer()
        {
            // Gets index of highest score then uses that same index to get the corresponding initials
            int maxIndex = Array.IndexOf(playerScores, playerScores.Max());
            string topPlayer = playerInitials[maxIndex, 0] + playerInitials[maxIndex, 1];
            return topPlayer;
        }
    }
}
