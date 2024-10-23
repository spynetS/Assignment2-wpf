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

namespace Assignment2
{
    public partial class SetUpGameDialog : Window
    {
        public string Player1Name { get; private set; }
        public string Player1Type { get; private set; }

        public string Player2Name { get; private set; }
        public string Player2Type { get; private set; }

        public SetUpGameDialog()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(Player1NameTextBox.Text) || string.IsNullOrWhiteSpace(Player2NameTextBox.Text))
            {
                MessageBox.Show("Please enter names for both players.");
                return;
            }

            // Collect data
            Player1Name = Player1NameTextBox.Text;
            Player1Type = (Player1TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            Player2Name = Player2NameTextBox.Text;
            Player2Type = (Player2TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Close the dialog and return true
            this.DialogResult = true;
            this.Close();
        }
    }

}
