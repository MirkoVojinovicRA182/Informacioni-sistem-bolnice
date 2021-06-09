using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Windows.PatientGUI;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for EditAppointmentPage.xaml
    /// </summary>
    public partial class EditAppointmentPage : Page
    {
        private ObservableCollection<Appointment> appointmentList;
        private Patient patient;
        private static EditAppointmentPage instance = null;
        public EditAppointmentPage(Patient patient)
        {
            InitializeComponent();
            AppointmentDataGrid.ItemsSource = AppointmentController.getInstance().GetAppointments();
            var days = new List<DayOfWeek>();
            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Tuesday);
            this.patient = patient;
            RefreshTable();
        }
        public static EditAppointmentPage getInstance(Patient patient)
        {
            if (instance == null)
                instance = new EditAppointmentPage(patient);
            return instance;
        }

        private void NewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            NewPatientAppointmentWindow window = new NewPatientAppointmentWindow(patient);
            window.ShowDialog();
            RefreshTable();
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedRow = (Appointment)AppointmentDataGrid.SelectedItem;
            AppointmentController.getInstance().DeleteAppointment(selectedRow);
            RefreshTable();
        }

        private void DeleteAllAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            AppointmentController.getInstance().RemoveAllAppointments();
            MessageBox.Show("Sve termini su izbrisani", "Brisanje termina", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditPatientAppointmentWindow editAppointmentWindow = new EditPatientAppointmentWindow((Appointment)AppointmentDataGrid.SelectedItem, null);
            editAppointmentWindow.ShowDialog();
            RefreshTable();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow window = new NotificationsWindow(patient);
            window.ShowDialog();
            RefreshTable();
        }
        public void RefreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(AppointmentController.getInstance().GetAppointments());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
        }
        private static void DispatcherTimer_Tick(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
