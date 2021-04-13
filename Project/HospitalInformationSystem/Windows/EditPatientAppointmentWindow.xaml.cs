using BusinessLogic;
using HospitalInformationSystem.BusinessLogic;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for EditPatientAppointment.xaml
    /// </summary>
    public partial class EditPatientAppointmentWindow : Window
    {
        private Appointment appointment;
        public EditPatientAppointmentWindow(Appointment forEditing)
        {
            InitializeComponent();
            this.appointment = forEditing;
            string dateTime = appointment.StartTime.ToString("dd.MM.yyyy HH:mm");
            string[] sDateTime = dateTime.Split(' ');
            dateTextBox.Text = sDateTime[0];
            timeTextBox.Text = sDateTime[1];
            var database = DoctorDataBase.getInstance();
            var list = database.GetDoctors();
            doctorComboBox.ItemsSource = list;
            doctorComboBox.SelectedItem = appointment.doctor;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAppointment();

            MessageBox.Show("Termin je izmenjen.", "Menjanje termina", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditAppointment()
        {
            string date = dateTextBox.Text;
            string time = timeTextBox.Text;
            string dateTime = date + " " + time;
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime startTime = DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", provider);
            Doctor doctor = (Doctor)doctorComboBox.SelectedItem;

            AppointmentManagement patientAppointmentManagement = new AppointmentManagement();
            patientAppointmentManagement.changeAppointment(appointment, startTime, TypeOfAppointment.Pregled, appointment.room, appointment.patient, (Doctor)doctorComboBox.SelectedItem);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
