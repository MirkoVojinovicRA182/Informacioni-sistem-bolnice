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
        public MainPatientManagement ParentWindow { get; set; }
        private Patient tempPatient = null;
        public EditAccount(Patient p)
        {
            InitializeComponent();
            tempPatient = p;
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
        }

        private void otkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void izmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            tempPatient.Username = usernameTxt.Text;
            tempPatient.Name = nameTxt.Text;
            tempPatient.Surname = surnameTxt.Text;
            tempPatient.DateOfBirth = (DateTime)date.SelectedDate;
            tempPatient.PhoneNumber = numberTxt.Text;
            tempPatient.ParentsName = parentsNameTxt.Text;
            tempPatient.Email = emailTxt.Text;
            tempPatient.Gender = genderTxt.Text;
            tempPatient.Jmbg = jmbgTxt.Text;
            tempPatient.Blood = (Patient.BloodType)bloodCmb.SelectedItem;
            tempPatient.LBO = lboTxt.Text;
            tempPatient.IsGuest = (bool)isGuestCheckbox.IsChecked;

            ParentWindow.RefreshList();

        }

        private void bloodCmb_Loaded(object sender, RoutedEventArgs e)
        {
            bloodCmb.ItemsSource = Enum.GetValues(typeof(Patient.BloodType)).Cast<Patient.BloodType>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAllergens editAllergens = new EditAllergens(tempPatient);
            editAllergens.Show();
        }

        private void editAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow(tempPatient);
            window.Show();
        }
    }
}
