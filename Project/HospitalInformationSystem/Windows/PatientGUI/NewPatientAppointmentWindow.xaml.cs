
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
    /// Interaction logic for NewPatientAppointmentWindow.xaml
    /// </summary>
    public partial class NewPatientAppointmentWindow : Window
    {
        private Patient _patient;
        public NewPatientAppointmentWindow(Patient patient)
        {
            InitializeComponent();
            this._patient = patient;
            LoadDoctorComboBox();
            LoadTimeComboBox();
        }
        private void LoadDoctorComboBox()
        {
            ObservableCollection<Doctor> allDoctors = DoctorController.getInstance().GetDoctors();
            doctorComboBox.ItemsSource = allDoctors;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScheduleNewAppointment();
            Serialize();
            PatientAppointmentCRUDOperationsWindow.getInstance(_patient).RefreshTable();
        }
        
        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPatientAppointmentSystemWindow window = new NewPatientAppointmentSystemWindow(_patient, this);
            this.Hide();
            window.ShowDialog();
        }
        private void ScheduleNewAppointment()
        {
            Appointment newAppointment = CreateAppointment();

            if (AppointmentController.getInstance().AppointmentIsTaken(newAppointment))
            {
                MessageBox.Show("Termin je zauzet.", "Notifikacija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (AppointmentStartTimeHasPassed(newAppointment))
            {
               MessageBox.Show("Termin nije validan.", "Notifikacija", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AppointmentController.getInstance().AddAppointmentToAppointmentList(newAppointment);
                MessageBox.Show("Kreiran je novi termin.", "Novi termin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool AppointmentStartTimeHasPassed(Appointment newAppointment)
        {
            return newAppointment.StartTime.CompareTo(DateTime.Now) <= 0;
        }
        private Appointment CreateAppointment()
        {
            DateTime startDate = (DateTime)datePicker.SelectedDate;
            string startTime = (string)timeComboBox.SelectedItem;
            var hoursAndMinutes = startTime.Split(':');
            DateTime startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, Int32.Parse(hoursAndMinutes[0]), Int32.Parse(hoursAndMinutes[1]), 0);
            Doctor doctor = (Doctor)doctorComboBox.SelectedItem;

            Appointment app = new Appointment(startDateTime, TypeOfAppointment.Pregled, doctor.room, _patient, doctor);
            app.SchedulingTime = DateTime.Now;
            return app;
        }
        private void LoadTimeComboBox()
        {
            var timesStringList = new List<string>();
            timesStringList.AddRange(new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" , "12:00", "12:30", "13:00", "13:30",
                                                    "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30" , "18:00", "18:30", "19:00", "19:30"
                                                  });
            timeComboBox.ItemsSource = timesStringList;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(_patient).Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientAppointmentCRUDOperationsWindow.getInstance(_patient).Show();
        }
        private void DoctorChart_Click(object sender, RoutedEventArgs e)
        {
            DoctorChartWindow window = new DoctorChartWindow((Doctor)doctorComboBox.SelectedItem);
            window.Show();
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
    }
}
