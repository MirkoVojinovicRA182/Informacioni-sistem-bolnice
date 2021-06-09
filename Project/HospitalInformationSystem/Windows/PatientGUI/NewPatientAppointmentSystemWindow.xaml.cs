using HospitalInformationSystem.Controller;
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

        private ObservableCollection<Appointment> appointmentList;
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
            Doctor selectedDoctor = (Doctor)DoctorComboBox.SelectedItem;
            DateTime startDateTime = (DateTime)startDatePicker.SelectedDate;
            DateTime endDateTime = (DateTime)endDatePicker.SelectedDate;

            List<Appointment> recommendedAppointments = GenerateFreeAppointments(selectedDoctor, startDateTime, endDateTime);

            if (NoAvailableAppointmentsExist(recommendedAppointments))
            {
                if (DoctorIsPrioritized())
                {
                    recommendedAppointments = GenerateAppointmentsForWiderRangeOfDates(selectedDoctor, startDateTime, endDateTime);
                }
                else
                {
                    recommendedAppointments = GenerateAppointmentsForDoctorsOfSameSpecialization(selectedDoctor, startDateTime, endDateTime);
                }
            }

            return recommendedAppointments;
        }
        private List<Appointment> GenerateAppointmentsForWiderRangeOfDates(Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {
            return GenerateFreeAppointments(selectedDoctor, startDateTime.AddDays(-3), endDateTime.AddDays(3));
        }
        private List<Appointment> GenerateAppointmentsForDoctorsOfSameSpecialization(Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {
            List<Appointment> possibleAppointments = new List<Appointment>();
            ObservableCollection<Doctor> possibleDoctors = GetDoctorsWithSameSpecialization(selectedDoctor);
            foreach (var doctor in possibleDoctors)
            {
                possibleAppointments.AddRange(GenerateFreeAppointments(doctor, startDateTime, endDateTime));
            }
            return possibleAppointments;
        }
        private ObservableCollection<Doctor> GetDoctorsWithSameSpecialization(Doctor selectedDoctor)
        {
            ObservableCollection<Doctor> doctors = DoctorController.getInstance().GetDoctors();
            for (int i = 0; i < doctors.Count; i++)
            {
                if (DoctorsAreNotOfSameSpecialization(selectedDoctor, doctors[i]))
                    doctors.RemoveAt(i);
            }
            return doctors;
        }
        private static bool DoctorsAreNotOfSameSpecialization(Doctor doctor1, Doctor doctor2)
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
                    if (existingAppointments[j].Doctor == recommendedAppointments[i].Doctor & existingAppointments[j].StartTime == recommendedAppointments[i].StartTime)
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
                    string s = datesString[i] + "." + " " + timesString[j];
                    dateTimes.Add(DateTime.ParseExact(s, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture));
                }
            }

            return dateTimes;
        }
        private List<string> GetPossibleTimes()
        {
            var timesString = new List<string>();
            timesString.AddRange(new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" , "12:00", "12:30", "13:00", "13:30",
                                                    "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30" , "18:00", "18:30", "19:00", "19:30"
                                                  });
            return timesString;
        }
        private List<string> GetPossibleDates(DateTime startDateTime, DateTime endDateTime)
        {
            var dates = new List<DateTime>();
            var datesString = new List<string>();

            for (var date = startDateTime; date <= endDateTime; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            DatesListToString(dates, datesString);

            return datesString;
        }
        private static void DatesListToString(List<DateTime> dates, List<string> datesString)
        {
            for (int i = 0; i < dates.Count; i++)
            {
                datesString.Add(dates[i].Date.ToString("dd.MM.yyyy"));
            }
        }
        public void RefreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(RecommendAppointments());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
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
    }
}
