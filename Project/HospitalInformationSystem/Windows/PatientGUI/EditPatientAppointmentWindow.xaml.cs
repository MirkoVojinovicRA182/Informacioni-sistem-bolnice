using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for EditPatientAppointment.xaml
    /// </summary>
    public partial class EditPatientAppointmentWindow : Window
    {
        private Appointment appointmentForEditing;
        public EditPatientAppointmentWindow(Appointment forEditing)
        {
            InitializeComponent();
            this.appointmentForEditing = forEditing;
            LoadDateComboBox();
            LoadTimeTextBox();
        }

        private void LoadDateComboBox()
        {
            List<string> validDateList = new List<string>();
            validDateList.Add(appointmentForEditing.StartTime.Date.AddDays(1).ToString("dd.MM.yyyy"));
            validDateList.Add(appointmentForEditing.StartTime.Date.AddDays(2).ToString("dd.MM.yyyy"));
            validDateList.Add(appointmentForEditing.StartTime.Date.AddDays(-1).ToString("dd.MM.yyyy"));
            validDateList.Add(appointmentForEditing.StartTime.Date.AddDays(-2).ToString("dd.MM.yyyy"));
            dateComboBox.ItemsSource = null;
            dateComboBox.ItemsSource = validDateList;
        }

        private void LoadTimeTextBox()
        {
            string[] dateTime = appointmentForEditing.StartTime.ToString("dd.MM.yyyy HH:mm").Split(' ');
            timeTextBox.Text = dateTime[1];
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
            DateTime newStartTime = DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime oldTime = appointmentForEditing.StartTime;
            DateTime lastTimeToChange = oldTime.AddDays(-1);

            if (IsValidDate(lastTimeToChange))
            {
                AppointmentController.getInstance().ChangeStartTime(appointmentForEditing, newStartTime);
                MessageBox.Show("Termin je izmenjen.", "Menjanje termina", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
            else
            {
                MessageBox.Show("Prekasno je da se termin pomera", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private bool IsValidDate(DateTime lastTimeToChange)
        {
            if (DateTime.Now.CompareTo(lastTimeToChange) < 0)
                return true;
            return false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
