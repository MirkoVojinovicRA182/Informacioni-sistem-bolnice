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
        private List<Model.Patient> initialPatients;
        public NewPatientAppointmentWindow()
        {
            InitializeComponent();

            var database = DoctorDataBase.getInstance();

            var list = database.GetDoctors();

            DoctorComboBox.ItemsSource = list;

            initPatients();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppointment();

            dateTextBox.Clear();
            timeTextBox.Clear();

            PatientAppointmentCRUDOperationsWindow.getInstance().RefreshTable();

            MessageBox.Show("Kreiran je novi termin.", "Novi termin", MessageBoxButton.OK, MessageBoxImage.Information);

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

            patientAppointmentManagement.createAppointment(startTime, TypeOfAppointment.Pregled, doctor.room, (Model.Patient)patientComboBox.SelectedItem, doctor);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void initPatients()
        {
            DoctorComboBox.ItemsSource = DoctorDataBase.getInstance().GetDoctors();
        }

    }
}
