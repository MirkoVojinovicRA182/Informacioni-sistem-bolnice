using HospitalInformationSystem.Model;
using HospitalInformationSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    public class FeedbackController
    {
        private static FeedbackController _instance = null;
        private FeedbackService _service;
        public static FeedbackController GetInstance()
        {
            if (_instance == null)
                _instance = new FeedbackController();
            return _instance;
        }
        private FeedbackController() { _service = new FeedbackService(); }
        public void AddNewFeedback(Feedback newFeedback) { _service.AddNewFeedback(newFeedback); }
        public void SerializeFeedback() { _service.SerializeFeedback(); }
        public void DeserializeFeedback() { _service.DeserializeFeedback(); }
    }
}
