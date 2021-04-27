using HospitalInformationSystem.Service;
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
            string dateTime0 = appointment.StartTime.ToString("dd.MM.yyyy HH:mm");
            string dateTime1 = appointment.StartTime.Date.AddDays(1).ToString("dd.MM.yyyy");
            string dateTime2 = appointment.StartTime.Date.AddDays(2).ToString("dd.MM.yyyy");
            string dateTime3 = appointment.StartTime.Date.AddDays(-1).ToString("dd.MM.yyyy");
            string dateTime4 = appointment.StartTime.Date.AddDays(-2).ToString("dd.MM.yyyy");
            string[] sDateTime0 = dateTime0.Split(' ');
            string[] list = {"1","2","3","4"};
            list[0] = dateTime1;
            list[1] = dateTime2;
            list[2] = dateTime3;
            list[3] = dateTime4;
            dateComboBox.ItemsSource = list;
            timeTextBox.Text = sDateTime0[1];

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAppointment();
        }

        private void EditAppointment()
        {
            string date = (string)dateComboBox.SelectedItem;
            string time = timeTextBox.Text;
            string dateTime = date + " " + time;
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime startTime = DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", provider);
            DateTime oldTime = appointment.StartTime;
            DateTime endTime = oldTime.AddDays(-1);

            if (DateTime.Now.CompareTo(endTime) < 0 & DateTime.Now.CompareTo(startTime) < 0)
            {
                //patientAppointmentManagement.changeAppointment(appointment, startTime, TypeOfAppointment.Pregled, appointment.room, appointment.patient, (Doctor)doctorComboBox.SelectedItem);
                appointment.StartTime = startTime;
                MessageBox.Show("Termin je izmenjen.", "Menjanje termina", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Prekasno je da se termin pomera", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
