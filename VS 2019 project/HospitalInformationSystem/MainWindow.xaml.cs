using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoomsManagementWindow w1 = new RoomsManagementWindow();
            PatientAppointmentsWindow w2 = new PatientAppointmentsWindow();
            DoctorAppointmentsWindow w3 = new DoctorAppointmentsWindow();
            PatientManagementWindow w4 = new PatientManagementWindow();

            if ((bool)roomRadioButton.IsChecked)
                w1.Show();
            else if ((bool)patientTerminRadioButton.IsChecked)
                w2.Show();
            else if ((bool)doctorTerminRadioButton.IsChecked)
                w3.Show();
            else if ((bool)patientRadioButton.IsChecked)
                w4.Show();
            else
                MessageBox.Show("IZABERITE FUNKCIONALNOST!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
