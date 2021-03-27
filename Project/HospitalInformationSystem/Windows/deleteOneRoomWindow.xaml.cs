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
        public DeleteOneRoomWindow()
        {
            InitializeComponent();
            loadComboBox();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            deleteOneRoom();
            this.Close();
        }

        private void loadComboBox()
        {
            roomsComboBox.ItemsSource = RoomDataBase.getInstance().GetRoom();
        }

        private void deleteOneRoom()
        {
            RoomManagement management = new RoomManagement();

            management.DeleteRoom((Room)roomsComboBox.SelectedItem);
        }
    }
}
