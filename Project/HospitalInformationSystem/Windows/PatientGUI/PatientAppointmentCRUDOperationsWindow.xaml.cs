
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
            //AppointmentDataGrid.ItemsSource = AppointmentController.getInstance().GetAppointmentsByPatient(patient);
            var therapy = new List<Therapy>();
            var days = new List<DayOfWeek>();
            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Tuesday);
            days.Add(DayOfWeek.Wednesday);
            bool notificationsEnabled = true;
            therapy.Add(new Therapy(Medication.Albuterol, 3, days, default(DateTime).Add(DateTime.ParseExact("13:35", "HH:mm", CultureInfo.InvariantCulture).TimeOfDay), notificationsEnabled));
            therapy.Add(new Therapy(Medication.Losartan, 2, days, default(DateTime).Add(DateTime.ParseExact("10:00", "HH:mm", CultureInfo.InvariantCulture).TimeOfDay), notificationsEnabled));
            this.patient = patient;
            this.patient.SetTherapy(therapy);
            AppointmentDataGrid.DataContext = patient.GetAppointment();
            Notify();
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
            CheckIfPatientIsTroll();
            if (patient.Activity.IsTroll == false)
            {
                ShowNewAppointmentWindow();
            }
            else
            {
                MessageBox.Show("Zakazivanje termina je onomogućeno zbog sumnjive aktivnosti na ovom nalogu.", "Zakazivanje termina", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            window.ShowDialog();
            RefreshTable();
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedRow = (Appointment)AppointmentDataGrid.SelectedItem;

            AppointmentController.getInstance().removeAppointment(selectedRow);

            RefreshTable();
        }

        private void DeleteAllAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {

            AppointmentController.getInstance().removeAllAppointment();

            MessageBox.Show("Sve termini su izbrisani", "Brisanje termina", MessageBoxButton.OK, MessageBoxImage.Information);

            RefreshTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedRow = (Appointment)AppointmentDataGrid.SelectedItem;
            EditPatientAppointmentWindow window = new EditPatientAppointmentWindow(selectedRow);

            window.ShowDialog();

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
            appointmentList = new ObservableCollection<Appointment>(patient.GetAppointment());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
        }
        private void Notify()
        {

            var therapies = patient.GetTherapy();
            TimeSpan dayTime = new TimeSpan(24, 00, 00);
            TimeSpan currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            List<TimeSpan> notificationTime = new List<TimeSpan>();

            for (int i = 0; i < therapies.Count; i++)
            {
                if (therapies[i].Days.Contains(DateTime.Now.DayOfWeek) & therapies[i].NotificationEnabled == true)
                {
                    notificationTime.Add(therapies[i].Time.AddHours(-1).TimeOfDay);
                }
            }

            for (int i = 0; i < notificationTime.Count; i++)
            {
                TimeSpan timeToNotification = ((dayTime - currentTime) + notificationTime[i]);
                if (timeToNotification.TotalHours > 24)
                    timeToNotification -= new TimeSpan(24, 0, 0);
                var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
                dispatcherTimer.Interval = timeToNotification;
                dispatcherTimer.Start();
            }

        }
 
        private static void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var therapies = PatientAppointmentCRUDOperationsWindow.getInstance((Patient)MainWindow.GetUser()).patient.GetTherapy();
            NotificationWindow window = new NotificationWindow();
            for (int i = 0; i < therapies.Count; i++)
            {
                if (DateTime.Now.TimeOfDay.CompareTo(therapies[i].Time.AddMinutes(-61).TimeOfDay) > 0 && DateTime.Now.TimeOfDay.CompareTo(therapies[i].Time.AddMinutes(-59).TimeOfDay) < 0)
                {
                    window.medicatonText.Text = therapies[i].Medication.ToString();
                    window.timeText.Text = therapies[i].TimeString;
                    window.doseText.Text = therapies[i].Dosage.ToString();
                    window.ShowDialog();
                }

            }

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
            return DateTime.Now.CompareTo(AppointmentController.getInstance().GetStartTime((Appointment)AppointmentDataGrid.SelectedItem).AddMinutes(30)) > 0;
        }

        private void ShowDoctorReviewWindow()
        {
            ReviewDoctorWindow window = new ReviewDoctorWindow((Appointment)AppointmentDataGrid.SelectedItem);
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CheckIfPatientIsTroll();
            instance = null;
        }

    }
}
