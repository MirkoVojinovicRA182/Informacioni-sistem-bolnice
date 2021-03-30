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

using Model;
using BusinessLogic;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for DeleteOneRoomWindow.xaml
    /// </summary>
    public partial class DeleteOneRoomWindow : Window
    {
        private bool isSelected = false;
        public DeleteOneRoomWindow()
        {
            InitializeComponent();
            refreshComboBox();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected)
            {
                deleteOneRoom();
                MessageBox.Show("Izabrana prostorija je izbrisana iz sistema.", "Brisanje", MessageBoxButton.OK, MessageBoxImage.Information);
                refreshComboBox();
            }
            else
                MessageBox.Show("Niste izabrali prostoriju!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void refreshComboBox()
        {
            roomsComboBox.ItemsSource = RoomDataBase.getInstance().GetRoom();
        }

        private void deleteOneRoom()
        {
            RoomManagement management = new RoomManagement();

            management.DeleteRoom((Room)roomsComboBox.SelectedItem);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void roomsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
        }
    }
}
