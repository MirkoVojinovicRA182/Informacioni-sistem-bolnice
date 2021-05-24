
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


using HospitalInformationSystem.Service;
using System.Globalization;
using System.Timers;
using HospitalInformationSystem.Repository;
using HospitalInformationSystem.Controller;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for PatientAppointmentCRUDOperationsWindow.xaml
    /// </summary>
    public partial class PatientAppointmentCRUDOperationsWindow : Window
    {
        private ObservableCollection<Appointment> appointmentList;
        private Patient patient;
        private static PatientAppointmentCRUDOperationsWindow instance = null;
        public PatientAppointmentCRUDOperationsWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            RefreshTable();
        }

        public static PatientAppointmentCRUDOperationsWindow getInstance(Patient patient)
        {
            if (instance == null)
                instance = new PatientAppointmentCRUDOperationsWindow(patient);
            return instance;
        }

        private void NewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (patient.Activity.IsTroll == false)
            {
                ShowNewAppointmentWindow();
            }
            else
            {
                MessageBox.Show("Zakazivanje termina je onomogućeno zbog sumnjive aktivnosti na ovom nalogu.", "Zakazivanje termina", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CheckIfPatientIsTroll();
        }

        private void CheckIfPatientIsTroll()
        {
            SetPatientActivity();

            if (patient.Activity.NumberOfMovedAppointmentsInMonth > 3 || patient.Activity.NumberOfScheduledAppointmentsInDay > 5)
            {
                patient.Activity.IsTroll = true;
            }
        }

        private void SetPatientActivity()
        {
            patient.Activity.NumberOfMovedAppointmentsInMonth = GetNumberOfMovedAppointmentsInThePastMonth();
            patient.Activity.NumberOfScheduledAppointmentsInDay = GetNumberOfAppointmentsScheduledInPastDay();
        }

        private int GetNumberOfAppointmentsScheduledInPastDay()
        {
            int numberOfAppointments = 0;
            foreach (var appointment in AppointmentController.getInstance().GetAppointmentsByPatient(patient))
            {
                if (AppointmentWasScheduledInThePastDay(appointment))
                {
                    numberOfAppointments++;
                }
            }
            return numberOfAppointments;
        }

        private static bool AppointmentWasScheduledInThePastDay(Appointment appointment)
        {
            return appointment.SchedulingTime.CompareTo(DateTime.Now.AddDays(-1)) > 0 && appointment.SchedulingTime.CompareTo(DateTime.Now) < 0;
        }

        private int GetNumberOfMovedAppointmentsInThePastMonth()
        {
            int numberOfAppointments = 0;
            foreach (var appointment in AppointmentController.getInstance().GetAppointmentsByPatient(patient))
            {
                if (appointment.HasBeenMoved)
                {
                    if (AppointmentWasMovedInThePastMonth(appointment))
                    {
                        numberOfAppointments++;
                    }
                }
            }
            return numberOfAppointments;
        }

        private static bool AppointmentWasMovedInThePastMonth(Appointment appointment)
        {
            return appointment.SchedulingTime.CompareTo(DateTime.Now.AddMonths(-1)) > 0 && appointment.SchedulingTime.CompareTo(DateTime.Now) < 0;
        }

        private void ShowNewAppointmentWindow()
        {
            NewPatientAppointmentWindow window = new NewPatientAppointmentWindow(patient);
            this.Hide();
            window.ShowDialog();
            RefreshTable();
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedRow = (Appointment)AppointmentDataGrid.SelectedItem;

            AppointmentController.getInstance().DeleteAppointment(selectedRow);

            RefreshTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedRow = (Appointment)AppointmentDataGrid.SelectedItem;
            EditPatientAppointmentWindow window = new EditPatientAppointmentWindow(selectedRow, this);
            this.Hide();
            window.ShowDialog();

            RefreshTable();
            CheckIfPatientIsTroll();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow window = new NotificationsWindow(patient);

            window.ShowDialog();

            RefreshTable();
        }
        public void RefreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(AppointmentController.getInstance().GetAppointmentsByPatient(patient));
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
        }

        private void RateDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentReviewValidation())
            {
                ShowDoctorReviewWindow();
            }
            else
            {
                MessageBox.Show("Niste izabrali pregled koji se završio.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RateHospitalButton_Click(object sender, RoutedEventArgs e)
        {
            if (HospitalReviewValidation())
            {
                ShowHospitalReviewWindow();
            }
            else
            {
                MessageBox.Show("Niste imali dovoljno pregleda da bi ste ocenjivali bolnicu.", "Ocenjivanje bolnice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool HospitalReviewValidation()
        {
            CalculateFinishedAppointments();
            return patient.Activity.NumberOfFinishedAppointmentsSinceReview >= 5;
        }

        private void CalculateFinishedAppointments()
        {
            foreach (var appointment in AppointmentController.getInstance().GetAppointmentsByPatient(patient))
            {
                if (AppointmentIsFinished(appointment))
                {
                    patient.Activity.NumberOfFinishedAppointmentsSinceReview++;
                }
            }
        }

        private static bool AppointmentIsFinished(Appointment appointment)
        {
            return DateTime.Now.CompareTo(appointment.StartTime.AddMinutes(30)) > 0;
        }

        private void ShowHospitalReviewWindow()
        {
            ReviewHospitalWindow window = new ReviewHospitalWindow(this.patient);
            window.ShowDialog();
        }

        private bool AppointmentReviewValidation()
        {
            return AppointmentIsSelected() && AppointmentIsFinished();
        }

        private bool AppointmentIsSelected()
        {
            return AppointmentDataGrid.SelectedItem != null;
        }

        private bool AppointmentIsFinished()
        {
            return DateTime.Now.CompareTo(((Appointment)AppointmentDataGrid.SelectedItem).StartTime.AddMinutes(30)) > 0;
        }

        private void ShowDoctorReviewWindow()
        {
            ReviewDoctorWindow window = new ReviewDoctorWindow((Appointment)AppointmentDataGrid.SelectedItem);
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CheckIfPatientIsTroll();
            AppointmentController.getInstance().SaveAppointmentsInFile();
            DoctorController.getInstance().SaveInFlie();
            PatientController.getInstance().SaveInFile();
            instance = null;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            CheckIfPatientIsTroll();
            PatientMainWindow.GetInstance(patient).Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CheckIfPatientIsTroll();
            PatientMainWindow.GetInstance(patient).Show();
            this.Close();
        }
    }
}
