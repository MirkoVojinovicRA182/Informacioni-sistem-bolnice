using HospitalInformationSystem.Controller;
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
        private Doctor doctor;
        public static DoctorMainWindow GetInstance(Doctor doctor)
        {
            if (instance == null)
                instance = new DoctorMainWindow(doctor);
            return instance;
        }

        private DoctorMainWindow(Doctor doctor)
        {
            InitializeComponent();
            LoadDataFromFiles();
            this.doctor = doctor;
            InitData();
        }
        private void LoadDataFromFiles()
        {
            RoomController.getInstance().loadFromFile();
            DoctorController.getInstance().LoadFromFile();
            PatientController.getInstance().LoadFromFile();
            AppointmentController.getInstance().loadFromFile();
        }
        private void InitData()
        {
            nameLabel.Content = doctor.Name + " " + doctor.Surname;
            if (doctor.Specialization == Specialization.Family_Physician)
                specializationLabel.Content = "Doktor opste prakse";
            else if (doctor.Specialization == Specialization.Gynecologist)
                specializationLabel.Content = "Ginekolog";
            else if (doctor.Specialization == Specialization.Neurologist)
                specializationLabel.Content = "Neurolog";
            else if (doctor.Specialization == Specialization.Pediatrician)
                specializationLabel.Content = "Pediatar";
            else if (doctor.Specialization == Specialization.Surgeon)
                specializationLabel.Content = "Hirurg";
            else
                specializationLabel.Content = "Urolog";

        }

        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.P))
            {
                PatientPreviewWindow window = PatientPreviewWindow.GetInstance(doctor);
                window.Show();
            }
            else if((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.K))
            {
                DoctorAppointmentsManagementWindow window = DoctorAppointmentsManagementWindow.GetInstance(doctor);
                window.Show();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.M))
            {
                MedicinePreviewWindow window = MedicinePreviewWindow.GetInstance();
                window.Show();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Q))
            {
                this.Close();
                instance = null;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoctorController.getInstance().SaveInFlie();
            RoomController.getInstance().saveInFile();
            AppointmentController.getInstance().saveInFile();
            PatientController.getInstance().SaveInFile();
            instance = null;
        }
    }
}
