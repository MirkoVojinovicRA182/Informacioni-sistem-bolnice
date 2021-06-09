using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Model
{
    class HospitalResidenceParticipants : IHospitalResidence
    {
        private IHospitalResidence _hospitalResidence;
        protected List<Person> _hospitalResidenceParticipants;

        public HospitalResidenceParticipants(IHospitalResidence hospitalResidence)
        {
            this._hospitalResidence = hospitalResidence;
        }
        public void AddNewParticipant(Person newParticipant)
        {
            _hospitalResidenceParticipants.Add(newParticipant);
        }
        public List<Person> GetAllParticipants()
        {
            return _hospitalResidenceParticipants;
        }
        public void RemoveParticipant(Person personToRemove)
        {
            _hospitalResidenceParticipants.Remove(personToRemove);
        }
        public void ChangeResidence(IHospitalResidence newResidence)
        {
            _hospitalResidence.ChangeResidence(newResidence);
        }
    }
}
