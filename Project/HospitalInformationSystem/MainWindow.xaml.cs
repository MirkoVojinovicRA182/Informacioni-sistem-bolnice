using HospitalInformationSystem.Windows.ManagerGUI;
using HospitalInformationSystem.Windows.DoctorGUI;
using Model;
using System.Linq;
using System.Windows;
using HospitalInformationSystem.Repository;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Windows.PatientGUI;
using HospitalInformationSystem.Windows.SecretaryGUI;
using HospitalInformationSystem.Model;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Person person;
        PatientsRepository savePatients = new PatientsRepository();
 
        public MainWindow()
        {
            InitializeComponent();
            savePatients.loadFromFile();
            AppointmentController.getInstance().loadFromFile();
            DoctorController.getInstance().loadFromFile();


            /*var doctor = new Doctor("Marko", "Markovic", Specialization.Family_Physician, new Room(1, "Markova kancelarija", 1, TypeOfRoom.ExaminationRoom));
            var doctor2 = new Doctor("Jovan", "Jovanovic", Specialization.Family_Physician, new Room(2, "Jovanova prostorija", 2, TypeOfRoom.ExaminationRoom));
            var doctor3 = new Doctor("Stevan", "Stojanovic", Specialization.Family_Physician, new Room(3, "Stevanova prostorija", 3, TypeOfRoom.ExaminationRoom));
            DoctorController.getInstance().addDoctor(doctor);
            DoctorController.getInstance().addDoctor(doctor2);
            DoctorController.getInstance().addDoctor(doctor3);*/
            //var doctor = new Doctor("Darko", "Ilic", Specialization.Surgeon, new Room(1, "Darkova kancelarija", 4, TypeOfRoom.ExaminationRoom));
            //DoctorController.getInstance().addDoctor(doctor);

            Patient first = new Patient("Pera", "Pacijent", new PatientActivity(0, 0, 0, false));
            Patient second = new Patient("Jova", "Pacijent", "2");
            Patient third = new Patient("Mika", "Pacijent", "3");

            MedicalRecord firstMedicalRecord = new MedicalRecord(1);
            MedicalRecord secondMedicalRecord = new MedicalRecord(2);
            MedicalRecord thirdMedicalRecord = new MedicalRecord(3);

            first.setMedicalRecord(firstMedicalRecord);
            second.setMedicalRecord(secondMedicalRecord);
            third.setMedicalRecord(thirdMedicalRecord);

            PatientController.getInstance().getPatient().Add(first);
            PatientController.getInstance().getPatient().Add(second);
            PatientController.getInstance().getPatient().Add(third);

            Secretary secretary = new Secretary();
            secretary.Name = "Petar";
            secretary.Surname = "Petrovic";
            //secretary.Id = "12";

            Manager manager = new Manager();
            manager.Name = "Stefan";
            manager.Surname = "Jovanovic";
            //manager.Id = "52";

            AccountDataBase.getInstance().AddAccount(new Account("perapacijent1@yahoo.com", "pass", first));
            AccountDataBase.getInstance().AddAccount(new Account("markomarkovic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[0]));
            AccountDataBase.getInstance().AddAccount(new Account("jovanjovanovic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[1]));
            AccountDataBase.getInstance().AddAccount(new Account("stevanstojanovic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[2]));
            AccountDataBase.getInstance().AddAccount(new Account("darkoilic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[3]));
            AccountDataBase.getInstance().AddAccount(new Account("petarpetrovic@yahoo.com", "pass", secretary));
            AccountDataBase.getInstance().AddAccount(new Account("m", "pass", manager));

            ManagerMainWindow.getInstance().Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            Secretary secretary = new Secretary();
            bool loggedIn = false;
            var accounts = AccountDataBase.getInstance().GetAccount();
            for (int i = 0; i < accounts.Count; i++)
            {
                if (usernameTextBox.Text.TrimEnd() == accounts[i].Username & passwordTextBox.Password.TrimEnd() == accounts[i].Password)
                {
                    if (accounts[i].Person.GetType() == manager.GetType())
                    {
                        loggedIn = true;
                        ManagerMainWindow window = ManagerMainWindow.getInstance();
                        window.Show();
                        //this.Hide();
                    }
                    else if (accounts[i].Person.GetType() == secretary.GetType())
                    {
                        loggedIn = true;
                        MainPatientManagement window = new MainPatientManagement();
                        window.Show();
                        //this.Hide();
                    }
                    else if (accounts[i].Person.GetType() == DoctorController.getInstance().getDoctors().First().GetType())
                    {
                        loggedIn = true;
                        person = accounts[i].Person;
                        DoctorAppointmentsManagementWindow window = DoctorAppointmentsManagementWindow.GetInstance((Doctor)person);
                        window.Show();
                        //this.Hide();
                    }
                    else
                    {
                        loggedIn = true;
                        PatientAppointmentCRUDOperationsWindow window = PatientAppointmentCRUDOperationsWindow.getInstance((Patient)accounts[i].Person);
                        person = accounts[i].Person;
                        window.Show();
                        //this.Hide();
                    }
                }
            }
            if (!loggedIn)
            {
                MessageBox.Show("Lozinka ili korisničko ime nisu validni.", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
                passwordTextBox.Clear();
            }
        }

        public static Person GetUser()
        {
            return person;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            savePatients.saveInFile();
        }
    }
}
