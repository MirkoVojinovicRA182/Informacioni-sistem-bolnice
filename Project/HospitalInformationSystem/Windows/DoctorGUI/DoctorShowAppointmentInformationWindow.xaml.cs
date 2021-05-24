using Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorShowAppointmentInformationWindow.xaml
    /// </summary>
    public partial class DoctorShowAppointmentInformationWindow : Window
    {
        private Doctor _loggedDoctor;
        private Appointment _appointmentToShow;
        private static DoctorShowAppointmentInformationWindow instance = null;
        public static DoctorShowAppointmentInformationWindow GetInstance(Appointment appointmentToShow, Doctor loggedDoctor)
        {
            if (instance == null)
                instance = new DoctorShowAppointmentInformationWindow(appointmentToShow, loggedDoctor);
            return instance;
        }
        private DoctorShowAppointmentInformationWindow(Appointment appointmentToShow, Doctor loggedDoctor)
        {
            InitializeComponent();
            this._appointmentToShow = appointmentToShow;
            this._loggedDoctor = loggedDoctor;
            InitTextBoxes();
        }
        private void InitTextBoxes()
        {
            dateTextBox.Text = _appointmentToShow.StartTime.ToString("dd.MM.yyyy.");
            timeTextBox.Text = _appointmentToShow.StartTime.ToString("HH:mm");
            appointmentTypeTextBox.Text = _appointmentToShow.Type.ToString();
            patientTextBox.Text = _appointmentToShow.patient.Name + " " + _appointmentToShow.patient.Surname;
            if(Object.Equals(_appointmentToShow.Type.ToString(), "Operacija"))
            {
                roomLabel.Visibility = Visibility.Visible;
                roomTextBox.Visibility = Visibility.Visible;
                roomTextBox.Text = _appointmentToShow.room.Name;
            }
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                ShowPatientInformationWindow.GetInstance(_appointmentToShow.patient).ShowDialog();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) => CheckKeyPress();
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => instance = null;
    }
}
