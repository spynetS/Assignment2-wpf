using Assignment2.GameManger;
using Assignment2.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assignment2
{

    public abstract class Player
    {
        public string name {get;set;}
        public Disk disk {get;set;}

        protected GameBoard board;

        public Action<Position> RetriveMove { get; set; }

        protected Player(string name, Disk disk)
        {
            this.name = name;
            this.disk = disk;
        }
        protected Player(string name)
        {
            this.name = name;
            this.disk = Disk.EMPTY;
        }
        protected Player()
        {
        }
        /// <summary>
        /// This function triggers the retrive move event.
        /// </summary>
        /// <param name="position">The move to send to Retrive move subscribers</param>
        public virtual void SendMove(Position position)
        {
            // if the move is valid we can tell the manager
            if(board != null && board.IsValidMove(position,disk))
            {
                RetriveMove.Invoke(position);
            }
        }

        /// <summary>
        /// This function begins the players turn. It Tells the player to do a move.
        /// Then the player is don the player sends the move with RetriveMove
        /// </summary>
        /// <param name="board">The gameBoard </param>
        /// <param name="validMoves">The moves that are valid for the player</param>
        public virtual void RequestMove(GameBoard board, List<Position> validMoves)
        {
            this.board = board;
        }
    }
}
