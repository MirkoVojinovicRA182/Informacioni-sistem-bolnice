using HospitalInformationSystem.Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    public partial class DoctorAppointmentsManagementWindow : Window
    {
        Doctor loggedDoctor;
        Appointment appointment;
        private ObservableCollection<Appointment> loggedDoctorAppointmentsList;

        private static DoctorAppointmentsManagementWindow instance = null;

        public static DoctorAppointmentsManagementWindow GetInstance(Doctor loggedDoctor)
        {
            if (instance == null)
                instance = new DoctorAppointmentsManagementWindow(loggedDoctor);
            return instance;
        }
        private DoctorAppointmentsManagementWindow(Doctor loggedDoctor)
        {
            InitializeComponent();
            this.loggedDoctor = loggedDoctor;
            appointmentsTable.DataContext = loggedDoctor.GetAppointment();
            RefreshTable();
        }

        public void RefreshTable()
        {
            loggedDoctorAppointmentsList = new ObservableCollection<Appointment>(loggedDoctor.GetAppointment());
            appointmentsTable.ItemsSource = null;
            appointmentsTable.ItemsSource = loggedDoctorAppointmentsList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoctorController.getInstance().SaveInFlie();
            AppointmentController.getInstance().saveInFile();
            instance = null;
        }

        private void OpenNewAppointmentWindow()
        {
            Window2 doctorAddNewAppointmentWindow = new Window2(loggedDoctor);
            doctorAddNewAppointmentWindow.ShowDialog();
            RefreshTable();
        }

        private void OpenEditAppointmentWindow()
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                DoctorEditAppointmentWindow doctorEditAppointmentWindow = new DoctorEditAppointmentWindow(appointment);
                doctorEditAppointmentWindow.ShowDialog();
                RefreshTable();
            }
        }

        private void DeleteAppointment()
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                AppointmentController.getInstance().removeAppointment(appointment);
                loggedDoctor.RemoveAppointment(appointment);
                appointment.GetPatient().RemoveAppointment(appointment);
                RefreshTable();
            }
        }

        private void OpenAppointmentPreviewWindow()
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                DoctorShowAppointmentInformationWindow doctorShowAppointmentInformationWindow = new DoctorShowAppointmentInformationWindow(appointment, loggedDoctor);
                doctorShowAppointmentInformationWindow.ShowDialog();
                RefreshTable();
            }
        }

        private void ClosingWindow()
        {
            DoctorController.getInstance().SaveInFlie();
            AppointmentController.getInstance().saveInFile();
            this.Close();
            instance = null;
        }

        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.A))
            {
                OpenNewAppointmentWindow();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.E))
            {
                OpenEditAppointmentWindow();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.D))
            {
                DeleteAppointment();
            }
            else if (Keyboard.IsKeyDown(Key.Enter))
            {
                OpenAppointmentPreviewWindow();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Q))
            {
                ClosingWindow();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void appointmentsTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
