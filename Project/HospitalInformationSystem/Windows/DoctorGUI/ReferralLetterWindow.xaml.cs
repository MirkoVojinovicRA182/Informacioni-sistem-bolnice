using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for ReferralLetterWindow.xaml
    /// </summary>
    public partial class ReferralLetterWindow : Window
    {
        private const string dateApperence = "dd.MM.yyyy. HH:mm";
        private Patient _patientForReferralLetter;
        private static ReferralLetterWindow instance = null;
        public static ReferralLetterWindow GetInstance(Patient patientForReferralLetter)
        {
            if (instance == null)
                instance = new ReferralLetterWindow(patientForReferralLetter);
            return instance;
        }
        private ReferralLetterWindow(Patient patientForReferralLetter)
        {
            InitializeComponent();
            this._patientForReferralLetter = patientForReferralLetter;
            initReferralLetterData();
        }
        private void initReferralLetterData()
        {
            patientNameLabel.Content = _patientForReferralLetter.Name + " " + _patientForReferralLetter.Surname;
            InitSpecializationComboBox();
            InitDoctorComboBox();
            InitAppointmentTypeComboBox();
            InitRoomsListBox();
            roomsListBox.IsEnabled = false;
            urgentlyCheckBox.IsEnabled = false;
        }
        private void InitSpecializationComboBox()
        {
            List<string> specializationList = new List<string>();
            string[] allSecializations = { "Doktor opste prakse", "Ginekolog", "Neurolog", "Pedijatar", "Hirurg", "Urolog" };
            specializationList.AddRange(allSecializations);
            specializationComboBox.ItemsSource = specializationList;
            specializationComboBox.SelectedItem = specializationList[0];
        }
        private Specialization specializationComboBoxTransform()
        {
            switch(specializationComboBox.SelectedItem.ToString())
            {
                case "Doktor opste prakse":
                    return Specialization.Family_Physician;
                case "Ginekolog":
                    return Specialization.Gynecologist;
                case "Neurolog":
                    return Specialization.Neurologist;
                case "Pedijatar":
                    return Specialization.Pediatrician;
                case "Hirurg":
                    return Specialization.Surgeon;
                default:
                    return Specialization.Urologist;
            }
        }
        private void InitDoctorComboBox()
        {
            List<Doctor> doctorList = new List<Doctor>();
            foreach(Doctor doctor in DoctorController.getInstance().GetDoctors())
            {
                if(doctor.Specialization == specializationComboBoxTransform())
                    doctorList.Add(doctor);
            }
            doctorComboBox.ItemsSource = doctorList;
        }
        private void InitAppointmentTypeComboBox()
        {
            List<TypeOfAppointment> typeOfAppointmentsList = new List<TypeOfAppointment>();
            typeOfAppointmentsList.Add(TypeOfAppointment.Pregled);
            if(!string.Equals(specializationComboBox.SelectedItem, "Doktor opste prakse"))
                typeOfAppointmentsList.Add(TypeOfAppointment.Operacija);
            typeOfAppointmentComboBox.ItemsSource = typeOfAppointmentsList;
            typeOfAppointmentComboBox.SelectedItem = TypeOfAppointment.Pregled;
        }
        private void InitRoomsListBox()
        {
            List<Room> operationRoomsList = new List<Room>();
            foreach(Room room in RoomController.GetInstance().GetRooms())
            {
                if (room.Type == TypeOfRoom.OperationRoom)
                    operationRoomsList.Add(room);
            }
            roomsListBox.DataContext = operationRoomsList;
        }
        private bool CheckInputs() => CheckDoctor() && CheckRoomInput() && CheckDate() && CheckDoctorAppointments() && CheckRoomState();
        private bool CheckDoctor()
        {
            if(doctorComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati lekara!", "Lekar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckSelectedRoom() => 
            (TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem == TypeOfAppointment.Operacija && roomsListBox.SelectedIndex < 0;
        private bool CheckRoomInput()
        {
            if (CheckSelectedRoom())
            {
                MessageBox.Show("Morate odabrati prostoriju!", "Prostorija", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckDate()
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                    dateApperence, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum ili vreme!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckDoctorAppointments()
        {
            DateTime inputDate = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                                                    dateApperence, System.Globalization.CultureInfo.InvariantCulture);
            Doctor doctor = (Doctor)doctorComboBox.SelectedItem;
            foreach (Appointment appointment in doctor.GetAppointment())
            {
                if (inputDate > appointment.StartTime.AddMinutes(-30) && 
                    inputDate < appointment.StartTime.AddMinutes(30))
                {
                    MessageBox.Show("Odabrani doktor vec ima termin u odabrano vreme!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        private bool CheckRoomState()
        {
            if (CheckSelectedRoom())
            {
                Room room = (Room)roomsListBox.SelectedItem;
                DateTime dateOfAppointment = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                                                                dateApperence, System.Globalization.CultureInfo.InvariantCulture);
                if (!(dateOfAppointment.AddMinutes(30) < room.RoomRenovationState.StartDate || dateOfAppointment > room.RoomRenovationState.EndDate))
                {
                    MessageBox.Show("Prostorija je zauzeta u odabranom terminu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void specializationComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            InitDoctorComboBox();
            InitAppointmentTypeComboBox();
        }
        private void typeOfAppointmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if((TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem == TypeOfAppointment.Operacija)
            {
                roomsListBox.IsEnabled = true;
                urgentlyCheckBox.IsEnabled = true;
            }
            else
            {
                roomsListBox.IsEnabled = false;
                urgentlyCheckBox.IsEnabled = false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInputs() && CheckSelectedRoom())
            {
                Appointment appointment = new Appointment(DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                    dateApperence, System.Globalization.CultureInfo.InvariantCulture),
                    (TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem,
                    (Room)roomsListBox.SelectedItem, _patientForReferralLetter, (Doctor)doctorComboBox.SelectedItem);
                AppointmentController.getInstance().AddAppointmentToAppointmentList(appointment);
            }
            else if(CheckInputs())
            {
                Appointment appointment = new Appointment(DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                    dateApperence, System.Globalization.CultureInfo.InvariantCulture),
                    (TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem,
                    ((Doctor)doctorComboBox.SelectedItem).room, _patientForReferralLetter, (Doctor)doctorComboBox.SelectedItem);
                AppointmentController.getInstance().AddAppointmentToAppointmentList(appointment);
            }
        }
    }
}
