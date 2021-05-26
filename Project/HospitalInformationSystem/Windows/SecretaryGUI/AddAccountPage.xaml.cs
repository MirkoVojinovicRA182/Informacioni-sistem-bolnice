using HospitalInformationSystem.Controller;
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
using static Model.Patient;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for AddAccountPage.xaml
    /// </summary>
    public partial class AddAccountPage : Page
    {
        public AccountsPage ParentPage { get; set; }
        public AddAccountPage(AccountsPage accountsPage)
        {
            InitializeComponent();
            ParentPage = accountsPage;
        }
        private void potvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(nameTxt.Text + surnameTxt.Text);

            PatientController.getInstance().CreatePatient(usernameTxt.Text, nameTxt.Text, surnameTxt.Text, (DateTime)date.SelectedDate, numberTxt.Text,
                emailTxt.Text, parentsNameTxt.Text, genderTxt.Text, jmbgTxt.Text, (bool)isGuestCheckbox.IsChecked, (BloodType)bloodCmb.SelectedItem, lboTxt.Text);

            ParentPage.RefreshList();
        }

        private void OtkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountsPage());
        }

        private void BloodCmb_Loaded(object sender, RoutedEventArgs e)
        {
            bloodCmb.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
        }

    }
}
