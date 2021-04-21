using BusinessLogic;
using HospitalInformationSystem.BusinessLogic;
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

namespace HospitalInformationSystem.Windows
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

            var database = DoctorDataBase.getInstance();
            this.patient = patient;
            var list = database.GetDoctors();

            DoctorComboBox.ItemsSource = list;

           // initPatients();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppointment();

            dateTextBox.Clear();
            timeTextBox.Clear();

            PatientAppointmentCRUDOperationsWindow.getInstance(patient).RefreshTable();

            MessageBox.Show("Kreiran je novi termin.", "Novi termin", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void New_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPatientAppointmentSystemWindow window = new NewPatientAppointmentSystemWindow();
            window.ShowDialog();
        }

        private void CreateNewAppointment()
        {
            string date = dateTextBox.Text;
            string time = timeTextBox.Text;
            string dateTime = date + " " + time;
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime startTime = DateTime.ParseExact(dateTime, "dd.MM.yyyy. HH:mm", provider);
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;

            AppointmentManagement patientAppointmentManagement = new AppointmentManagement();

            patientAppointmentManagement.createAppointment(startTime, TypeOfAppointment.Pregled, doctor.room, patient, doctor);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
