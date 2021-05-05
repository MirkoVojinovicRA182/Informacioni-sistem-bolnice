using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalInformationSystem.Controller
{
    class AnnouncementsController
    {
        private static AnnouncementsController instance = null;
        private AnnouncementsService announcementsService;

        public static AnnouncementsController getInstance()
        {
            if (instance == null)
                instance = new AnnouncementsController();
            return instance;
        }
        private AnnouncementsController()
        {
            announcementsService = new AnnouncementsService();
        }

        public void addAnnouncement(Announcement announcement)
        {
            announcementsService.addAnnouncement(announcement);
        }

        public ObservableCollection<Announcement> getAnnouncements()
        {
            return announcementsService.Announcements;
        }
    }
}
