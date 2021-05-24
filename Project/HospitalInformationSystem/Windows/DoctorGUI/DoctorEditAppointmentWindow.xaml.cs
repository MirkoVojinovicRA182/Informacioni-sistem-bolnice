using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorEditAppointmentWindow.xaml
    /// </summary>
    public partial class DoctorEditAppointmentWindow : Window
    {
        private Appointment _appointmentToEdit;
        private static DoctorEditAppointmentWindow instance = null;
        public static DoctorEditAppointmentWindow GetInstance(Appointment appointmentToEdit)
        {
            if (instance == null)
                instance = new DoctorEditAppointmentWindow(appointmentToEdit);
            return instance;
        }
        private DoctorEditAppointmentWindow(Appointment appointmentToEdit)
        {
            InitializeComponent();
            this._appointmentToEdit = appointmentToEdit;
            LoadPatient();
            LoadPatientComboBox();
        }
        private void LoadPatient()
        {
            dateTextBox.Text = _appointmentToEdit.StartTime.ToString("dd.MM.yyyy.");
            timeTextBox.Text = _appointmentToEdit.StartTime.ToString("HH:mm");
        }
        private void LoadPatientComboBox()
        {
            patientListBox.ItemsSource = PatientController.getInstance().getPatient();
            patientListBox.SelectedIndex = 10;
            patientListBox.SelectedItem = _appointmentToEdit.GetPatient();
        }
        private bool CheckData()
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum ili vreme!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        private bool CheckRoomState(Room room)
        {
            DateTime dateOfAppointment = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return dateOfAppointment.AddMinutes(30) < room.RoomRenovationState.StartDate || dateOfAppointment > room.RoomRenovationState.EndDate;
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.E))
            {
                if (CheckData() && CheckRoomState(_appointmentToEdit.room))
                {
                    DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    if(patientListBox.SelectedIndex < 0)
                        AppointmentController.getInstance().ChangeAppointment(
                            _appointmentToEdit, date, _appointmentToEdit.Type, _appointmentToEdit.room, 
                            _appointmentToEdit.patient, _appointmentToEdit.doctor);
                    else
                        AppointmentController.getInstance().ChangeAppointment(
                            _appointmentToEdit, date, _appointmentToEdit.Type, _appointmentToEdit.room, 
                            (Patient)patientListBox.SelectedItem, _appointmentToEdit.doctor);
                    MessageBox.Show("Informacije o terminu su sada izmenjene.", "Izmena informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
            {
                this.Close();
            }
        }
        private void patientListBox_PreviewKeyDown(object sender, KeyEventArgs e) => CheckKeyPress();
        private void Window_KeyDown(object sender, KeyEventArgs e) => CheckKeyPress();
    }
}
