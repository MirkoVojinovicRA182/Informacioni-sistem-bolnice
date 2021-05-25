using HospitalInformationSystem.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for EditDoctor.xaml
    /// </summary>
    public partial class EditDoctorPage : Page
    {
        private Doctor _doctor;
        public EditDoctorPage()
        {
            InitializeComponent();
        }

        public EditDoctorPage(Doctor doctor)
        {
            InitializeComponent();
            roomCmb.ItemsSource = RoomController.GetInstance().GetRooms();
            specializationCmb.ItemsSource = Enum.GetValues(typeof(Specialization)).Cast<Specialization>();
            _doctor = doctor;
            usernameTxt.Text = doctor.Username;
            nameTxt.Text = doctor.Name;
            surnameTxt.Text = doctor.Surname;
            date.SelectedDate = doctor.DateOfBirth;
            numberTxt.Text = doctor.PhoneNumber;
            parentsNameTxt.Text = doctor.ParentsName;
            emailTxt.Text = doctor.Email;
            genderTxt.Text = doctor.Gender;
            jmbgTxt.Text = doctor.Jmbg;
            roomCmb.SelectedItem = doctor.room;
            specializationCmb.SelectedItem = doctor.Specialization;
        }

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            _doctor.Username = usernameTxt.Text;
            _doctor.Name = nameTxt.Text;
            _doctor.Surname = surnameTxt.Text;
            _doctor.DateOfBirth = (DateTime)date.SelectedDate;
            _doctor.PhoneNumber = numberTxt.Text;
            _doctor.ParentsName = parentsNameTxt.Text;
            _doctor.Email = emailTxt.Text;
            _doctor.Gender = genderTxt.Text;
            _doctor.Jmbg = jmbgTxt.Text;
            _doctor.room = (Room)roomCmb.SelectedItem;
            _doctor.Specialization = (Specialization)specializationCmb.SelectedItem;

        }

        private void OtkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new DoctorsCRUDPage();
        }
    }
}
