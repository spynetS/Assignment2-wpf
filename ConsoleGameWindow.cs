using System;

namespace Assignment2.Views
{
    public class ConsoleGameWindow : GameWindow
    {
        public void Render(GameBoard board, Disk player)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    switch (board.matrix[x][y])
                    {
                        case Disk.EMPTY:
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Green;
                            if (board.IsValidMove(new Position(x, y), player))
                            {
                                Console.Write("|" + x + "," + y + "");
                            }
                            else
                            {
                                Console.Write("|   ");
                            }
                            break;
                        case Disk.WHITE:
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("(@)");
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("");
                            break;
                        case Disk.BLACK:
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("|");
                            Console.Write("(@)");
                            Console.Write("");
                            break;
                    }
                }
                Console.Write("|\n");
                Console.WriteLine("+---+---+---+---+---+---+---+---+");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
