using BusinessLogic;
using System;
using System.Linq;
using System.Windows;
using static Model.Patient;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        public MainPatientManagement ParentWindow { get; set; }

        public AddAccount()
        {
            InitializeComponent();
        }

        private void potvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(nameTxt.Text + surnameTxt.Text);

            PatientManagement.CreatePatient(usernameTxt.Text, nameTxt.Text, surnameTxt.Text, (DateTime)date.SelectedDate, numberTxt.Text,
                emailTxt.Text, parentsNameTxt.Text, genderTxt.Text, jmbgTxt.Text, (bool)isGuestCheckbox.IsChecked, (BloodType)bloodCmb.SelectedItem, lboTxt.Text);

            //Console.WriteLine(nameTxt.Text + surnameTxt.Text + idTxt.Text);
            //Console.WriteLine(Model.PatientDataBase.getInstance().GetPatient());
            ParentWindow.RefreshList();
            Close();

        }

        private void otkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bloodCmb_Loaded(object sender, RoutedEventArgs e)
        {

            bloodCmb.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
        }
    }
}
