using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for SendToHospitalTreatmentWindow.xaml
    /// </summary>
    public partial class SendToHospitalTreatmentWindow : Window
    {
        private const string dateTemplate = "dd.MM.yyyy.";
        private Patient patientToSend;
        private static SendToHospitalTreatmentWindow instance = null;
        public static SendToHospitalTreatmentWindow GetInstance(Patient patientToSend)
        {
            if (instance == null)
                instance = new SendToHospitalTreatmentWindow(patientToSend);
            return instance;
        }
        private SendToHospitalTreatmentWindow(Patient patientToSend)
        {
            InitializeComponent();
            this.patientToSend = patientToSend;
            InitData();
        }
        private void InitData()
        {
            patientNameLabel.Content = patientToSend.Name + " " + patientToSend.Surname;
            InitRoomsForHospitalTreatment();
        }
        private void InitRoomsForHospitalTreatment()
        {
            List<Room> roomsForHospitalTreatment = new List<Room>();
            foreach(Room roomWithBed in RoomController.GetInstance().GetRooms())
                if (roomWithBed.Type == TypeOfRoom.RoomWithBeds) roomsForHospitalTreatment.Add(roomWithBed);
            roomsListBox.ItemsSource = roomsForHospitalTreatment;
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.A) && CheckAllInputs())
            {
                if (patientToSend.hospitalTreatment == null)
                {
                    PatientController.getInstance().AddHospitalTreatment(patientToSend,
                        new HospitalTreatment(
                            DateTime.ParseExact(startDateTextBox.Text,
                        dateTemplate, System.Globalization.CultureInfo.InvariantCulture),
                            DateTime.ParseExact(endDateTextBox.Text,
                        dateTemplate, System.Globalization.CultureInfo.InvariantCulture),
                            (Room)roomsListBox.SelectedItem));
                    MessageBox.Show("Pacijent je poslat na bolnicko lijecenje!", "Lecenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Pacijent je vec na bolnickom lecenju!", "Lecenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }
        private bool CheckStartDateInput()
        {
            try
            {
                DateTime startDate = DateTime.ParseExact(startDateTextBox.Text,
                    dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckEndDateInput()
        {
            try
            {
                DateTime endDate = DateTime.ParseExact(startDateTextBox.Text,
                    dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckSelectedRoom()
        {
            return (roomsListBox.SelectedIndex >= 0);
        }
        private bool CheckIfRoomHaveBeds()
        {
            foreach(DictionaryEntry de in ((Room)roomsListBox.SelectedItem).Equipment)
            {
                string str = EquipmentController.getInstance().getEquipmentName(de.Key.ToString());
                if (str.Equals("Krevet"))
                    return true;
            }
            return false;
        }
        private bool CheckAllInputs()
        {
            return (CheckSelectedRoom() && CheckEndDateInput() && CheckStartDateInput() && CheckIfRoomHaveBeds());
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void roomsListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
