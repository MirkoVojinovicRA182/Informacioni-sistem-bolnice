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
        private Patient patient;
        private static ReferralLetterWindow instance = null;
        public static ReferralLetterWindow GetInstance(Patient patient)
        {
            if (instance == null)
                instance = new ReferralLetterWindow(patient);
            return instance;
        }
        private ReferralLetterWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            initReferralLetterData();
        }
        private void initReferralLetterData()
        {
            patientNameLabel.Content = patient.Name + " " + patient.Surname;
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
            if (string.Equals(specializationComboBox.SelectedItem, "Doktor opste prakse"))
                return Specialization.Family_Physician;
            else if (string.Equals(specializationComboBox.SelectedItem, "Ginekolog"))
                return Specialization.Gynecologist;
            else if (string.Equals(specializationComboBox.SelectedItem, "Neurolog"))
                return Specialization.Neurologist;
            else if (string.Equals(specializationComboBox.SelectedItem, "Pedijatar"))
                return Specialization.Pediatrician;
            else if (string.Equals(specializationComboBox.SelectedItem, "Hirurg"))
                return Specialization.Surgeon;
            else
                return Specialization.Urologist;
        }
        private void InitDoctorComboBox()
        {
            List<Doctor> doctorList = new List<Doctor>();
            foreach(Doctor doctor in DoctorController.getInstance().getDoctors())
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
            List<Room> roomsList = new List<Room>();
            foreach(Room room in RoomController.getInstance().getRooms())
            {
                if (room.Type == TypeOfRoom.OperationRoom)
                    roomsList.Add(room);
            }
            roomsListBox.DataContext = RoomController.getInstance().getRooms();
        }
        private bool CheckInputs()
        {
            return (CheckSpecialization() && CheckDoctor() && CheckAppointmentType() && CheckRoom() && CheckDate());
        }
        private bool CheckSpecialization()
        {
            return !(specializationComboBox.SelectedIndex < 0);
        }
        private bool CheckDoctor()
        {
            if(doctorComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati lekara!", "Lekar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckAppointmentType()
        {
            return !(typeOfAppointmentComboBox.SelectedIndex < 0);
        }
        private bool CheckRoom()
        {
            if ((TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem == TypeOfAppointment.Operacija && roomsListBox.SelectedIndex < 0)
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
                    "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum ili vreme!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
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
            if(CheckInputs() && (TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem == TypeOfAppointment.Operacija)
            {
                Appointment appointment = new Appointment(DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                    "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                    (TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem,
                    (Room)roomsListBox.SelectedItem, patient, (Doctor)doctorComboBox.SelectedItem);
                AppointmentController.getInstance().addAppointment(appointment);
                appointment.GetDoctor().AddAppointment(appointment);
                patient.AddAppointment(appointment);

            }
            else if(CheckInputs())
            {
                Doctor doctor = (Doctor)doctorComboBox.SelectedItem;
                Appointment appointment = new Appointment(DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                    "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                    (TypeOfAppointment)typeOfAppointmentComboBox.SelectedItem,
                    doctor.room, patient, doctor);
            }
        }
    }
}
