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
    /// Interaction logic for NewRoomWindow.xaml
    /// </summary>
    public partial class NewRoomWindow : Window
    {
        public NewRoomWindow()
        {
            InitializeComponent();

            loadComboBox();
        }

        private void loadComboBox()
        {
            var list = new List<String>();

            list.Add("Operaciona sala");
            list.Add("Sala za odmor");
            list.Add("Soba sa krevetima");

            typeOfRoomComboBox.ItemsSource = list;

            typeOfRoomComboBox.SelectedItem = 0;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            int floor = int.Parse(floorTextBox.Text);
         
            RoomManagement roomManagement = new RoomManagement();

            roomManagement.CreateRoom(floor, id, name);

        }
    }

}
