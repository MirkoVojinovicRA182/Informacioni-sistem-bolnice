using BusinessLogic;
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


using HospitalInformationSystem.BusinessLogic;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        private Room room;
        private TypeOfAppointment typeOfAppointment;

        public Window2()
        {
            InitializeComponent();

            //initPatients();
            initTypeOfAppointment();
            initDoctors();

            patientComboBox.ItemsSource = PatientDataBase.getInstance().getPatient();
            roomsListBox.DataContext = RoomDataBase.getInstance().getRooms();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (checkData())
            {
                if (String.Compare((string)appointmentComboBox.SelectedItem, "Operacija") == 0)
                    room = (Room)roomsListBox.SelectedItem;

                createAppointment();

                MessageBox.Show("Termin je uspesno zakazan", "Novi Termin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void roomsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            room = (Room)roomsListBox.SelectedItem;
        }
        private void createAppointment()
        {
            DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy. HH:mm", System.Globalization.CultureInfo.InvariantCulture);
           
            AppointmentManagement appointmentsManagment = new AppointmentManagement();
            
            appointmentsManagment.createAppointment(date, typeOfAppointment, room, (Patient)patientComboBox.SelectedItem, (Doctor)doctorComboBox.SelectedItem);
        }


        private void initTypeOfAppointment()
        {
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

                Doctor doctor = (Doctor)doctorComboBox.SelectedItem;

                room = doctor.room;
                typeOfAppointment = TypeOfAppointment.Pregled;
            }

        }

        private void initDoctors()
        {
            doctorComboBox.ItemsSource = DoctorDataBase.getInstance().GetDoctors();
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

            if (doctorComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati lekara!", "Lekara", MessageBoxButton.OK, MessageBoxImage.Error);
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

                return true;
        }
    }

}
