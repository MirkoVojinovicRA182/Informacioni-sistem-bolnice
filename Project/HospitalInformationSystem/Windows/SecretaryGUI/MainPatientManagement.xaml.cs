using HospitalInformationSystem.Controller;
using Model;
using System.Windows;
using System.Windows.Controls;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for MainPatientManagement.xaml
    /// </summary>
    public partial class MainPatientManagement : Window
    {
        public MainPatientManagement()
        {
            InitializeComponent();
            accountsList.ItemsSource = PatientController.getInstance().getPatient();
            izmeniBtn.IsEnabled = false;
            obrisiBtn.IsEnabled = false;   
        }

        public void RefreshList()
        {
            accountsList.Items.Refresh();
        }

        private void accountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dodajButton_Click(object sender, RoutedEventArgs e)
        {
            AddAccount addAccount = new AddAccount();
            addAccount.ParentWindow = this;
            addAccount.ShowDialog();
        }

        private void obrisiButton_Click(object sender, RoutedEventArgs e)
        {
            PatientController.getInstance().RemovePatient((Patient)accountsList.SelectedItem);
            RefreshList();
        }

        private void izmeniButton_Click(object sender, RoutedEventArgs e)
        {
            if(accountsList.SelectedValue != null) { 
                EditAccount editAccount = new EditAccount((Patient)accountsList.SelectedItem);
                editAccount.ParentWindow = this;
                editAccount.ShowDialog();
            }
        }

        private void accountsList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            izmeniBtn.IsEnabled = true;
            obrisiBtn.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (accountsList.SelectedIndex >= 0)
            {
                AddAllergen addAllergen = new AddAllergen((Patient)accountsList.SelectedItem);
                addAllergen.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PatientController.getInstance().SaveInFile();
        }
    }
}
