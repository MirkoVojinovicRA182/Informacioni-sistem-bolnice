using HospitalInformationSystem.Windows;
using HospitalInformationSystem.Windows.Manager;
using Model;
using System;
using System.Windows;
using WorkWithFiles;

namespace HospitalInformationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RoomsFileManipulation save = new RoomsFileManipulation();
        DoctorAppointmentsFIleManipulation doctorAppFile = new DoctorAppointmentsFIleManipulation();
        public MainWindow()
        {
            InitializeComponent();
            //save.LoadFromFile();
            //doctorAppFile.LoadFromFile();

            //inicijalizacija tri rucno uneta doktora

            DoctorDataBase.getInstance().GetDoctors().Add(new Doctor("Marko", "Markovic", Specialization.Family_Physician, new Room(1, "Markova kancelarija", 1, TypeOfRoom.ExaminationRoom)));
            DoctorDataBase.getInstance().GetDoctors().Add(new Doctor("Jovan", "Jovanovic", Specialization.Family_Physician, new Room(2, "Jovanova kancelarija", 2, TypeOfRoom.ExaminationRoom)));
            DoctorDataBase.getInstance().GetDoctors().Add(new Doctor("Stevan", "Stojanovic", Specialization.Family_Physician, new Room(3, "Stevanova kancelarija", 3, TypeOfRoom.ExaminationRoom)));

            Patient first = new Patient("Pera", "Peric", "1");
            Patient second = new Patient("Jova", "Jovic", "2");
            Patient third = new Patient("Mika", "Mikic", "3");

            MedicalRecord firstMedicalRecord = new MedicalRecord(1);
            MedicalRecord secondMedicalRecord = new MedicalRecord(2);
            MedicalRecord thirdMedicalRecord = new MedicalRecord(3);

            first.setMedicalRecord(firstMedicalRecord);
            second.setMedicalRecord(secondMedicalRecord);
            third.setMedicalRecord(thirdMedicalRecord);

            PatientDataBase.getInstance().AddPatient(first);
            PatientDataBase.getInstance().AddPatient(second);
            PatientDataBase.getInstance().AddPatient(third);

        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentsManagementWindow doctorAppManagementWindow = new DoctorAppointmentsManagementWindow();
            MainPatientManagement mainPatientManagement = new MainPatientManagement();
            PatientAppointmentCRUDOperationsWindow patientAppointmentCRUDOperationsWindow = new PatientAppointmentCRUDOperationsWindow();



            if ((bool)roomRadioButton.IsChecked)
                ManagerMainWindow.getInstance().Show();
            else if ((bool)patientAppointmentsRadioButton.IsChecked)
                patientAppointmentCRUDOperationsWindow.Show();
            else if ((bool)doctorAppointmentsRadioButton.IsChecked)
                doctorAppManagementWindow.Show();
            else if ((bool)patientRadioButton.IsChecked)
                mainPatientManagement.Show();
            else
                MessageBox.Show("Niste izabrali opciju!", "Greška", MessageBoxButton.OK);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            save.SaveInFile();
            doctorAppFile.SaveInFile();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
