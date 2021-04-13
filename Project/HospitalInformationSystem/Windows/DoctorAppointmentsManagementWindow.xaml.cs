using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentsManagementWindow.xaml
    /// </summary>
    public partial class DoctorAppointmentsManagementWindow : Window
    {
        Appointment appointment;
        private ObservableCollection<Appointment> appointmentList;
        public DoctorAppointmentsManagementWindow()
        {
            InitializeComponent();
            //appointmentsTable.DataContext = AppointmentDataBase.getInstance().GetAppointment();
            refreshTable();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 doctorAddNewAppointmentWindow = new Window2();


            doctorAddNewAppointmentWindow.ShowDialog();
            
            refreshTable();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            appointment = (Appointment)appointmentsTable.SelectedItem;
            
            DoctorEditAppointmentWindow doctorEditAppointmentWindow = new DoctorEditAppointmentWindow(appointment);

            doctorEditAppointmentWindow.ShowDialog();

            refreshTable();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            appointment = (Appointment)appointmentsTable.SelectedItem;

            AppointmentDataBase.getInstance().RemoveAppointment(appointment);

            refreshTable();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void refreshTable()
        {
            appointmentList = new ObservableCollection<Appointment>(AppointmentDataBase.getInstance().GetAppointment());
            appointmentsTable.ItemsSource = null;
            appointmentsTable.ItemsSource = appointmentList;
        }
    }
}
