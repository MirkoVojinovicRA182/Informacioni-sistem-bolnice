using HospitalInformationSystem.Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentsManagementWindow.xaml
    /// </summary>
    public partial class DoctorAppointmentsManagementWindow : Window
    {
        Doctor doctor;
        Appointment appointment;
        private ObservableCollection<Appointment> appointmentList;

        private static DoctorAppointmentsManagementWindow instance = null;

        public static DoctorAppointmentsManagementWindow GetInstance(Doctor doctor)
        {
            if (instance == null)
                instance = new DoctorAppointmentsManagementWindow(doctor);
            return instance;
        }
        private DoctorAppointmentsManagementWindow(Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
            appointmentsTable.DataContext = doctor.GetAppointment();
            refreshTable();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 doctorAddNewAppointmentWindow = new Window2(doctor);

            doctorAddNewAppointmentWindow.ShowDialog();
            
            refreshTable();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                DoctorEditAppointmentWindow doctorEditAppointmentWindow = new DoctorEditAppointmentWindow(appointment);
                doctorEditAppointmentWindow.ShowDialog();
                refreshTable();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                AppointmentController.getInstance().removeAppointment(appointment);
                doctor.RemoveAppointment(appointment);
                appointment.GetPatient().RemoveAppointment(appointment);
                refreshTable();
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void refreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(doctor.GetAppointment());
            appointmentsTable.ItemsSource = null;
            appointmentsTable.ItemsSource = appointmentList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                DoctorShowAppointmentInformationWindow doctorShowAppointmentInformationWindow = new DoctorShowAppointmentInformationWindow(appointment, doctor);
                doctorShowAppointmentInformationWindow.ShowDialog();
                refreshTable();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoctorController.getInstance().saveInFlie();
            AppointmentController.getInstance().saveInFile();
            instance = null;
        }

        private void medicineButton_Click(object sender, RoutedEventArgs e)
        {
            MedicinePreviewWindow medicinePreviewWindow = MedicinePreviewWindow.GetInstance();
            medicinePreviewWindow.ShowDialog();
        }
    }
}
