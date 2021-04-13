using BusinessLogic;
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
using WorkWithFiles;


using HospitalInformationSystem.BusinessLogic;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for PatientAppointmentCRUDOperationsWindow.xaml
    /// </summary>
    public partial class PatientAppointmentCRUDOperationsWindow : Window
    {
        private ObservableCollection<Appointment> appointmentList;
        PatientsAppointmentsFIleManipulation save = new PatientsAppointmentsFIleManipulation();
        private static PatientAppointmentCRUDOperationsWindow instance = null;
        public PatientAppointmentCRUDOperationsWindow()
        {
            InitializeComponent();
            AppointmentDataGrid.ItemsSource = AppointmentDataBase.getInstance().GetAppointment();
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
        public void RefreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(AppointmentDataBase.getInstance().GetAppointment());
            AppointmentDataGrid.ItemsSource = null;
            AppointmentDataGrid.ItemsSource = appointmentList;
        }

    }
}
