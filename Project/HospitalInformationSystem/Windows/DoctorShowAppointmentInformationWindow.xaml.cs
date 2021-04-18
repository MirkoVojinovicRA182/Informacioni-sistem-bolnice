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

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for DoctorShowAppointmentInformationWindow.xaml
    /// </summary>
    public partial class DoctorShowAppointmentInformationWindow : Window
    {

        Appointment appointment;

        public DoctorShowAppointmentInformationWindow(Appointment appointment)
        {
            InitializeComponent();
            this.appointment = appointment;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        { 

            ShowPatientInformationWindow showPatientInformationWindow = new ShowPatientInformationWindow(appointment.patient);

            showPatientInformationWindow.ShowDialog();

        }
    }
}
