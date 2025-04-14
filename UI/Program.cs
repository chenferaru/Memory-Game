using Ex02.ConsoleUtils;
using System;
using UI;

namespace MemoryGameEngine
{
    public class Program
    {
        public static void Main()
        {
            run();
        }

        private static void run()
        {
            bool playAgain;
            bool endGame;

            Console.WriteLine(@"Hello!
Welcome to our Memory Game!!!");
            do
            {
                playAgain = false;
                GameFlowManager gameFlowManager = new GameFlowManager();
                gameFlowManager.GameSetUp();
                endGame = gameFlowManager.PlayGame();
                if (endGame)
                {
                    break;
                }

                gameFlowManager.FinishGame();
                Console.WriteLine(@"Please select from the options below:
1 - Another game
Else - Exit");
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (userInput == 1)
                    {
                        playAgain = true;
                        Screen.Clear();
                    }
                }
            } while (playAgain);

            Console.WriteLine(@"Thank you for playing our game! 
Press Enter to exit...");
            Console.ReadLine();
        }
    }
}