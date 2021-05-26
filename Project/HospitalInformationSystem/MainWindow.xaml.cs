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
using System.Collections.ObjectModel;

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
            Deserialize();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager manager = new Manager();
            Secretary secretary = new Secretary();
            bool loggedIn = false;
            ObservableCollection<Account> accounts = AccountController.GetInstance().GetAllAccounts();
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
                        MainPatientManagement.Instance.Show();
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
        private void Deserialize()
        {
            EquipmentController.getInstance().loadFromFile();
            RoomController.GetInstance().LoadRoomsFromFile();
            MedicineController.GetInstance().LoadFromFile();
            DoctorController.getInstance().LoadFromFile();
            NotificationController.GetInstance().LoadFromFile();
            PatientController.getInstance().loadFromFile();
            AppointmentController.getInstance().LoadAppointmentsFromFile();
            AccountController.GetInstance().LoadFromFile();
        }
        public static void Serialize()
        {
            EquipmentController.getInstance().saveInFile();
            RoomController.GetInstance().SaveRoomsInFile();
            MedicineController.GetInstance().SaveInFile();
            DoctorController.getInstance().SaveInFlie();
            NotificationController.GetInstance().SaveInFile();
            PatientController.getInstance().SaveInFile();
            AppointmentController.getInstance().SaveAppointmentsInFile();
            AccountController.GetInstance().SaveInFile();
        }
    }
}
