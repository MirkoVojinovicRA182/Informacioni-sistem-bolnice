﻿using HospitalInformationSystem.Windows.Manager;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkWithFiles;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Person person;
        RoomRepository save = new RoomRepository();
        PatientsFileManipulation savePatients = new PatientsFileManipulation();
        DoctorAppointmentsFIleManipulation doctorAppFile = new DoctorAppointmentsFIleManipulation();
        public MainWindow()
        {
            InitializeComponent();
            savePatients.LoadFromFile();
            //save.LoadFromFile();
            doctorAppFile.LoadFromFile();

            var doctor = new Doctor("Marko", "Markovic", Specialization.Family_Physician, new Room(1, "Markova kancelarija", 1, TypeOfRoom.ExaminationRoom));
            DoctorDataBase.getInstance().GetDoctors().Add(new Doctor("Marko", "Markovic", Specialization.Family_Physician, new Room(1, "Markova prostorija", 1, TypeOfRoom.ExaminationRoom)));
            DoctorDataBase.getInstance().GetDoctors().Add(new Doctor("Jovan", "Jovanovic", Specialization.Family_Physician, new Room(2, "Jovanova prostorija", 2, TypeOfRoom.ExaminationRoom)));
            DoctorDataBase.getInstance().GetDoctors().Add(new Doctor("Stevan", "Stojanovic", Specialization.Family_Physician, new Room(3, "Stevanova prostorija", 3, TypeOfRoom.ExaminationRoom)));

            Patient first = new Patient("Pera", "Pacijent", "1");
            Patient second = new Patient("Jova", "Pacijent", "2");
            Patient third = new Patient("Mika", "Pacijent", "3");

            MedicalRecord firstMedicalRecord = new MedicalRecord(1);
            MedicalRecord secondMedicalRecord = new MedicalRecord(2);
            MedicalRecord thirdMedicalRecord = new MedicalRecord(3);

            first.setMedicalRecord(firstMedicalRecord);
            second.setMedicalRecord(secondMedicalRecord);
            third.setMedicalRecord(thirdMedicalRecord);

            PatientDataBase.getInstance().getPatient().Add(first);
            PatientDataBase.getInstance().getPatient().Add(second);
            PatientDataBase.getInstance().getPatient().Add(third);

            Secretary secretary = new Secretary();
            secretary.Name = "Petar";
            secretary.Surname = "Petrovic";
            //secretary.Id = "12";

            Model.Manager manager = new Model.Manager();
            manager.Name = "Stefan";
            manager.Surname = "Jovanovic";
            //manager.Id = "52";

            AccountDataBase.getInstance().AddAccount(new Account("perapacijent1@yahoo.com", "pass", first));
            AccountDataBase.getInstance().AddAccount(new Account("markomarkovic@yahoo.com", "pass", doctor));
            AccountDataBase.getInstance().AddAccount(new Account("petarpetrovic@yahoo.com", "pass", secretary));
            AccountDataBase.getInstance().AddAccount(new Account("stefanjovanovic@yahoo.com", "pass", manager));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model.Manager manager = new Model.Manager();
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
                    else if (accounts[i].Person.GetType() == DoctorDataBase.getInstance().GetDoctors().First().GetType())
                    {
                        loggedIn = true;
                        DoctorAppointmentsManagementWindow window = new DoctorAppointmentsManagementWindow();
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
            //save.SaveInFile();
            doctorAppFile.SaveInFile();
            savePatients.SaveInFile();
        }
    }
}
