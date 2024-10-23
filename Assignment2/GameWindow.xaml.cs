using Assignment2.Dialogs;
using Assignment2.GameManger;
using Assignment2.Manager;
using Assignment2.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window, IGameWindow
    {
        private GameManager manager;
        public GameWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        /// <summary>
        /// Starts the game dialog to get player info. then starts the gamemanager with the player info
        /// </summary>
        private void StartNewGame()
        {
            // Show the setup dialog
            SetUpGameDialog setupDialog = new SetUpGameDialog();
            if (setupDialog.ShowDialog() == true)
            {
                // Retrieve the player names and types
                string player1Name = setupDialog.Player1Name;
                string player1Type = setupDialog.Player1Type;

                string player2Name = setupDialog.Player2Name;
                string player2Type = setupDialog.Player2Type;

                // Create the players based on the type (e.g., HumanPlayer or ComputerPlayer)
                Player player1 = CreatePlayer(player1Name, player1Type);
                Player player2 = CreatePlayer(player2Name, player2Type);

                // Pass players to GameManager to start a new game session
                manager = new GameManager(player1, player2,new GameBoard(),this);

                Init(manager);
                manager.Start();

            }
        }
        /// <summary>
        /// (Factory) function to create a player based on name and playerType
        /// </summary>
        /// <param name="playerName">The name of the created player</param>
        /// <param name="playerType">The type of player to create</param>
        /// <returns></returns>
        private Player CreatePlayer(string playerName, string playerType)
        {
            if (playerType == "HumanPlayer")
            {
                return new HumanPlayer(playerName);
            }
            else if (playerType == "ComputerPlayer")
            {
                return new ComputerPlayer(playerName);
            }
            return null;
        }
        /// <summary>
        /// Function to return what circle to render inside the button. If there is empty but valid we return a gray circle
        /// </summary>
        /// <param name="disk">The type of disk to render</param>
        /// <param name="isValid">if disk is valid return gray disk</param>
        /// <returns></returns>
        private Ellipse GetContent(Disk disk,bool isValid) {
            Color color = Colors.Transparent;
            if (disk == Disk.BLACK) { color = Colors.Black; }
            if (disk == Disk.WHITE) { color = Colors.White; }
            if (isValid) { color = Colors.DarkGray; }

            Ellipse circle = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = new SolidColorBrush(color)
            };
            return circle;

        }
        /// <summary>
        /// Inits the window with the buttons
        /// </summary>
        /// <param name="manager">the game manager</param>
        public void Init(GameManager manager)
        {
            GameBoard board = manager.GetGameBoard();
            for(int y = 0; y < board.matrix.Length; y++)
            {

                for(int x = 0; x < board.matrix[y].Length; x++)
                {
                    Button button = new Button
                    {
                        Background = new SolidColorBrush(Colors.Green),
                    };
                    Disk d = board.matrix[x][y];

                    button.Content = GetContent(d,false);

                    int localX = x;
                    int localY = y;

                    // we add a subscription to the click event
                    button.Click += (object p, RoutedEventArgs args) =>
                    {
                        // if players is humanplayer we make the click event run SendMove with 
                        // the positions of the button
                        // this will (if valid move) send to manager the position
                        if (manager.GetPlayer1() is HumanPlayer)
                        {
                            manager.GetPlayer1().SendMove(new Position(localX, localY));
                        }
                        if (manager.GetPlayer2() is HumanPlayer)
                        {
                            manager.GetPlayer2().SendMove(new Position(localX, localY));
                        }
                    };

                    // set buttons position right to grid
                    Grid.SetRow(button, y);
                    Grid.SetColumn(button, x);

                    this.grid.Children.Add(button);
                }
            }
        }
    

        public void Render(GameBoard board, Disk player)
        {
            // we loop though gameboards matrix and gets all buttons
            // and update their content with the new gameboard data
            int index = 0;
            for(int y = 0; y < board.matrix.Length; y++)
            {
                for(int x = 0; x < board.matrix[y].Length; x++)
                {
                    Button button = (Button)grid.Children[index];
                    button.Content = GetContent(board.matrix[x][y],board.IsValidMove(new Position(x,y),player));
                    index++;
                }
            }
        }
        
        public void ShowWinner(Player winner)
        {
            WinnerDialog winnerDialog = new WinnerDialog(winner);
            winnerDialog.ShowDialog();
        }
        public void ShowDraw()
        {
            DrawDialog drawDialog = new DrawDialog();
            drawDialog.ShowDialog();

        }

    }
}
