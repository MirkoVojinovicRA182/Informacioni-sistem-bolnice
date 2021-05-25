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
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctorPage : Page
    {
        public AddDoctorPage()
        {
            InitializeComponent();
        }

        private void RoomCmb_Loaded(object sender, RoutedEventArgs e)
        {
            roomCmb.ItemsSource = RoomController.GetInstance().GetRooms();
        }

        private void SpecializationCmb_Loaded(object sender, RoutedEventArgs e)
        {
            specializationCmb.ItemsSource = Enum.GetValues(typeof(Specialization)).Cast<Specialization>();
        }

        private void OtkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new DoctorsCRUDPage();
        }

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorController.getInstance().AddDoctor(new Doctor(usernameTxt.Text, nameTxt.Text, surnameTxt.Text, (DateTime)date.SelectedDate, numberTxt.Text,
                emailTxt.Text, parentsNameTxt.Text, genderTxt.Text, jmbgTxt.Text, (Room)roomCmb.SelectedItem, (Specialization)specializationCmb.SelectedItem));
        }
    }
}
