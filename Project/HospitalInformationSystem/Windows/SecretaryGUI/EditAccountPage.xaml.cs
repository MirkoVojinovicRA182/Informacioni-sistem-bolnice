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
    /// Interaction logic for EditAccountPage.xaml
    /// </summary>
    public partial class EditAccountPage : Page
    {
        public AccountsPage ParentPage { get; set; }
        private Patient _tempPatient = null;
        public EditAccountPage(Patient p, AccountsPage parent)
        {
            InitializeComponent();
            _tempPatient = p;
            usernameTxt.Text = p.Username;
            nameTxt.Text = p.Name;
            surnameTxt.Text = p.Surname;
            date.SelectedDate = p.DateOfBirth;
            numberTxt.Text = p.PhoneNumber;
            parentsNameTxt.Text = p.ParentsName;
            emailTxt.Text = p.Email;
            genderTxt.Text = p.Gender;
            jmbgTxt.Text = p.Jmbg;
            bloodCmb.SelectedItem = p.Blood;
            lboTxt.Text = p.LBO;
            isGuestCheckbox.IsChecked = p.IsGuest;
            ParentPage = parent;
        }

        private void OtkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new AccountsPage();
        }


        private void izmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            _tempPatient.Username = usernameTxt.Text;
            _tempPatient.Name = nameTxt.Text;
            _tempPatient.Surname = surnameTxt.Text;
            _tempPatient.DateOfBirth = (DateTime)date.SelectedDate;
            _tempPatient.PhoneNumber = numberTxt.Text;
            _tempPatient.ParentsName = parentsNameTxt.Text;
            _tempPatient.Email = emailTxt.Text;
            _tempPatient.Gender = genderTxt.Text;
            _tempPatient.Jmbg = jmbgTxt.Text;
            _tempPatient.Blood = (Patient.BloodType)bloodCmb.SelectedItem;
            _tempPatient.LBO = lboTxt.Text;
            _tempPatient.IsGuest = (bool)isGuestCheckbox.IsChecked;

            ParentPage.RefreshList();

        }

        private void bloodCmb_Loaded(object sender, RoutedEventArgs e)
        {
            bloodCmb.ItemsSource = Enum.GetValues(typeof(Patient.BloodType)).Cast<Patient.BloodType>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new EditAllergensPage(_tempPatient);
        }

        private void editAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new EditAppointmentPage(_tempPatient);
        }
    }
}
