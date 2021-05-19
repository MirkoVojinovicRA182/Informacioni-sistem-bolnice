using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorEditAppointmentWindow.xaml
    /// </summary>
    public partial class DoctorEditAppointmentWindow : Window
    {
        private Appointment appointment;
        public DoctorEditAppointmentWindow(Appointment appointment)
        {
            InitializeComponent();
            this.appointment = appointment;
            loadPatient();
            loadRoomComboBox();
            loadPatientComboBox();

            /*if (appointment.Type.Equals(TypeOfAppointment.Operacija))
            {
                roomLabel.Visibility = Visibility.Visible;
                roomComboBox.Visibility = Visibility.Visible;
                roomComboBox.IsEnabled = true;
                roomComboBox.SelectedItem = appointment.room;
            }*/
        }
        private void loadPatient()
        {
            dateTextBox.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            timeTextBox.Text = appointment.StartTime.ToString("HH:mm");
        }

        private void loadRoomComboBox()
        {
            //roomComboBox.ItemsSource = RoomController.getInstance().getRooms();
            //roomComboBox.SelectedIndex = 10;
        }

        private void loadPatientComboBox()
        {
            patientListBox.ItemsSource = PatientController.getInstance().getPatient();
            patientListBox.SelectedIndex = 10;
            patientListBox.SelectedItem = appointment.GetPatient();
        }

        private bool checkData()
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum ili vreme!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckRoomState(Room room)
        {
            DateTime dateOfAppointment = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return dateOfAppointment.AddMinutes(30) < room.RoomRenovationState.StartDate || dateOfAppointment > room.RoomRenovationState.EndDate;
        }

        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.E))
            {
                if (checkData() && CheckRoomState(appointment.room))
                {
                    DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    Room newRoom;
                    if (appointment.Type.Equals(TypeOfAppointment.Operacija))
                    {
                        //newRoom = (Room)roomComboBox.SelectedItem;
                        newRoom = appointment.room;
                    }
                    else
                    {
                        newRoom = appointment.room;
                    }
                    if(patientListBox.SelectedIndex < 0)
                        AppointmentController.getInstance().changeAppointment(appointment, date, appointment.Type, newRoom, appointment.patient, appointment.doctor);
                    else
                        AppointmentController.getInstance().changeAppointment(appointment, date, appointment.Type, newRoom, (Patient)patientListBox.SelectedItem, appointment.doctor);
                    MessageBox.Show("Informacije o terminu su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }

        private void patientListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
