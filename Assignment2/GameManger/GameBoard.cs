using Assignment2.Manager;
using System;
using System.Collections.Generic;

namespace Assignment2.GameManger
{
    public class GameBoard
    {
        public Disk[][] matrix = new Disk[8][];
        public List<Position> validMoves = new List<Position>();

        private Disk turn = Disk.BLACK;

        // all the directions to check (rows columsn diaginals)
        private Position[] directions = new Position[]{new Position(-1, 0),
        new Position(1, 0),
        new Position(0, -1),
        new Position(0, 1),
        new Position(-1, -1),
        new Position(-1, 1),
        new Position(1, -1),
        new Position(1, 1)};


        /// <summary>
        /// Standard Constructor which instanciates the board state
        /// </summary>
        public GameBoard()
        {
            //instansiate the matrix;
            for (int i = 0; i < 8; i++)
            {
                matrix[i] = new Disk[8];
                for (int j = 0; j < 8; j++)
                {
                    matrix[i][j] = Disk.EMPTY;
                }
            }
            //load the start disks
            matrix[4][4] = Disk.WHITE;
            matrix[4][3] = Disk.BLACK;
            matrix[3][3] = Disk.WHITE;
            matrix[3][4] = Disk.BLACK;

        }

        /// <summary>
        /// Function is used to place a new disk on the board.
        /// </summary>
        /// <param name="position">The position of the new disk</param>
        /// <param name="player">The type of tisk to be placed</param>
        public void ExecuteMove(Position position, Disk player)
        {
            turn = player == Disk.BLACK ? Disk.WHITE : Disk.BLACK;
            matrix[position.x][position.y] = player;

            // for each row or colimn or diags
            foreach (Position dir in directions)
            {
                // apply the dir
                int x = position.x + dir.x;
                int y = position.y + dir.y;
                List<Position> toFlip = new List<Position>();

                // we go through the direction untill we find a opponent
                while (x < 8 && x >= 0 &&
                       y < 8 && y >= 0 &&
                       matrix[x][y] == (player == Disk.BLACK ? Disk.WHITE : Disk.BLACK)
                )
                {
                    toFlip.Add(new Position(x, y));
                    x += dir.x;
                    y += dir.y;
                }

                // if we find a omponent we return true because that sthe requere ment
                if (x < 8 && x >= 0 &&
                   y < 8 && y >= 0 &&
                   matrix[x][y] == player)
                {

                    foreach (Position pos in toFlip)
                    {
                        matrix[pos.x][pos.y] = player;
                    }

                }
            }
        }
        /// <summary>
        /// Returns a list of valid moves for the specific disk type
        /// </summary>
        /// <param name="player">Disk typr</param>
        /// <returns>List of validMovies </returns>
        public List<Position> GetValidMoves(Disk player)
        {
            List<Position> moves = new List<Position>();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (IsValidMove(new Position(x, y), player))
                    {
                        moves.Add(new Position(x, y));
                    }
                }
            }

            validMoves = moves;
            return moves;
        }


        /// <summary>
        /// Evaluates if a move is valid for specific type of disk
        /// </summary>
        /// <param name="position">The move to check</param>
        /// <param name="player">The type of disk</param>
        /// <returns>true if the move is possible</returns>
        public bool IsValidMove(Position position, Disk player)
        {
            if (player != turn) return false;

            if (matrix[position.x][position.y] != Disk.EMPTY) return false;
            // for each row or colimn or diags
            foreach (Position dir in directions)
            {
                // apply the dir
                int x = position.x + dir.x;
                int y = position.y + dir.y;

                bool foundOpponent = false;

                // we go through the direction untill we find a opponent
                while (x < 8 && x >= 0 &&
                       y < 8 && y >= 0 &&
                       matrix[x][y] == (player == Disk.BLACK ? Disk.WHITE : Disk.BLACK)
                )
                {
                    foundOpponent = true;
                    x += dir.x;
                    y += dir.y;
                }
                // if we find a omponent we return true because that sthe requere ment
                if (foundOpponent &&
                   x < 8 && x >= 0 &&
                   y < 8 && y >= 0 &&
                   matrix[x][y] == player)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Function to see if the game is a draw
        /// </summary>
        /// <returns>True if the amount of disks (black and white) is the same</returns>
        public bool IsDraw()
        {
            int b = 0;
            int w = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (matrix[x][y] == Disk.BLACK) b++;
                    else w++;
                }
            }
            // if there is equaly manny blacks and whites it is a draw
            return b == w;

        }
    }
}
