using System;

namespace tictactoe
{
    public class Bot
    {
        public static int GetRandomPosition(int max)
        {
            return new Random().Next(0, max);
        }
    }
}
