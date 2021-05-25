using HospitalInformationSystem.Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    public partial class DoctorAppointmentsManagementWindow : Window
    {
        Doctor _loggedDoctor;
        Appointment appointment;
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
            this._loggedDoctor = loggedDoctor;
            RefreshTable();
        }
        public void RefreshTable()
        {
            ObservableCollection<Appointment> loggedDoctorAppointmentsList = new ObservableCollection<Appointment>(
                AppointmentController.getInstance().GetAppointmentsByDoctor(_loggedDoctor));
            appointmentsTable.ItemsSource = null;
            appointmentsTable.ItemsSource = loggedDoctorAppointmentsList;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void OpenNewAppointmentWindow()
        {
            DoctorAddNewAppointmentWindow.GetInstance(_loggedDoctor).ShowDialog();
            RefreshTable();
        }
        private void OpenEditAppointmentWindow()
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                DoctorEditAppointmentWindow.GetInstance(appointment).ShowDialog();
                RefreshTable();
            }
        }
        private void DeleteAppointment()
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                AppointmentController.getInstance().DeleteAppointment(appointment);
                RefreshTable();
            }
        }
        private void OpenAppointmentPreviewWindow()
        {
            if ((Appointment)appointmentsTable.SelectedItem != null)
            {
                appointment = (Appointment)appointmentsTable.SelectedItem;
                DoctorShowAppointmentInformationWindow.GetInstance(appointment, _loggedDoctor).ShowDialog();
                RefreshTable();
            }
        }
        private void ClosingWindow()
        {
            this.Close();
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
