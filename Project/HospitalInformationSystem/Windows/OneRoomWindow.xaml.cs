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
    /// Interaction logic for OneRoomWindow.xaml
    /// </summary>
    public partial class OneRoomWindow : Window
    {
        public OneRoomWindow()
        {
            InitializeComponent();

            loadRoomsComboBox();
            loadTypeComboBox();

        }

       

        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            RoomManagement management = new RoomManagement();
            management.ChangeRoom((Room)roomsComboBox.SelectedItem, int.Parse(idTextBox.Text), nameTextBox.Text, getType(typeComboBox.SelectedIndex), int.Parse(floorTextBox.Text));

            MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void roomsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typeComboBox.IsEnabled = true;

            Room selectedRoom = (Room)roomsComboBox.SelectedItem;

            idTextBox.Text = selectedRoom.Id.ToString();
            nameTextBox.Text = selectedRoom.Name;
            floorTextBox.Text = selectedRoom.Floor.ToString();
            fiilTypeComboBox(selectedRoom.Type);


            equipmentListBox.ItemsSource = selectedRoom.getListEq();
        }

        private void loadRoomsComboBox()
        {
            roomsComboBox.ItemsSource = RoomDataBase.getInstance().GetRoom();
        }
        private TypeOfRoom getType(int selectedValue)
        {
            TypeOfRoom type = 0;
            if (selectedValue == 0)
                type = TypeOfRoom.OperationRoom;
            else if (selectedValue == 1)
                type = TypeOfRoom.RestRoom;
            else if (selectedValue == 2)
                type = TypeOfRoom.RoomWithBeds;
            else if (selectedValue == 3)
                type = TypeOfRoom.HospitalizationRoom;
            else if (selectedValue == 4)
                type = TypeOfRoom.Office;
            else if (selectedValue == 5)
                type = TypeOfRoom.ExaminationRoom;

            return type;

        }

        private void loadTypeComboBox()
        {
            var list = new List<String>();

            list.Add("Operaciona sala");
            list.Add("Prostorija za odmor");
            list.Add("Soba sa krevetima");
            list.Add("Sala za hospitalizaciju");
            list.Add("Kancelarija");
            list.Add("Prostorija za preglede");

            typeComboBox.ItemsSource = list;

            typeComboBox.IsEnabled = false;
        }

        private void fiilTypeComboBox(TypeOfRoom type)
        {
            if (type == TypeOfRoom.ExaminationRoom)
                typeComboBox.SelectedIndex = 5;
            else if (type == TypeOfRoom.HospitalizationRoom)
                typeComboBox.SelectedIndex = 3;
            else if (type == TypeOfRoom.Office)
                typeComboBox.SelectedIndex = 4;
            else if (type == TypeOfRoom.OperationRoom)
                typeComboBox.SelectedIndex = 0;
            else if (type == TypeOfRoom.RestRoom)
                typeComboBox.SelectedIndex = 1;
            else if (type == TypeOfRoom.RoomWithBeds)
                typeComboBox.SelectedIndex = 2;
        }

    }
}
