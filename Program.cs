using System;
using System.Collections.Generic;

namespace tictactoe
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TicTacToe!\nPress any key to start...");
            InputController.ReadKey();

            Team player = new Team("X");
            Team bot = new Team("D");

            Field field = new Field(new List<Team>{ player, bot });

            Console.Write(field.ToString());

            while (true)
            {
                bool isPlayerMoving = true;

                while (isPlayerMoving)
                {
                    Console.Write("> ");

                    string key = InputController.ReadKey();

                    if (key == "Q")
                    {
                        Console.WriteLine("Quiting...");
                        return;
                    }

                    switch (key)
                    {
                        case "W":
                            field.Select(Field.Up);
                            break;
                        case "D":
                            field.Select(Field.Right);
                            break;
                        case "S":
                            field.Select(Field.Down);
                            break;
                        case "A":
                            field.Select(Field.Left);
                            break;

                        case "Spacebar":
                            if (field.MarkSelected(player)) isPlayerMoving = false;
                            break;

                        default:
                            Console.WriteLine("Wrong keypress...");
                            break;
                    }

                    Console.Write(field.ToString());
                }

                field.SelectRandomAs(bot);

                Console.Write(field.ToString());

                if (field.IsFieldFull())
                {
                    Console.WriteLine("Field is full, no one is winner");
                    return;
                }
                if (field.GetWinner() != null)
                {
                    Console.WriteLine("And the winner is... " + field.GetWinner().Signature + "!!");
                    return;
                }
            }
        }
    }
}
