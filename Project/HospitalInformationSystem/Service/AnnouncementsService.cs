using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    class AnnouncementsService
    {
        public ObservableCollection<Announcement> Announcements { get; set; }

        public AnnouncementsService()
        {
            Announcements = new ObservableCollection<Announcement>();
        }

        public void addAnnouncement(Announcement announcement)
        {
            Announcements.Add(announcement);
        }
    }
}
