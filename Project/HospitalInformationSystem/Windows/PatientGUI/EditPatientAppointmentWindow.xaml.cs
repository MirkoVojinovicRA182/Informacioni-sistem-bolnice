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
            string dateTimeFormat = "dd.MM.yyyy";
            List<string> validDateList = new List<string>();
            validDateList.AddRange(new List<string>() { appointmentForEditing.StartTime.Date.AddDays(-2).ToString(dateTimeFormat), 
                                                        appointmentForEditing.StartTime.Date.AddDays(-1).ToString(dateTimeFormat),
                                                        appointmentForEditing.StartTime.Date.AddDays(1).ToString(dateTimeFormat), 
                                                        appointmentForEditing.StartTime.Date.AddDays(2).ToString(dateTimeFormat) });
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
            AttemptToEditAppointment();
        }

        private void AttemptToEditAppointment()
        {
            DateTime originalTime = AppointmentController.getInstance().GetStartTime(appointmentForEditing);
            DateTime lastTimeToMoveAppointment = originalTime.AddDays(-1);

            if (IsValidDate(lastTimeToMoveAppointment))
            {
                EditAppointment();
            }
            else
            {
                MessageBox.Show("Prekasno je da se termin pomera", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void EditAppointment()
        {
            Appointment newAppointment = appointmentForEditing;
            AppointmentController.getInstance().ChangeStartTime(newAppointment, ParseDateTime());
            if (AppointmentIsTaken(newAppointment))
            {
                MessageBox.Show("Termin je zauzet.", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                AppointmentController.getInstance().ChangeStartTime(appointmentForEditing, ParseDateTime());
                MessageBox.Show("Termin je izmenjen.", "Menjanje termina", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsValidDate(DateTime lastTimeToChange)
        {
            if (DateTime.Now.CompareTo(lastTimeToChange) < 0)
                return true;
            return false;
        }

        private bool AppointmentIsTaken(Appointment appointment)
        {
            bool isTaken = false;
            for (int i = 0; i < AppointmentController.getInstance().getAppointment().Count; i++) {
                if (StartTimesAndDoctorsAreEqual(appointment, AppointmentController.getInstance().getAppointment()[i]))
                {
                    isTaken = true;
                }
            }
            return isTaken;
        }

        private bool StartTimesAndDoctorsAreEqual(Appointment appointment1, Appointment appointment2)
        {
            return AppointmentController.getInstance().GetStartTime(appointment1) == AppointmentController.getInstance().GetStartTime(appointment2)
                    && AppointmentController.getInstance().GetDoctor(appointment1) == AppointmentController.getInstance().GetDoctor(appointment2);
        }

        private DateTime ParseDateTime()
        {
            string date = (string)dateComboBox.SelectedItem;
            string time = timeTextBox.Text;
            string dateTime = date + " " + time;
            return DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
