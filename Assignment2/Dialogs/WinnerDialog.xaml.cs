using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Assignment2.Dialogs
{
    /// <summary>
    /// Interaction logic for WinnerDialog.xaml
    /// </summary>
    public partial class WinnerDialog : Window
    {
        public WinnerDialog(Player winner)
        {
            InitializeComponent();

            WinnerTextBlock.Text = $"Congratulations {winner.name}, You Won!";
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog when the button is clicked
            this.Close();
        }
    }
}
