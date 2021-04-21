using HospitalInformationSystem.Windows.PatientAccountsWindows;
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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for MainPatientManagement.xaml
    /// </summary>
    public partial class MainPatientManagement : Window
    {
        public MainPatientManagement()
        {
            InitializeComponent();
            accountsList.ItemsSource = Model.PatientDataBase.getInstance().getPatient();
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
            Model.PatientDataBase.getInstance().RemovePatient((Model.Patient)accountsList.SelectedItem);
            RefreshList();
        }

        private void izmeniButton_Click(object sender, RoutedEventArgs e)
        {
            if(accountsList.SelectedValue != null) { 
                EditAccount editAccount = new EditAccount((Model.Patient)accountsList.SelectedItem);
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
            AddAllergen addAllergen = new AddAllergen();
            addAllergen.ShowDialog();
        }
    }
}
