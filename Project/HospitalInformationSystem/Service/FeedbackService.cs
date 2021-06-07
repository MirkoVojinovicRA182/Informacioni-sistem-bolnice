using HospitalInformationSystem.Model;
using HospitalInformationSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    public class FeedbackService
    {
        private FeedbackRepository _repository;
        public FeedbackService() { _repository = new FeedbackRepository(); }
        public void SerializeFeedback() { _repository.saveInFile(); }
        public void DeserializeFeedback() { _repository.loadFromFile(); }
        public void AddNewFeedback(Feedback newFeedback) { _repository.AddNewFeedback(newFeedback); }
    }
}
