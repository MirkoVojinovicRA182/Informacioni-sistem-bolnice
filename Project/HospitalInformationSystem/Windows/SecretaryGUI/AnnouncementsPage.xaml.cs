using HospitalInformationSystem.Controller;
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
    /// Interaction logic for AnnouncementsPage.xaml
    /// </summary>
    public partial class AnnouncementsPage : Page
    {
        public AnnouncementsPage()
        {
            InitializeComponent();
            announcementsList.ItemsSource = AnnouncementsController.getInstance().getAnnouncements();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsController.getInstance().getAnnouncements().Remove((Announcement)announcementsList.SelectedItem);
            announcementsList.Items.Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new AddAnnouncementPage();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPatientManagement.Instance.MainFrame.Content = new EditAnnouncementPage((Announcement)announcementsList.SelectedItem);
        }

    }
}
