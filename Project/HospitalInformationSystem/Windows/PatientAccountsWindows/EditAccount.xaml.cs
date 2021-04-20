using HospitalInformationSystem.Windows.Patient;
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
            nameTxt.Text = p.Name;
            surnameTxt.Text = p.Surname;
        }

        private void otkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void izmeniBtn_Click(object sender, RoutedEventArgs e)
        {
            tempPatient.Name = nameTxt.Text;
            tempPatient.Surname = surnameTxt.Text;
            ParentWindow.RefreshList();

        }

    }
}
