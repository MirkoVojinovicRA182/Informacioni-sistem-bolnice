using BusinessLogic;
using HospitalInformationSystem.BusinessLogic;
using Model;
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

namespace HospitalInformationSystem.Windows
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
        }
        private void loadPatient()
        {
            //roomsTextBox.Text = appointment.room.Id.ToString();
            dateTextBox.Text = appointment.StartTime.ToString("dd.MM.yyyy");
            timeTextBox.Text = appointment.StartTime.ToString("HH:mm");
            //patientTextBox.Text = appointment.patient.Name + " " +appointment.patient.Surname;
        }

        private void loadRoomComboBox()
        {
            roomComboBox.ItemsSource = RoomDataBase.getInstance().getRooms();

            roomComboBox.SelectedIndex = 10;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            Room newRoom = (Room)roomComboBox.SelectedItem;

            AppointmentManagement appointmentManagement = new AppointmentManagement();
            appointmentManagement.changeAppointment(appointment, date, TypeOfAppointment.Pregled, newRoom, appointment.patient, appointment.doctor);

            MessageBox.Show("Informacije o prostoriji su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
