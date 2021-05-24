using HospitalInformationSystem.Model;
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
    /// Interaction logic for EditAnnouncementPage.xaml
    /// </summary>
    public partial class EditAnnouncementPage : Page
    {
        public Announcement Announcement { get; set; }
        public EditAnnouncementPage()
        {
            InitializeComponent();
        }

        public EditAnnouncementPage(Announcement announcement)
        {
            InitializeComponent();
            Announcement = announcement;
            announcementsTxt.Text = announcement.Text;
        }

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            Announcement.Text = announcementsTxt.Text;
            MainPatientManagement.Instance.MainFrame.Content = new AnnouncementsPage();
        }

        private void OtkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new AnnouncementsPage();
        }
    }
}
