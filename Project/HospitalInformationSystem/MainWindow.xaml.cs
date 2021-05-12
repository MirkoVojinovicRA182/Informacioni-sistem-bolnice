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
using System;
using System.Collections.Generic;

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
            /*Room room1 = new Room(2, "Prostorija dr Marka", 1, TypeOfRoom.ExaminationRoom);
            Room room2 = new Room(3, "Prostorija dr Jovana", 1, TypeOfRoom.ExaminationRoom);
            Room room3 = new Room(4, "Prostorija dr Stevana", 1, TypeOfRoom.ExaminationRoom);
            Room room4 = new Room(5, "Prostorija dr Darka", 1, TypeOfRoom.ExaminationRoom);
            RoomController.getInstance().AddRoomToRoomList(room1);
            RoomController.getInstance().AddRoomToRoomList(room2);
            RoomController.getInstance().AddRoomToRoomList(room3);
            RoomController.getInstance().AddRoomToRoomList(room4);*/
            RoomController.GetInstance().loadFromFile();
            savePatients.loadFromFile();
            AppointmentController.getInstance().loadFromFile();
            DoctorController.getInstance().loadFromFile();

            /*var doctor = new Doctor("Marko", "Markovic", Specialization.Family_Physician, RoomController.getInstance().getRooms()[0]);
            var doctor2 = new Doctor("Jovan", "Jovanovic", Specialization.Family_Physician, RoomController.getInstance().getRooms()[1]);
            var doctor3 = new Doctor("Stevan", "Stojanovic", Specialization.Family_Physician, RoomController.getInstance().getRooms()[2]);
            DoctorController.getInstance().addDoctor(doctor);
            DoctorController.getInstance().addDoctor(doctor2);
            DoctorController.getInstance().addDoctor(doctor3);
            var doctor4 = new Doctor("Darko", "Ilic", Specialization.Surgeon, RoomController.getInstance().getRooms()[3]);
            DoctorController.getInstance().addDoctor(doctor4);*/
            DoctorController.getInstance().getDoctors()[0].room = RoomController.GetInstance().getRooms()[0];
            DoctorController.getInstance().getDoctors()[1].room = RoomController.GetInstance().getRooms()[1];
            DoctorController.getInstance().getDoctors()[2].room = RoomController.GetInstance().getRooms()[2];
            DoctorController.getInstance().getDoctors()[3].room = RoomController.GetInstance().getRooms()[3];

            Patient first = new Patient("Pera", "Pacijent", new PatientActivity(0, 0, 0, false));
            Patient second = new Patient("Jova", "Pacijent", new PatientActivity(0, 0, 0, false));
            Patient third = new Patient("Mika", "Pacijent", new PatientActivity(0, 0, 0, false));

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
            AccountDataBase.getInstance().AddAccount(new Account("jovapacijent@yahoo.com", "pass", second));
            AccountDataBase.getInstance().AddAccount(new Account("mikapacijent@yahoo.com", "pass", third));
            AccountDataBase.getInstance().AddAccount(new Account("markomarkovic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[0]));
            AccountDataBase.getInstance().AddAccount(new Account("jovanjovanovic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[1]));
            AccountDataBase.getInstance().AddAccount(new Account("stevanstojanovic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[2]));
            AccountDataBase.getInstance().AddAccount(new Account("darkoilic@yahoo.com", "pass", DoctorController.getInstance().getDoctors()[3]));
            AccountDataBase.getInstance().AddAccount(new Account("petarpetrovic@yahoo.com", "pass", secretary));
            AccountDataBase.getInstance().AddAccount(new Account("m", "pass", manager));

            ManagerMainWindow.getInstance().ShowDialog();
            this.Close();

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

        public static List<String> GetTimeList()
        {
            List<String> timeList = new List<String>();
            foreach (string hour in GetHourList())
            {
                foreach (string minute in GetMinuteList())
                {
                    timeList.Add(hour + ":" + minute);
                }
            }
            return timeList;
        }
        public static List<String> GetHourList()
        {
            List<String> hourList = new List<String>();
            for (int i = 6; i <= 21; i++)
            {
                hourList.Add(GetHour(i));
            }
            return hourList;
        }
        public static string GetHour(int currentHour)
        {
            if (currentHour >= 6 && currentHour <= 9)
                return "0" + currentHour.ToString();
            else
                return currentHour.ToString();
        }
        public static List<String> GetMinuteList()
        {
            List<String> minuteList = new List<String>();
            for (int i = 0; i <= 59; i++)
            {
                minuteList.Add(GetMinute(i));
            }
            return minuteList;
        }
        public static string GetMinute(int currentMinute)
        {
            if (currentMinute >= 0 && currentMinute <= 9)
                return "0" + currentMinute.ToString();
            else
                return currentMinute.ToString();
        }
    }
}
