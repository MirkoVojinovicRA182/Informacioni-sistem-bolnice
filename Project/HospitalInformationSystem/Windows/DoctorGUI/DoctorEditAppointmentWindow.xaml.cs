using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorEditAppointmentWindow.xaml
    /// </summary>
    public partial class DoctorEditAppointmentWindow : Window
    {

        Appointment appointment;
        public DoctorEditAppointmentWindow(Appointment appointment)
        {
            InitializeComponent();
            this.appointment = appointment;
            loadPatient();
            loadRoomComboBox();
            loadPatientComboBox();

            if(appointment.Type.Equals(TypeOfAppointment.Operacija))
            {
                roomLabel.Visibility = Visibility.Visible;
                roomComboBox.Visibility = Visibility.Visible;
                roomComboBox.IsEnabled = true;
                roomComboBox.SelectedItem = appointment.room;
            }
        }
        private void loadPatient()
        {
            //roomsTextBox.Text = appointment.room.Id.ToString();
            dateTextBox.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            timeTextBox.Text = appointment.StartTime.ToString("HH:mm");
            //patientTextBox.Text = appointment.patient.Name + " " +appointment.patient.Surname;
        }

        private void loadRoomComboBox()
        {
            roomComboBox.ItemsSource = RoomController.GetInstance().getRooms();
            roomComboBox.SelectedIndex = 10;
        }

        private void loadPatientComboBox()
        {
            patientComboBox.ItemsSource = PatientController.getInstance().getPatient();
            patientComboBox.SelectedIndex = 10;
            patientComboBox.SelectedItem = appointment.GetPatient();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkData() && CheckRoomState((Room)roomComboBox.SelectedItem))
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                Room newRoom;
                if (appointment.Type.Equals(TypeOfAppointment.Operacija))
                {
                    newRoom = (Room)roomComboBox.SelectedItem;
                }
                else
                {
                    newRoom = appointment.room;
                }
                AppointmentController.getInstance().changeAppointment(appointment, date, appointment.Type, newRoom, (Patient)patientComboBox.SelectedItem, appointment.doctor);

                MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
    }
}
