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


using Model;
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

        private List<Patient> initialPatients;
        public Window2()
        {
            InitializeComponent();

            roomsListBox.DataContext = RoomDataBase.getInstance().getRooms();
            initPatients();
            initTypeOfAppointment();
            initDoctors();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createAppointment();

            MessageBox.Show("Termin je uspesno zakazan", "Novi Termin", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void roomsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            room = (Room)roomsListBox.SelectedItem;
        }
        private void createAppointment()
        {

            DateTime date = DateTime.ParseExact(dateTextBox.Text + " " + timeTextBox.Text, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            AppointmentManagement appointmentsManagment = new AppointmentManagement();

            appointmentsManagment.createAppointment(date, typeOfAppointment, room, (Patient)patientComboBox.SelectedItem, (Doctor)doctorComboBox.SelectedItem);
        }

        private void initPatients()
        {
            initialPatients = new List<Patient>();

            Patient first = new Patient("Pera", "Pacijent 1", "1");
            Patient second = new Patient("Jova", "Pacijent 2", "2");
            Patient third = new Patient("Mika", "Pacijent 3", "3");

            initialPatients.Add(first);
            initialPatients.Add(second);
            initialPatients.Add(third);


            patientComboBox.ItemsSource = initialPatients;
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

    }

}
