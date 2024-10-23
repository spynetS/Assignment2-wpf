using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using Assignment2.GameManger;
using Assignment2.Views;

namespace Assignment2.Manager
{
    public class GameManager
    {
        private Player[] players = new Player[2];
        private int playerIndex = 0;
        private GameBoard gameBoard;
        private IGameWindow gameWindow;

        public GameManager(Player player1, Player player2, GameBoard gameBoard, IGameWindow gameWindow)
        {
            // init players and board and window
            // we subscripe to players RetriveMove event
            // to get their updates
            this.players[0] = player1;
            this.players[0].disk = Disk.BLACK;
            this.players[0].RetriveMove += Update;

            this.players[1] = player2;
            this.players[1].disk = Disk.WHITE;
            this.players[1].RetriveMove += Update;

            this.gameBoard = gameBoard;
            this.gameWindow = gameWindow;
        }

        public Player GetPlayer() {
            return players[playerIndex];
        }
        public Player GetPlayer1() {
            return players[0];
        }
        public Player GetPlayer2() {
            return players[1];
        }
        private Player NextPlayer() {
            playerIndex = (playerIndex + 1) % 2;
            return players[playerIndex];
        }
        private Position clickPos = null;
        public void Update (Position pos){

            Trace.WriteLine("UPDATE");
            gameBoard.ExecuteMove(pos,GetPlayer().disk);

            NextPlayer();
            Start();

        } 
        
        public void Start()
        {
            var player = GetPlayer();
            // render the state to the window
            gameWindow.Render(gameBoard,player.disk);

            List<Position> validMovies = gameBoard.GetValidMoves(player.disk);
            // we there is no valid movies this player has lost
            if(validMovies.Count > 0)
            {
                player.RequestMove(gameBoard,validMovies);
            }
            else
            {
                // if the winner isnt disk empty  we have a winnser
                // else it is a draw
                if(gameBoard.IsDraw())
                {
                    gameWindow.ShowDraw();
                }
                else
                {
                    gameWindow.ShowWinner(player);
                }
            }
                
        }
        public GameBoard GetGameBoard() { return gameBoard; } 
    }
}
