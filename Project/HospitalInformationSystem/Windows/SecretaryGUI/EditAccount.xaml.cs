using HospitalInformationSystem.Windows.PatientGUI;
using Model;
using System;
using System.Linq;
using System.Windows;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        public AccountsPage ParentPage { get; set; }
        private Patient _patient = null;
        public EditAccount(Patient patient)
        {
            InitializeComponent();
            _patient = patient;
            usernameTxt.Text = patient.Username;
            nameTxt.Text = patient.Name;
            surnameTxt.Text = patient.Surname;
            date.SelectedDate = patient.DateOfBirth;
            numberTxt.Text = patient.PhoneNumber;
            parentsNameTxt.Text = patient.ParentsName;
            emailTxt.Text = patient.Email;
            genderTxt.Text = patient.Gender;
            jmbgTxt.Text = patient.Jmbg;
            bloodCmb.SelectedItem = patient.Blood;
            lboTxt.Text = patient.LBO;
            isGuestCheckbox.IsChecked = patient.IsGuest;
        }
        private void OtkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IzmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            _patient.Username = usernameTxt.Text;
            _patient.Name = nameTxt.Text;
            _patient.Surname = surnameTxt.Text;
            _patient.DateOfBirth = (DateTime)date.SelectedDate;
            _patient.PhoneNumber = numberTxt.Text;
            _patient.ParentsName = parentsNameTxt.Text;
            _patient.Email = emailTxt.Text;
            _patient.Gender = genderTxt.Text;
            _patient.Jmbg = jmbgTxt.Text;
            _patient.Blood = (Patient.BloodType)bloodCmb.SelectedItem;
            _patient.LBO = lboTxt.Text;
            _patient.IsGuest = (bool)isGuestCheckbox.IsChecked;
            ParentPage.RefreshList();
        }
        private void BloodCmb_Loaded(object sender, RoutedEventArgs e)
        {
            bloodCmb.ItemsSource = Enum.GetValues(typeof(Patient.BloodType)).Cast<Patient.BloodType>();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAllergens editAllergens = new EditAllergens(_patient);
            editAllergens.Show();
        }
        private void EditAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow(_patient);
            window.Show();
        }
    }
}
