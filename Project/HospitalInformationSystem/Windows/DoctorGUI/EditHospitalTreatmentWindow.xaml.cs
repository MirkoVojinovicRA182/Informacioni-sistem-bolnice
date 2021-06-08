using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using static HospitalInformationSystem.Utility.Constants;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for EditHospitalTreatmentWindow.xaml
    /// </summary>
    public partial class EditHospitalTreatmentWindow : Window
    {
        private Patient _patientForEdit;
        private static EditHospitalTreatmentWindow instance = null;
        public static EditHospitalTreatmentWindow GetInstance(Patient parientForEdit)
        {
            if (instance == null)
                instance = new EditHospitalTreatmentWindow(parientForEdit);
            return instance;
        }
        private EditHospitalTreatmentWindow(Patient patientForEdit)
        {
            InitializeComponent();
            this._patientForEdit = patientForEdit;
            InitInputs();
        }
        private void InitInputs()
        {
            patientNameLabel.Content = _patientForEdit.Name + " " + _patientForEdit.Surname;
            startDateTextBox.Text = _patientForEdit.hospitalTreatment.TreatmentStartDate.ToString(DATE_TEMPLATE);
            endDateTextBox.Text = _patientForEdit.hospitalTreatment.TreatmentEndDate.ToString(DATE_TEMPLATE);
            InitRoomsForHospitalTreatment();
        }
        private void InitRoomsForHospitalTreatment()
        {
            List<Room> roomsForHospitalTreatment = new List<Room>();
            foreach (Room roomWithBed in RoomController.GetInstance().GetRooms())
                if (roomWithBed.Type == TypeOfRoom.RoomWithBeds) roomsForHospitalTreatment.Add(roomWithBed);
            roomsListBox.ItemsSource = roomsForHospitalTreatment;
            currentRoom.Content = _patientForEdit.hospitalTreatment.TreatmentRoom.Name;
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.E) && CheckAllInputs())
            {
                PatientController.getInstance().EditHospitalTreatment(_patientForEdit,
                            DateTime.ParseExact(startDateTextBox.Text,
                        DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture),
                            DateTime.ParseExact(endDateTextBox.Text,
                        DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture),
                            (Room)roomsListBox.SelectedItem);
                MessageBox.Show("Informacije o bolnickom lecenju su uspesno promjenjene!", "Lecenje", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
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
                    DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckSelectedRoom() => roomsListBox.SelectedIndex >= 0;
        private bool CheckAllInputs() => CheckSelectedRoom() && CheckEndDateInput() && CheckStartDateInput();
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;
        private void Window_KeyDown(object sender, KeyEventArgs e) => CheckKeyPress();
        private void roomsListBox_PreviewKeyDown(object sender, KeyEventArgs e) => CheckKeyPress();
    }
}
