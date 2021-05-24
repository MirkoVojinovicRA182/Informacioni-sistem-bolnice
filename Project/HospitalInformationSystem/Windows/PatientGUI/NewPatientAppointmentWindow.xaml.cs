
using HospitalInformationSystem.Controller;
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

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for NewPatientAppointmentWindow.xaml
    /// </summary>
    public partial class NewPatientAppointmentWindow : Window
    {
        private Patient patient;
        public NewPatientAppointmentWindow(Patient patient)
        {
            InitializeComponent();

            this.patient = patient;
            var list = DoctorController.getInstance().GetDoctors();

            DoctorComboBox.ItemsSource = list;
            LoadTimeComboBox(); 

           // initPatients();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppointment();
       
            PatientAppointmentCRUDOperationsWindow.getInstance(patient).RefreshTable();

            MessageBox.Show("Kreiran je novi termin.", "Novi termin", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPatientAppointmentSystemWindow window = new NewPatientAppointmentSystemWindow(patient, this);
            this.Hide();
            window.ShowDialog();
        }

        private void CreateNewAppointment()
        {
            DateTime startDate = (DateTime)datePicker.SelectedDate;
            string startTime = (string)timeComboBox.SelectedItem;
            var hoursAndMinutes = startTime.Split(':');
            DateTime startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, Int32.Parse(hoursAndMinutes[0]), Int32.Parse(hoursAndMinutes[1]), 0);
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;

            Appointment app = new Appointment(startDateTime, TypeOfAppointment.Pregled, doctor.room, patient, doctor);
            app.SchedulingTime = DateTime.Now;

            AppointmentController.getInstance().AddAppointmentToAppointmentList(app);
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
            PatientMainWindow.GetInstance(patient).Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientAppointmentCRUDOperationsWindow.getInstance(patient).Show();
        }

        /*private void initPatients()
        {
            initialPatients = new List<Patient>();

            Patient first = new Patient("Pera", "Pacijent 1", "1");
            Patient second = new Patient("Jova", "Pacijent 2", "2");
            Patient third = new Patient("Mika", "Pacijent 3", "3");

            initialPatients.Add(first);
            initialPatients.Add(second);
            initialPatients.Add(third);


            patientComboBox.ItemsSource = initialPatients;
        }*/

    }
}
