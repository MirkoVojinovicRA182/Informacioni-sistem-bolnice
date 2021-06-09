using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for EditPatientAppointment.xaml
    /// </summary>
    public partial class EditPatientAppointmentWindow : Window
    {
        private Appointment appointmentForEditing;
        private PatientAppointmentCRUDOperationsWindow previousWindow;
        public EditPatientAppointmentWindow(Appointment forEditing, PatientAppointmentCRUDOperationsWindow window)
        {
            InitializeComponent();
            this.appointmentForEditing = forEditing;
            previousWindow = window;
            LoadDateComboBox();
            LoadTimeComboBox();
        }

        private void LoadDateComboBox()
        {
            string dateTimeFormat = "dd.MM.yyyy";
            List<string> validDateList = new List<string>();
            validDateList.AddRange(new List<string>() { appointmentForEditing.StartTime.Date.AddDays(-2).ToString(dateTimeFormat), 
                                                        appointmentForEditing.StartTime.Date.AddDays(-1).ToString(dateTimeFormat),
                                                        appointmentForEditing.StartTime.Date.AddDays(1).ToString(dateTimeFormat), 
                                                        appointmentForEditing.StartTime.Date.AddDays(2).ToString(dateTimeFormat) });
            dateComboBox.ItemsSource = null;
            dateComboBox.ItemsSource = validDateList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AttemptToEditAppointment();
            Serialize();
        }

        private void AttemptToEditAppointment()
        {
            DateTime originalTime = appointmentForEditing.StartTime;
            DateTime lastTimeToMoveAppointment = originalTime.AddDays(-1);

            if (IsValidDate(lastTimeToMoveAppointment))
            {
                MoveAppointmentIfNotTaken();
            }
            else
            {
                MessageBox.Show("Prekasno je da se termin pomera", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MoveAppointmentIfNotTaken()
        {
            Appointment movedAppointment = new Appointment();
            movedAppointment.StartTime = ParseDateTime();
            movedAppointment.Doctor = appointmentForEditing.Doctor;

            if (AppointmentIsTaken(movedAppointment))
            {
                MessageBox.Show("Termin je zauzet.", "Greška", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MoveAppointment();
            }
        }

        private void MoveAppointment()
        {
            appointmentForEditing.StartTime = ParseDateTime();
            appointmentForEditing.HasBeenMoved = true;
            appointmentForEditing.MovingTime = DateTime.Now;
            MessageBox.Show("Termin je izmenjen.", "Menjanje termina", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidDate(DateTime lastTimeToChange)
        {
            if (DateTime.Now.CompareTo(lastTimeToChange) < 0)
                return true;
            return false;
        }

        private bool AppointmentIsTaken(Appointment appointment)
        {
            bool isTaken = false;
            for (int i = 0; i < AppointmentController.getInstance().GetAppointments().Count; i++) {
                if (StartTimesAndDoctorsAreEqual(appointment, AppointmentController.getInstance().GetAppointments()[i]))
                {
                    isTaken = true;
                }
            }
            return isTaken;
        }

        private bool StartTimesAndDoctorsAreEqual(Appointment appointment1, Appointment appointment2)
        {
            return appointment1.StartTime == appointment2.StartTime &&
                    appointment1.Doctor == appointment2.Doctor;
        }

        private DateTime ParseDateTime()
        {
            string date = (string)dateComboBox.SelectedItem;
            string time = (string)timeComboBox.SelectedItem;
            string dateTime = date + " " + time;
            return DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        private void LoadTimeComboBox()
        {
            var timesStringList = new List<string>();
            timesStringList.AddRange(new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30" , "12:00", "12:30", "13:00", "13:30",
                                                    "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30" , "18:00", "18:30", "19:00", "19:30"
                                                  });
            timeComboBox.ItemsSource = timesStringList;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PatientMainWindow.GetInstance(appointmentForEditing.Patient).Show();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            previousWindow.Show();
        }
        private static void Serialize()
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
