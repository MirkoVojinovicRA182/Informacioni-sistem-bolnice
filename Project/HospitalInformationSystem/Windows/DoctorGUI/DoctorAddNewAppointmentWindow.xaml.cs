using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using HospitalInformationSystem.Controller;
using System.Collections.Generic;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    public partial class Window2 : Window
    {
        private const string dateTemplate = "dd.MM.yyyy. HH:mm";
        Doctor loggedDoctor;
        private Room selectedRoomForAppointment;
        private TypeOfAppointment typeOfAppointment;

        public Window2(Doctor loggedDoctor)
        {
            InitializeComponent();
            this.loggedDoctor = loggedDoctor;
            InitDoctorsNameLabel();
            InitPatientsListBox();
            InitRooms();
            InitTypeOfAppointment();
        }

        private void InitDoctorsNameLabel()
        {
            doctorTextBox.Text = loggedDoctor.Name + " " + loggedDoctor.Surname;
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
            roomsListBox.DataContext = roomsList;
        }

        private void InitTypeOfAppointment()
        {
            if (loggedDoctor.Specialization != Specialization.Family_Physician)
                appointmentComboBox.Items.Add("Operacija");
            appointmentComboBox.Items.Add("Pregled");
        }

        private bool CheckRoomState(Room room)
        {
            DateTime dateOfAppointment = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, 
                dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
            return dateOfAppointment.AddMinutes(30) < room.RoomRenovationState.StartDate || dateOfAppointment > room.RoomRenovationState.EndDate;
        }

        private void roomsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoomForAppointment = (Room)roomsListBox.SelectedItem;
        }
        private void createAppointment()
        {
            DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
            Appointment appointment = new Appointment(date, typeOfAppointment, selectedRoomForAppointment, 
                (Patient)patientListBox.SelectedItem, loggedDoctor);
            AppointmentController.getInstance().addAppointment(appointment);
            loggedDoctor.AddAppointment(appointment);
            Patient patient = (Patient)patientListBox.SelectedItem;
            patient.AddAppointment(appointment);
        }

        private void appointmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (String.Compare((string)appointmentComboBox.SelectedItem, "Operacija") == 0)
            {
                roomLabel.Visibility = Visibility.Visible;
                roomsListBox.Visibility = Visibility.Visible;
                typeOfAppointment = TypeOfAppointment.Operacija;
            }
            else
            {
                roomLabel.Visibility = Visibility.Hidden;
                roomsListBox.Visibility = Visibility.Hidden;
                selectedRoomForAppointment = loggedDoctor.room;
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
                    dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
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
            if (appointmentComboBox.SelectedItem.Equals("Operacija") && roomsListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati prostoriju!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool IsDoctorFreeInSelectedTime()
        {
            DateTime appointmentTime = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, 
                dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
            foreach (Appointment appointment in loggedDoctor.GetAppointment())
            {
                if (appointmentTime.Ticks > appointment.StartTime.AddMinutes(-30).Ticks && appointmentTime.Ticks < appointment.StartTime.AddMinutes(30).Ticks)
                {
                    MessageBox.Show("Imate vec zakazan termin u odabranom vremenu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

        private bool IsPatientFreeInSelectedTime()
        {
            DateTime appointmentTime = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text,
                dateTemplate, System.Globalization.CultureInfo.InvariantCulture);
            Patient patient = (Patient)patientListBox.SelectedItem;
            foreach (Appointment appointment in patient.GetAppointment())
            {
                if (appointmentTime.Ticks > appointment.StartTime.AddMinutes(-30).Ticks && appointmentTime.Ticks < appointment.StartTime.AddMinutes(30).Ticks)
                {
                    MessageBox.Show("Pacijent vec ima zakazan termin u odabranom vremenu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

        private bool CheckAllInputs()
        {
            return (CheckSelectedPatient() && CheckDateInput() && CheckAppointmentComboBox() && CheckSelectedRoom() && 
                IsDoctorFreeInSelectedTime() && IsPatientFreeInSelectedTime());
        }

        private bool IsSelectedAppointmentOperation()
        {
            return (String.Compare((string)appointmentComboBox.SelectedItem, "Operacija") == 0);
        }

        private void AppointmentIsOperation()
        {

        }

        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.A))
            {
                if (CheckAllInputs() && IsSelectedAppointmentOperation())
                {
                        selectedRoomForAppointment = (Room)roomsListBox.SelectedItem;
                        if (!CheckRoomState(selectedRoomForAppointment))
                        {
                            MessageBox.Show("Prostorija je zauzeta u datom terminu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
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
    }

}
