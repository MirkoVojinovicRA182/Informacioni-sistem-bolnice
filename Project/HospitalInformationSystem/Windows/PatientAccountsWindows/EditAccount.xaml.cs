using System;
using System.Linq;
using System.Windows;

namespace HospitalInformationSystem.Windows.PatientAccountsWindows
{
    /// <summary>
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        public MainPatientManagement ParentWindow { get; set; }
        private Model.Patient tempPatient = null;
        public EditAccount(Model.Patient p)
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
            tempPatient.Blood = (Model.Patient.BloodType)bloodCmb.SelectedItem;
            tempPatient.LBO = lboTxt.Text;
            tempPatient.IsGuest = (bool)isGuestCheckbox.IsChecked;

            ParentWindow.RefreshList();

        }

        private void bloodCmb_Loaded(object sender, RoutedEventArgs e)
        {
            bloodCmb.ItemsSource = Enum.GetValues(typeof(Model.Patient.BloodType)).Cast<Model.Patient.BloodType>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditAllergens editAllergens = new EditAllergens(tempPatient);
            editAllergens.Show();
        }

        private void editAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAppointmentCRUDOperationsWindow window = new PatientAppointmentCRUDOperationsWindow();
            window.Show();
        }
    }
}
