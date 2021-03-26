using HospitalInformationSystem.Windows;
using System.Windows;

namespace HospitalInformationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            RoomManagementWindow roomManagementWindow = new RoomManagementWindow();
            PatientAppointmentsManagementWindow patientAppManagementWindow = new PatientAppointmentsManagementWindow();
            DoctorAppointmentsManagementWindow doctorAppManagementWindow = new DoctorAppointmentsManagementWindow();
            PatientManagementWindow patientManagementWindow = new PatientManagementWindow();

            if ((bool)roomRadioButton.IsChecked)
                roomManagementWindow.Show();
            else if ((bool)patientAppointmentsRadioButton.IsChecked)
                patientAppManagementWindow.Show();
            else if ((bool)doctorAppointmentsRadioButton.IsChecked)
                doctorAppManagementWindow.Show();
            else if ((bool)patientRadioButton.IsChecked)
                patientAppManagementWindow.Show();
            else
                MessageBox.Show("Niste izabrali opciju!", "Greška", MessageBoxButton.OK);

        }
    }
}
