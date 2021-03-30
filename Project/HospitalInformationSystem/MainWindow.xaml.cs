using HospitalInformationSystem.Windows;
using System.Windows;
using WorkWithFiles;

namespace HospitalInformationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RoomsFileManipulation save = new RoomsFileManipulation();
        public MainWindow()
        {
            InitializeComponent();
            save.LoadFromFile();
            
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            save.SaveInFile();
        }
    }
}
