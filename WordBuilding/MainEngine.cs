using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordBuilding
{
    class MainEngine
    {
        public int user_choice;

        private void ClearScreen()
        {
            Console.Clear();
        }


        private void User_exit()
        {
            Environment.Exit(0);
        }

        public void DisplayMenu()
        {
            while (true)
            {
                ClearScreen();
                Console.Write("\n\n\t\t\t\t WORD BUILDING \n\n\n\n");
                Console.Write("\t\t\t\t1.Play");
                Console.Write("\t\t\t\t2.Exit");
                Console.Write("\n\n\n\n\tEnter your choice:");
                Console.WindowWidth = 100;
                Console.WindowHeight = 50;

                try
                {
                    user_choice = int.Parse(Console.ReadLine());
                }
                catch { }

                ParseUserChoice();
            }

        }


        public void GameOver(User user)
        {
            ClearScreen();
            Console.WriteLine("\n\n\n\n\n\t\t\tGAME OVER!!!!\n\n\n\n\t\t\t YOUR SCORE: {0} \n\n\n\n\t\t\tPress Enter key to go back to menu: ", user.score);
            while (true)
            {
                ConsoleKey consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == ConsoleKey.Enter)
                    break;
            }

            DisplayMenu();
        }

        private void PlayGame()
        {
            var user = new User();
            var r = new Random();
            var ch = (char)r.Next(97, 122);
            bool flag = true;

            string[] lines = File.ReadAllLines(@"D:\Programming\Games\WordBuilding\countries.txt");
            var checkWord = new Dictionary<string, int>();

            while (true && user.lives > 0)
            {
                ClearScreen();
                if (flag)
                {
                    flag = false;
                    Console.WriteLine("\n\nLives Remaining : {0} -> ", user.lives);
                    Console.WriteLine("\n\nYour Score : {0} -> ", user.score);
                    Console.WriteLine("\n\nEnter Word starting with {0} -> ", ch);
                    user.enteredValue = Console.ReadLine();
                    user.enteredValue = user.enteredValue.ToLower();
                }
                else
                {
                    user.lives--;
                    Console.WriteLine("\n\nWrong/Invalid Word!");
                    Console.WriteLine("\n\nLives Remaining : {0} -> ", user.lives);
                    Console.WriteLine("\n\nYour Score : {0} -> ", user.score);
                    Console.WriteLine("\n\nEnter Country name starting with {0} -> ", ch);
                    user.enteredValue = Console.ReadLine();
                    user.enteredValue = user.enteredValue.ToLower();
                }
                if (user.enteredValue[0] == ch || user.enteredValue[0] == (ch - 32))
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (user.enteredValue.Equals(lines[i].ToLower()))
                        {
                            if (checkWord.ContainsKey(user.enteredValue))
                            {
                                Console.WriteLine("\n\nWord is Already Entered!");
                                break;
                            }
                            else
                            {
                                checkWord.Add(user.enteredValue, 1);
                                user.score++;
                                ch = user.enteredValue[user.enteredValue.Count() - 1];
                                flag = true;
                                break;
                            }
                        }
                    }
                }

            }

            GameOver(user);
        }

        public void ParseUserChoice()
        {
            if (user_choice == 2)
                User_exit();
            else if (user_choice == 1)
            {
                ClearScreen();
                PlayGame();
            }
            else
            {
                ClearScreen();
                Console.WriteLine("\n\n\n\n\n\n\t\t\t\t\t INVALID CHOICE!");
                Console.WriteLine("\n\n\n\t\t\t\t\t\t Press any key to go back to the MENU!");
                Console.ReadKey();
            }
        }
    }
}
