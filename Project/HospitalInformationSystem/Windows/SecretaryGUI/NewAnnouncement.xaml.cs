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
    /// Interaction logic for NewAnnouncement.xaml
    /// </summary>
    public partial class NewAnnouncement : Window
    {
        NewsPage ParentWindow { get; set; }
        public Announcement Announcement { get; set; }
        public NewAnnouncement()
        {
            InitializeComponent();
        }

        public NewAnnouncement(Announcement announcement, NewsPage parent)
        {
            InitializeComponent();
            Announcement = announcement;
            announcementsTxt.Text = announcement.Text;
            ParentWindow = parent;
        }

        private void otkaziBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void potvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AnnouncementsController.getInstance().getAnnouncements().Contains(Announcement))
            {
                Announcement.Text = announcementsTxt.Text;
                ParentWindow.announcementsList.Items.Refresh();
                return;
            }
            Announcement announcement = new Announcement(announcementsTxt.Text);
            AnnouncementsController.getInstance().addAnnouncement(announcement);
        }
    }
}
