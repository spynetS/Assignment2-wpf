
using Assignment2.GameManger;
using Assignment2.Manager;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Assignment2
{
    public class ComputerPlayer : Player
    {

        public ComputerPlayer(string name, Disk disk) : base(name, disk)
        {
        }
        public ComputerPlayer(string name) : base(name)
        {
        }
    
        public override void RequestMove(GameBoard board, List<Position> validMoves)
        {
            base.RequestMove(board, validMoves);
            // we simulate that it takes a long time for the computer to calculate a move
            Thread thread = new Thread(() =>
            {
                Thread.Sleep(1000);
                var pos = validMoves[new Random().Next(0, validMoves.Count)];
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SendMove(pos);
                });
            });
            thread.Start();
        }
    
    }

}
