using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for NewPatientAppointmentSystemWindow.xaml
    /// </summary>
    public partial class NewPatientAppointmentSystemWindow : Window
    {

        private ObservableCollection<Appointment> _appointmentList;
        private Patient _patient;
        private NewPatientAppointmentWindow _previousWindow;
        public NewPatientAppointmentSystemWindow(Patient patient, NewPatientAppointmentWindow window)
        {
            InitializeComponent();
            this._patient = patient;
            _previousWindow = window;
            LoadDoctorComboBox();
        }
        private void LoadDoctorComboBox()
        {
            ObservableCollection<Doctor> allDoctors = DoctorController.getInstance().GetDoctors();
            DoctorComboBox.ItemsSource = allDoctors;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppointment();

            MessageBox.Show("Kreiran je novi termin.", "Novi termin", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RecommendButton_Click(object sender, RoutedEventArgs e)
        {
            RecommendAppointments();
            RefreshTable();
        }
        private void CreateNewAppointment()
        {
            Appointment selectedAppointment = (Appointment)AppointmentDataGrid.SelectedItem;
            if (AppointmentController.getInstance().AppointmentIsTaken(selectedAppointment))
            {
                MessageBox.Show("Termin je zauzet.", "Notifikacija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (AppointmentStartTimeHasPassed(selectedAppointment))
            {
                MessageBox.Show("Termin nije validan.", "Notifikacija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ScheduleAppointment(selectedAppointment);
            }
            Serialize();
        }
        private static void Serialize()
        {
            EquipmentController.getInstance().saveInFile();
            RoomController.GetInstance().SaveRoomsInFile();
            MedicineController.GetInstance().SaveInFile();
            DoctorController.getInstance().SaveInFlie();
            NotificationController.GetInstance().SaveInFile();
            PatientController.getInstance().SaveInFile();
            AppointmentController.getInstance().SaveAppointmentsInFile();
            AccountController.GetInstance().SaveInFile();
        }
        private bool AppointmentStartTimeHasPassed(Appointment newAppointment)
        {
            return newAppointment.StartTime.CompareTo(DateTime.Now) <= 0;
        }
        private void ScheduleAppointment(Appointment appointment)
        {
            Appointment app = FormAppointment(appointment);
            AppointmentController.getInstance().AddAppointmentToAppointmentList(app);
        }
        private Appointment FormAppointment(Appointment appointment)
        {
            Appointment app = new Appointment(appointment.StartTime, appointment.Type, appointment.Room, appointment.Patient, appointment.Doctor);
            app.SchedulingTime = DateTime.Now;
            return app;
        }
        private List<Appointment> RecommendAppointments()
        {
            List<Appointment> recommendedAppointments = new List<Appointment>();

            Doctor selectedDoctor = (Doctor)DoctorComboBox.SelectedItem;
            DateTime startDateTime = (DateTime)startDatePicker.SelectedDate;
            DateTime endDateTime = (DateTime)endDatePicker.SelectedDate;

            AppointmentGenerationContext context = new AppointmentGenerationContext();

            if ((bool)doctorPriorityRadioButton.IsChecked)
            {
                context.SetStrategy(new AppointmentGenerationDoctorPriority());
            }
            else
            {
                context.SetStrategy(new AppointmentGenerationTimePriority());
            }

            recommendedAppointments = context.RecommendAppointments(selectedDoctor, _patient, startDateTime, endDateTime);

            return recommendedAppointments;
        }
        public void RefreshTable()
        {
            _appointmentList = new ObservableCollection<Appointment>(RecommendAppointments());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = _appointmentList;
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_patient).Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            _previousWindow.Show();
        }

        private void Filter()
        {
            return !(doctor1.Specialization == doctor2.Specialization);
        }
        private bool DoctorIsPrioritized()
        {
            return (bool)DoctorPriorityRadioButton.IsChecked;
        }
        private static bool NoAvailableAppointmentsExist(List<Appointment> recommendedAppointments)
        {
            return !recommendedAppointments.Any();
        }
        private List<Appointment> GenerateFreeAppointments(Doctor doctor, DateTime startDateTime, DateTime endDateTime)
        {
            List<Appointment> existingAppointments = AppointmentController.getInstance().GetAppointments();

            List<DateTime> dateTimes = GetPossibleDatesAndTimes(startDateTime, endDateTime);
            List<Appointment> freeAppointments = GenerateAllPossibleAppointments(doctor, dateTimes);
            RemoveExistingAppointments(existingAppointments, freeAppointments);

            return freeAppointments;
        }
        private List<Appointment> GenerateAllPossibleAppointments(Doctor doctor, List<DateTime> dateTimes)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                freeAppointments.Add(new Appointment(dateTimes[i], TypeOfAppointment.Pregled, doctor.room, _patient, doctor));
            }
            return freeAppointments;
        }
        private static void RemoveExistingAppointments(List<Appointment> existingAppointments, List<Appointment> recommendedAppointments)
        {
            for (int i = 0; i < recommendedAppointments.Count; i++)
            {
                for (int j = 0; j < existingAppointments.Count; j++)
                {
                    if (existingAppointments[j].doctor == recommendedAppointments[i].doctor & existingAppointments[j].StartTime == recommendedAppointments[i].StartTime)
                    {
                        recommendedAppointments.RemoveAt(i);
                    }
                }
            }
        }
        private List<DateTime> GetPossibleDatesAndTimes(DateTime startDateTime, DateTime endDateTime)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            List<string> timesString = GetPossibleTimes();
            List<string> datesString = GetPossibleDates(startDateTime, endDateTime);
            for (int i = 0; i < datesString.Count; i++)
            {
                for (int j = 0; j < timesString.Count; j++)
                {
                    if (!(Int32.Parse(roomTextBox.Text) == appointment.room.Id))
                        _appointmentList.Remove(appointment);
                }
                AppointmentDataGrid.ItemsSource = null;
                AppointmentDataGrid.ItemsSource = _appointmentList;
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchGroup.Visibility = Visibility.Visible;
        }

        private void FinButton_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void ExitSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchGroup.Visibility = Visibility.Hidden;
        }
    }
}
