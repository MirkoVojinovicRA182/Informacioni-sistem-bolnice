using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using HospitalInformationSystem.Controller;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Doctor doctor;
        private Room room;
        private TypeOfAppointment typeOfAppointment;

        public Window2(Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
            doctorTextBox.Text = doctor.Name + " " + doctor.Surname;
            patientComboBox.ItemsSource = PatientController.getInstance().getPatient();
            roomsListBox.DataContext = RoomController.getInstance().getRooms();
            initTypeOfAppointment();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                if (String.Compare((string)appointmentComboBox.SelectedItem, "Operacija") == 0)
                {
                    room = (Room)roomsListBox.SelectedItem;
                    if (!CheckRoomState(room))
                    {
                        MessageBox.Show("Prostorija je zauzeta u datom terminu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                createAppointment();
                MessageBox.Show("Termin je uspesno zakazan", "Novi Termin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool CheckRoomState(Room room)
        {
            DateTime dateOfAppointment = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return dateOfAppointment.AddMinutes(30) < room.RoomRenovationState.StartDate || dateOfAppointment > room.RoomRenovationState.EndDate;
        }

        private void roomsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            room = (Room)roomsListBox.SelectedItem;
        }
        private void createAppointment()
        {
            DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            Appointment appointment = new Appointment(date, typeOfAppointment, room, (Patient)patientComboBox.SelectedItem, doctor);
            AppointmentController.getInstance().addAppointment(appointment);
            appointment.GetDoctor().AddAppointment(appointment);
            Patient patient = (Patient)patientComboBox.SelectedItem;
            patient.AddAppointment(appointment);


        }


        private void initTypeOfAppointment()
        {
            if(doctor.Specialization != Specialization.Family_Physician)
                appointmentComboBox.Items.Add("Operacija");
            appointmentComboBox.Items.Add("Pregled");
        }

        private void appointmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (String.Compare((string)appointmentComboBox.SelectedItem, "Operacija") == 0)
            {
                roomLabel.Visibility = System.Windows.Visibility.Visible;
                roomsListBox.Visibility = System.Windows.Visibility.Visible;
                typeOfAppointment = TypeOfAppointment.Operacija;
            }
            else
            {
                roomLabel.Visibility = System.Windows.Visibility.Hidden;
                roomsListBox.Visibility = System.Windows.Visibility.Hidden;
                room = doctor.room;
                typeOfAppointment = TypeOfAppointment.Pregled;
            }

        }

        private Boolean checkData()
        {
            if (patientComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati pacijenta!", "Pacijent", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum ili vreme!", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (appointmentComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati vrstu termina!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (appointmentComboBox.SelectedItem.Equals("Operacija") && roomsListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Morate odabrati prostoriju!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            DateTime date1 = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            foreach (Appointment appointment in doctor.GetAppointment())
            {
                if (date1.Ticks > appointment.StartTime.AddMinutes(-30).Ticks && date1.Ticks < appointment.StartTime.AddMinutes(30).Ticks)
                {
                    MessageBox.Show("Imate vec zakazan termin u odabranom vremenu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            Patient patient = (Patient)patientComboBox.SelectedItem;
            foreach (Appointment appointment in patient.GetAppointment())
            {
                if (date1.Ticks > appointment.StartTime.AddMinutes(-30).Ticks && date1.Ticks < appointment.StartTime.AddMinutes(30).Ticks)
                {
                    MessageBox.Show("Pacijent vec ima zakazan termin u odabranom vremenu!", "Termin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }
    }

}
