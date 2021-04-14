using HospitalInformationSystem.Windows;
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

        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentsManagementWindow doctorAppManagementWindow = new DoctorAppointmentsManagementWindow();
            PatientManagementWindow patientManagementWindow = new PatientManagementWindow();
            PatientAppointmentCRUDOperationsWindow patientAppointmentCRUDOperationsWindow = new PatientAppointmentCRUDOperationsWindow();



            if ((bool)roomRadioButton.IsChecked)
                RoomCRUDOperationsWindow.getInstance().Show();
            else if ((bool)patientAppointmentsRadioButton.IsChecked)
                patientAppointmentCRUDOperationsWindow.Show();
            else if ((bool)doctorAppointmentsRadioButton.IsChecked)
                doctorAppManagementWindow.Show();
            else if ((bool)patientRadioButton.IsChecked)
                patientManagementWindow.Show();
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
