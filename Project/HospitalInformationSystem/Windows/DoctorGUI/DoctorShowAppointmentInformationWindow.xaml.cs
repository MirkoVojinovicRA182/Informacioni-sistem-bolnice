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
        Doctor doctor;
        Appointment appointment;

        public DoctorShowAppointmentInformationWindow(Appointment appointment, Doctor doctor)
        {
            InitializeComponent();
            this.appointment = appointment;
            this.doctor = doctor;
            initTextBoxes();
        }

        private void initTextBoxes()
        {
            dateTextBox.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            timeTextBox.Text = appointment.StartTime.ToString("HH:mm");
            appointmentTypeTextBox.Text = appointment.Type.ToString();
            patientTextBox.Text = appointment.patient.Name + " " +appointment.patient.Surname;
            if(Object.Equals(appointment.Type.ToString(), "Operacija"))
            {
                roomLabel.Visibility = Visibility.Visible;
                roomTextBox.Visibility = Visibility.Visible;
                roomTextBox.Text = appointment.room.Name;
            }
        }

        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                ShowPatientInformationWindow showPatientInformationWindow = ShowPatientInformationWindow.GetInstance(appointment.patient, doctor);
                showPatientInformationWindow.ShowDialog();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
