using HospitalInformationSystem.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Service
{
    public class RoomSer
    {
        private IRepo repo;
        public RoomSer(IRepo repo) { this.repo = repo;}
        public List<Room> GetAll() { return repo.Rooms; }
    }
}
