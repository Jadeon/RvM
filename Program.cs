using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuS_MinMax
{
    class Program
    {
        public static int three = 0;
        public static int two = 0;
        public static int one = 0;

        static void Main(string[] args)
        {
            bool playerTurn = true;
            int numOfSticks = 21;
            int takeSticks;
            while (numOfSticks >= 1)
            {
                if (playerTurn)
                {
                    takeSticks = TakeSticks(numOfSticks);                    
                    Console.WriteLine("Player_1 took {0} Stick(s)!\n", takeSticks);
                }
                else
                {
                    takeSticks = AITakeSticks(playerTurn, numOfSticks);
                    one = 0;
                    two = 0;
                    three = 0;
                    Console.WriteLine("AI took {0} Stick(s)!\n", takeSticks);
                }
                playerTurn = !playerTurn;
                numOfSticks -= takeSticks;
            }
            Winner(playerTurn);
            Console.ReadLine();
        }

        static int TakeSticks(int numOfSticks)
        {
            int takeSticks = 0;
            while (!VerifyTakeAmount(takeSticks))
            {
                Console.WriteLine("There are {0} Stick(s)!\nHow many would you like to take?\n(Choose Either 1, 2, or 3)", numOfSticks);
                try
                {
                    takeSticks = Int32.Parse(Console.ReadLine());
                }
                catch (System.FormatException ex)
                {
                    takeSticks = 0;
                }
                if (!VerifyTakeAmount(takeSticks))
                {
                    Console.Clear();
                    Console.WriteLine("Your choice was invalid, please try again.");
                }
            }
            return takeSticks;
        }

        static bool VerifyTakeAmount(int takeSticks)
        {
            //Create a new array consisting of values 1, 2, and 3 then check if our value is contained within that collection.
            if (new[] { 1, 2, 3 }.Contains(takeSticks))
                return true;
            else
                return false;
        }

        static int AITakeSticks( bool playerTurn, int numOfSticks)
        {            
            int maxTake = 3;
            if (numOfSticks < maxTake)
            {
                maxTake = numOfSticks;
            }

            for (int i = 1; i <= maxTake; i++)
            {
                MiniMax(playerTurn, numOfSticks, i, ref i);
            }

            if (one > two && one > three)
                return 1;
            else if (two > one && two > three)
                return 2;
            else
                return 3;
        }

        static void MiniMax(bool playerTurn, int numOfSticks, int sticksToTake, ref int rootNode)
        {
            int maxTake = 3;
            numOfSticks -= sticksToTake;
            if (numOfSticks == 0)
            {
                playerTurn = !playerTurn;
            }
            if (numOfSticks <= 1)
            {
                if (!playerTurn)
                {
                    if (rootNode == 1) one++;
                    if (rootNode == 2) two++;
                    if (rootNode == 3) three++;
                }
                else
                {
                    if (rootNode == 1) one--;
                    if (rootNode == 2) two--;
                    if (rootNode == 3) three--;
                }
                return;
            }
            if (numOfSticks < maxTake)
            {
                maxTake = numOfSticks;
            }
            for (int i = 1; i <= maxTake; i++)
            {
                MiniMax(!playerTurn, numOfSticks, i, ref rootNode);
            }
        }

        static void Winner(bool playerTurn)
        {
            if (playerTurn)
            {
                Console.WriteLine("\nPlayer 1 wins!\n");
            }
            else
            {
                Console.WriteLine("\nAI Wins!\n");
            }
        }
        

    }
}
