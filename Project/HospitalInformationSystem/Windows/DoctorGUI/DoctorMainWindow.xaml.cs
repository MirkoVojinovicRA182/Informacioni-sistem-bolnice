using Model;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorMainWindow.xaml
    /// </summary>
    public partial class DoctorMainWindow : Window
    {
        private static DoctorMainWindow instance = null;
        private Doctor _loggedDoctor;
        public static DoctorMainWindow GetInstance(Doctor loggedDoctor)
        {
            if (instance == null)
                instance = new DoctorMainWindow(loggedDoctor);
            return instance;
        }
        private DoctorMainWindow(Doctor loggedDoctor)
        {
            InitializeComponent();
            this._loggedDoctor = loggedDoctor;
            InitData();
        }
        private void InitData()
        {
            nameLabel.Content = _loggedDoctor.Name + " " + _loggedDoctor.Surname;
            switch(_loggedDoctor.Specialization)
            {
                case Specialization.Family_Physician:
                    specializationLabel.Content = "Doktor opste prakse";
                    break;
                case Specialization.Gynecologist:
                    specializationLabel.Content = "Ginekolog";
                    break;
                case Specialization.Neurologist:
                    specializationLabel.Content = "Neurolog";
                    break;
                case Specialization.Pediatrician:
                    specializationLabel.Content = "Pediatar";
                    break;
                case Specialization.Surgeon:
                    specializationLabel.Content = "Hirurg";
                    break;
                default:
                    specializationLabel.Content = "Urolog";
                    break;
            }
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.P))
                PatientPreviewWindow.GetInstance(_loggedDoctor).Show();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.K))
                DoctorAppointmentsManagementWindow.GetInstance(_loggedDoctor).Show();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.M))
                MedicinePreviewWindow.GetInstance().Show();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.I))
                CreateReportWindow.GetInstance().Show();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Q))
                this.Close();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.F))
                DoctorFeedback.GetInstance(_loggedDoctor).Show();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) => CheckKeyPress();
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.Serialize();
            instance = null;
        }
    }
}
