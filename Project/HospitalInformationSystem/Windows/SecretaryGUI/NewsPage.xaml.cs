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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for News.xaml
    /// </summary>
    public partial class NewsPage : Window
    {
        public NewsPage()
        {
            InitializeComponent();
            announcementsList.ItemsSource = AnnouncementsController.getInstance().getAnnouncements();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsController.getInstance().getAnnouncements().Remove((Announcement)announcementsList.SelectedItem);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            NewAnnouncement newAnnouncement = new NewAnnouncement();
            newAnnouncement.ShowDialog();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            NewAnnouncement newAnnouncement = new NewAnnouncement((Announcement)announcementsList.SelectedItem, this);
            newAnnouncement.ShowDialog();

        }
    }
}
