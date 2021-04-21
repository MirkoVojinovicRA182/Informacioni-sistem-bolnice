using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WorkWithFiles;


using HospitalInformationSystem.BusinessLogic;
using System.Globalization;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for PatientAppointmentCRUDOperationsWindow.xaml
    /// </summary>
    public partial class PatientAppointmentCRUDOperationsWindow : Window
    {
        private ObservableCollection<Appointment> appointmentList;
        PatientsAppointmentsFIleManipulation save = new PatientsAppointmentsFIleManipulation();
        private Model.Patient Patient;
        private static PatientAppointmentCRUDOperationsWindow instance = null;
        public PatientAppointmentCRUDOperationsWindow()
        {
            InitializeComponent();
            AppointmentDataGrid.ItemsSource = AppointmentDataBase.getInstance().GetAppointment();
            var therapy = new List<Therapy>();
            var days = new List<DayOfWeek>();
            days.Add(DayOfWeek.Monday);
            days.Add(DayOfWeek.Tuesday);
            bool b = true;
            therapy.Add(new Therapy(Medication.Albuterol, 3, days, default(DateTime).Add(DateTime.ParseExact("21:46", "HH:mm", CultureInfo.InvariantCulture).TimeOfDay), b));
            therapy.Add(new Therapy(Medication.Losartan, 2, days, default(DateTime).Add(DateTime.ParseExact("14:00", "HH:mm", CultureInfo.InvariantCulture).TimeOfDay), b));
            this.Patient = new Model.Patient("Pera", "Petrovic", "1");
            this.Patient.SetTherapy(therapy);
            Notify();
            RefreshTable();
        }

        public static PatientAppointmentCRUDOperationsWindow getInstance()
        {
            if (instance == null)
                instance = new PatientAppointmentCRUDOperationsWindow();
            return instance;
        }

        private void NewAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            NewPatientAppointmentWindow window = new NewPatientAppointmentWindow();
            window.ShowDialog();

            RefreshTable();
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedRow = (Appointment)AppointmentDataGrid.SelectedItem;
            AppointmentManagement management = new AppointmentManagement();
            management.deleteAppointment(selectedRow);

            RefreshTable();
        }

        private void DeleteAllAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            AppointmentManagement management = new AppointmentManagement();
            management.deleteAllAppointments();
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
            NotificationsWindow window = new NotificationsWindow(this.Patient);

            window.ShowDialog();

            RefreshTable();
        }
        public void RefreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(AppointmentDataBase.getInstance().GetAppointment());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
        }
        private void Notify()
        {

            var therapies = this.Patient.GetTherapy();
            TimeSpan dayTime = new TimeSpan(24, 00, 00);
            TimeSpan currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            List<TimeSpan> notificationTime = new List<TimeSpan>();

            for (int i = 0; i < therapies.Count(); i++)
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
            var therapies = PatientAppointmentCRUDOperationsWindow.getInstance().Patient.GetTherapy();
            NotificationWindow window = new NotificationWindow();
            for (int i = 0; i < therapies.Count; i++)
            {
                if (therapies[i].Days.Contains(DateTime.Now.DayOfWeek) & therapies[i].NotificationEnabled == true & DateTime.Now.TimeOfDay.CompareTo(therapies[i].Time.AddMinutes(-61).TimeOfDay) > 0 & DateTime.Now.TimeOfDay.CompareTo(therapies[i].Time.AddMinutes(61).TimeOfDay) < 0)
                {
                    window.medicatonText.Text = therapies[i].Medication.ToString();
                    window.timeText.Text = therapies[i].TimeString;
                    window.doseText.Text = therapies[i].Dosage.ToString();
                }

            }
            window.ShowDialog();
        }

    }
}
