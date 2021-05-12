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
    /// Interaction logic for AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : Page
    {
        public MainPatientManagement MainWindow { get; set; }
        public AccountsPage(MainPatientManagement mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            accountsList.ItemsSource = PatientController.getInstance().getPatient();
            //izmeniBtn.IsEnabled = false;
            //obrisiBtn.IsEnabled = false;
        }

        public void RefreshList()
        {
            accountsList.Items.Refresh();
        }

        private void accountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("LALALA");
            izmeniBtn.IsEnabled = false;
            obrisiBtn.IsEnabled = false;
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrame.Content = new AddAccountPage(this);
        }

        private void ObrisiButton_Click(object sender, RoutedEventArgs e)
        {
            PatientController.getInstance().RemovePatient((Patient)accountsList.SelectedItem);
            RefreshList();
        }

        private void IzmeniButton_Click(object sender, RoutedEventArgs e)
        {
            if (accountsList.SelectedValue != null)
            {
                MainWindow.MainFrame.Content = new EditAccountPage((Patient)accountsList.SelectedItem, this);
            }
        }
    }
}
