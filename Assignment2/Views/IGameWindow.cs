using Assignment2.GameManger;
using Assignment2.Manager;
using System;

namespace Assignment2.Views
{
    public interface IGameWindow
    {
        /// <summary>
        /// Render the gameboards state 
        /// </summary>
        /// <param name="board">The gameboard to render</param>
        /// <param name="player">the current player</param>
        public void Render(GameBoard board, Disk player);
        /// <summary>
        /// Shows the winner of the game
        /// </summary>
        /// <param name="winner">Winner player</param>
        public void ShowWinner(Player winner);
        /// <summary>
        /// Shows a draw screen if the game was a draw
        /// </summary>
        public void ShowDraw();
        
    }
}
