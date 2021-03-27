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

            loadComboBox();
        }

        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            changeRoomInfo((Room)roomsComboBox.SelectedItem);
            MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void roomsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selectedRoom = (Room)roomsComboBox.SelectedItem;

            idTextBox.Text = selectedRoom.Id.ToString();
            nameTextBox.Text = selectedRoom.Name;
            floorTextBox.Text = selectedRoom.Floor.ToString();
            fillTypeTextBox(selectedRoom.Type);
        }

        private void loadComboBox()
        {
            roomsComboBox.ItemsSource = RoomDataBase.getInstance().GetRoom();
        }

        private void fillTypeTextBox(TypeOfRoom type)
        {
            if (type == TypeOfRoom.ExaminationRoom)
                typeTextBox.Text = "Prostorija za preglede";
            else if (type == TypeOfRoom.HospitalizationRoom)
                typeTextBox.Text = "Sala za hospitalizaciju";
            else if (type == TypeOfRoom.Office)
                typeTextBox.Text = "Kancelarija";
            else if (type == TypeOfRoom.OperationRoom)
                typeTextBox.Text = "Operaciona sala";
            else if (type == TypeOfRoom.RestRoom)
                typeTextBox.Text = "Prostorija za odmor";
            else if (type == TypeOfRoom.RoomWithBeds)
                typeTextBox.Text = "Soba sa krevetima";

        }

        private void changeRoomInfo(Room room)
        {
            room.Id = int.Parse(idTextBox.Text);
            room.Name = nameTextBox.Text;
            room.Floor = int.Parse(floorTextBox.Text);
            room.Type = loadType(typeTextBox.Text);
        }

        private TypeOfRoom loadType(string selectedValue)
        {
            TypeOfRoom type = 0;
            if (String.Compare(selectedValue, "Operaciona sala") == 0)
                type = TypeOfRoom.OperationRoom;
            else if (String.Compare(selectedValue, "Prostorija za odmor") == 0)
                type = TypeOfRoom.RestRoom;
            else if (String.Compare(selectedValue, "Soba sa krevetima") == 0)
                type = TypeOfRoom.RoomWithBeds;
            else if (String.Compare(selectedValue, "Sala za hospitalizaciju") == 0)
                type = TypeOfRoom.HospitalizationRoom;
            else if (String.Compare(selectedValue, "Kancelarija") == 0)
                type = TypeOfRoom.Office;
            else if (String.Compare(selectedValue, "Prostorija za preglede") == 0)
                type = TypeOfRoom.ExaminationRoom;

            return type;

        }

    }
}
