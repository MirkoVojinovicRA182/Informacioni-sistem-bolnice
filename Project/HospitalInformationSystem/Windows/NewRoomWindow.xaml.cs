using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Windows;
using Model;

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


        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            createRoom();
            this.Close();

        }

        private void createRoom()
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            int floor = int.Parse(floorTextBox.Text);
            TypeOfRoom type = loadType((string)typeOfRoomComboBox.SelectedItem);

            RoomManagement roomManagement = new RoomManagement();

            roomManagement.CreateRoom(floor, id, name, type);
        }

        private void loadComboBox()
        {
            var list = new List<String>();

            list.Add("Operaciona sala");
            list.Add("Prostorija za odmor");
            list.Add("Soba sa krevetima");
            list.Add("Sala za hospitalizaciju");
            list.Add("Kancelarija");
            list.Add("Prostorija za preglede");

            typeOfRoomComboBox.ItemsSource = list;
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
