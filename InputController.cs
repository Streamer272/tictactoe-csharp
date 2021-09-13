using System;

namespace tictactoe
{
    public class InputController
    {
        public static string ReadKey()
        {
            string key = Console.ReadKey().Key.ToString();

            Console.WriteLine("");

            return key;
        }
    }
}
