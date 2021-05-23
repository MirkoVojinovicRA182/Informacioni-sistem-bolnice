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
using System.Collections;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Person person;
 
        public MainWindow()
        {
            InitializeComponent();
            RoomController.GetInstance().LoadRoomsFromFile();
            PatientController.getInstance().LoadFromFile();
            AppointmentController.getInstance().loadFromFile();
            DoctorController.getInstance().LoadFromFile();
            
            /*Room room1 = new Room(1, "Magacin", -1, TypeOfRoom.Magacine, new Hashtable());
            Room room2 = new Room(2, "Prostorija dr Marka", 1, TypeOfRoom.ExaminationRoom, new Hashtable());
            Room room3 = new Room(3, "Prostorija dr Jovana", 1, TypeOfRoom.ExaminationRoom, new Hashtable());
            Room room4 = new Room(4, "Prostorija dr Stevana", 1, TypeOfRoom.ExaminationRoom, new Hashtable());
            Room room5 = new Room(5, "Prostorija dr Darka", 1, TypeOfRoom.ExaminationRoom, new Hashtable());
            RoomController.GetInstance().AddRoomToRoomList(room1);
            RoomController.GetInstance().AddRoomToRoomList(room2);
            RoomController.GetInstance().AddRoomToRoomList(room3);
            RoomController.GetInstance().AddRoomToRoomList(room4);
            RoomController.GetInstance().AddRoomToRoomList(room5);*/

            /*var doctor = new Doctor("Marko", "Markovic", Specialization.Family_Physician, RoomController.GetInstance().GetRooms()[0]);
            var doctor2 = new Doctor("Jovan", "Jovanovic", Specialization.Family_Physician, RoomController.GetInstance().GetRooms()[1]);
            var doctor3 = new Doctor("Stevan", "Stojanovic", Specialization.Family_Physician, RoomController.GetInstance().GetRooms()[2]);
            DoctorController.getInstance().AddDoctor(doctor);
            DoctorController.getInstance().AddDoctor(doctor2);
            DoctorController.getInstance().AddDoctor(doctor3);
            var doctor4 = new Doctor("Darko", "Ilic", Specialization.Surgeon, RoomController.GetInstance().GetRooms()[3]);
            DoctorController.getInstance().AddDoctor(doctor4);
            DoctorController.getInstance().GetDoctors()[0].room = RoomController.GetInstance().GetRooms()[0];
            DoctorController.getInstance().GetDoctors()[1].room = RoomController.GetInstance().GetRooms()[1];
            DoctorController.getInstance().GetDoctors()[2].room = RoomController.GetInstance().GetRooms()[2];
            DoctorController.getInstance().GetDoctors()[3].room = RoomController.GetInstance().GetRooms()[3];

            Patient first = new Patient("Pera", "Pacijent", new PatientActivity(0, 0, 0, false));
            first.Jmbg = "001";
            Patient second = new Patient("Jova", "Pacijent", new PatientActivity(0, 0, 0, false));
            second.Jmbg = "002";
            Patient third = new Patient("Mika", "Pacijent", new PatientActivity(0, 0, 0, false));
            third.Jmbg = "003";

            MedicalRecord firstMedicalRecord = new MedicalRecord(1);
            MedicalRecord secondMedicalRecord = new MedicalRecord(2);
            MedicalRecord thirdMedicalRecord = new MedicalRecord(3);

            first.setMedicalRecord(firstMedicalRecord);
            second.setMedicalRecord(secondMedicalRecord);
            third.setMedicalRecord(thirdMedicalRecord);

            PatientController.getInstance().getPatient().Add(first);
            PatientController.getInstance().getPatient().Add(second);
            PatientController.getInstance().getPatient().Add(third);*/

            Secretary secretary = new Secretary();
            secretary.Name = "Petar";
            secretary.Surname = "Petrovic";
            //secretary.Id = "12";

            Manager manager = new Manager();
            manager.Name = "Stefan";
            manager.Surname = "Jovanovic";
            //manager.Id = "52";


            AccountDataBase.getInstance().AddAccount(new Account("pera", "pass", PatientController.getInstance().getPatient()[0]));
            AccountDataBase.getInstance().AddAccount(new Account("jova", "pass", PatientController.getInstance().getPatient()[1]));
            AccountDataBase.getInstance().AddAccount(new Account("mika", "pass", PatientController.getInstance().getPatient()[2]));
            AccountDataBase.getInstance().AddAccount(new Account("marko", "pass", DoctorController.getInstance().GetDoctors()[0]));
            AccountDataBase.getInstance().AddAccount(new Account("jovan", "pass", DoctorController.getInstance().GetDoctors()[1]));
            AccountDataBase.getInstance().AddAccount(new Account("stevan", "pass", DoctorController.getInstance().GetDoctors()[2]));
            AccountDataBase.getInstance().AddAccount(new Account("darko", "pass", DoctorController.getInstance().GetDoctors()[3]));
            AccountDataBase.getInstance().AddAccount(new Account("petar", "pass", secretary));
            AccountDataBase.getInstance().AddAccount(new Account("u", "pass", manager));

            //ManagerMainWindow.getInstance().Show();

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
                    else if (accounts[i].Person.GetType() == DoctorController.getInstance().GetDoctors().First().GetType())
                    {
                        loggedIn = true;
                        person = accounts[i].Person;
                        DoctorMainWindow window = DoctorMainWindow.GetInstance((Doctor)person);
                        window.Show();
                        //this.Hide();
                    }
                    else
                    {
                        loggedIn = true;
                        Patient rightPatient = null;
                        foreach (var patient in PatientController.getInstance().getPatient())
                        {
                            if (((Patient)accounts[i].Person).Jmbg == patient.Jmbg)
                                rightPatient = patient;
                        }
                        person = accounts[i].Person;
                        PatientMainWindow.GetInstance(rightPatient).Show();
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
            PatientController.getInstance().SaveInFile();
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
            for (int i = 6; i <= 23; i++)
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
