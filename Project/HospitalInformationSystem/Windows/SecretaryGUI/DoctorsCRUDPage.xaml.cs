using HospitalInformationSystem.Controller;
using Model;
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

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for DoctorsCRUDPage.xaml
    /// </summary>
    public partial class DoctorsCRUDPage : Page
    {
        public DoctorsCRUDPage()
        {
            InitializeComponent();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsGrid.SelectedItem is Doctor)
                DoctorController.getInstance().RemoveDoctor((Doctor)doctorsGrid.SelectedItem);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if(doctorsGrid.SelectedItem is Doctor)
                MainPatientManagement.Instance.MainFrame.Content = new EditDoctorPage((Doctor)doctorsGrid.SelectedItem);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new AddDoctorPage();
        }

        private void doctorsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            doctorsGrid.ItemsSource = DoctorController.getInstance().GetDoctors();
        }
    }
}
