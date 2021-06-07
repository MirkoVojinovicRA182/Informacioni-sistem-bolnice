using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HospitalInformationSystem.Repository
{
    class FeedbackRepository : IRepository
    {
        List<Feedback> _allFeedbacks;
        public FeedbackRepository() { _allFeedbacks = new List<Feedback>(); }
        public void loadFromFile()
        {
            throw new NotImplementedException();
        }

        public void saveInFile()
        {
            FileStream fs = new FileStream("Feedbacks.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, _allFeedbacks);
            }
            catch (SerializationException e)
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
        public void AddNewFeedback(Feedback newFeedback) { _allFeedbacks.Add(newFeedback); }
    }
}
