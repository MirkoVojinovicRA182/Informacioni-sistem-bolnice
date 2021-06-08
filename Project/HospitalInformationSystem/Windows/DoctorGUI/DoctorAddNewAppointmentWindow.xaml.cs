using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using HospitalInformationSystem.Controller;
using System.Collections.Generic;
using System.Windows.Input;
using static HospitalInformationSystem.Utility.Constants;
using System.Threading;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    public partial class DoctorAddNewAppointmentWindow : Window
    {
        Doctor _loggedDoctor;
        private Room _selectedRoomForAppointment;
        private TypeOfAppointment typeOfAppointment;
        private static DoctorAddNewAppointmentWindow instance = null;
        public static DoctorAddNewAppointmentWindow GetInstance(Doctor loggedDoctor)
        {
            if (instance == null)
                instance = new DoctorAddNewAppointmentWindow(loggedDoctor);
            return instance;
        }
        private DoctorAddNewAppointmentWindow(Doctor loggedDoctor)
        {
            InitializeComponent();
            this._loggedDoctor = loggedDoctor;
            InitDoctorsNameLabel();
            InitPatientsListBox();
            InitRooms();
            InitTypeOfAppointment();
        }
        private void InitDoctorsNameLabel()
        {
            doctorTextBox.Text = _loggedDoctor.Name + " " + _loggedDoctor.Surname;
        }
        private void InitPatientsListBox()
        {
            patientListBox.ItemsSource = PatientController.getInstance().getPatient();
        }
        private void InitRooms()
        {
            List<Room> roomsList = new List<Room>();
            foreach(Room room in RoomController.GetInstance().GetRooms())
            {
                if (room.Type == TypeOfRoom.OperationRoom)
                    roomsList.Add(room);
            }
            operationRoomsListBox.ItemsSource = roomsList;
        }
        private void InitTypeOfAppointment()
        {
            if (_loggedDoctor.Specialization != Specialization.Family_Physician)
                appointmentComboBox.Items.Add("Operacija");
            appointmentComboBox.Items.Add("Pregled");
        }
        private bool CheckRoomState(Room room)
        {
            DateTime dateOfAppointment = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                DATE_TIME_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            return dateOfAppointment.AddMinutes(30) < room.RoomRenovationState.StartDate || dateOfAppointment > room.RoomRenovationState.EndDate;
        }
        private void createAppointment()
        {
            DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, DATE_TIME_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            Appointment appointment = new Appointment(date, typeOfAppointment, _selectedRoomForAppointment, 
                (Patient)patientListBox.SelectedItem, _loggedDoctor);
            AppointmentController.getInstance().AddAppointmentToAppointmentList(appointment);
        }
        private void appointmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((string)appointmentComboBox.SelectedItem).Equals("Operacija"))
            {
                roomLabel.Visibility = Visibility.Visible;
                operationRoomsListBox.Visibility = Visibility.Visible;
                operationRoomsListBox.IsEnabled = true;
                typeOfAppointment = TypeOfAppointment.Operacija;
            }
            else
            {
                roomLabel.Visibility = Visibility.Hidden;
                operationRoomsListBox.Visibility = Visibility.Hidden;
                operationRoomsListBox.IsEnabled = false;
                _selectedRoomForAppointment = _loggedDoctor.room;
                typeOfAppointment = TypeOfAppointment.Pregled;
            }
        }
        private bool CheckSelectedPatient()
        {
            if (patientListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati pacijenta!", "Pacijent", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckDateInput()
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                    DATE_TIME_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum ili vreme!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckAppointmentComboBox()
        {
            if (appointmentComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati vrstu termina!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckSelectedRoom()
        {
            if (appointmentComboBox.SelectedItem.Equals("Operacija") && operationRoomsListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati prostoriju!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckIsDoctorFreeInSelectedTime()
        {
            DateTime appointmentTime = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                DATE_TIME_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            foreach (Appointment appointment in _loggedDoctor.GetAppointment())
            {
                if (appointmentTime.Ticks > appointment.StartTime.AddMinutes(-30).Ticks && appointmentTime.Ticks < appointment.StartTime.AddMinutes(30).Ticks)
                {
                    MessageBox.Show("Imate vec zakazan termin u odabranom vremenu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        private bool CheckIsPatientFreeInSelectedTime()
        {
            DateTime appointmentTime = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                DATE_TIME_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            foreach (Appointment appointment in ((Patient)patientListBox.SelectedItem).GetAppointment())
            {
                if (appointmentTime > appointment.StartTime.AddMinutes(-30) && appointmentTime < appointment.StartTime.AddMinutes(30))
                {
                    MessageBox.Show("Pacijent vec ima zakazan termin u odabranom vremenu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        private bool CheckAllInputs()
        {
            return CheckSelectedPatient() && CheckDateInput() && CheckAppointmentComboBox() && CheckSelectedRoom() && 
                CheckIsDoctorFreeInSelectedTime() && CheckIsPatientFreeInSelectedTime();
        }
        private bool CheckIsSelectedAppointmentOperation()
        {
            return (String.Compare((string)appointmentComboBox.SelectedItem, "Operacija") == 0);
        }

        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.A))
            {
                if (CheckAllInputs() && CheckIsSelectedAppointmentOperation())
                {
                    _selectedRoomForAppointment = (Room)operationRoomsListBox.SelectedItem;
                    if (!CheckRoomState(_selectedRoomForAppointment))
                    {
                        MessageBox.Show("Prostorija je zauzeta u datom terminu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    createAppointment();
                    MessageBox.Show("Termin je uspesno zakazan", "Novi Termin", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (CheckAllInputs())
                {
                    _selectedRoomForAppointment = _loggedDoctor.room;
                    createAppointment();
                    MessageBox.Show("Termin je uspesno zakazan", "Novi Termin", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
        private void patientListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
        private void operationRoomsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRoomForAppointment = (Room)operationRoomsListBox.SelectedItem;
        }
        private void operationRoomsListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }

}
