using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Repository
{
    class RoomRepo : IRepo
    {
        public List<Room> Rooms { get; set; }
        public RoomRepo() { Rooms = new List<Room>(); }
        public void AddNew(object element)
        {
            Rooms.Add((Room)element);
        }
        public void DeleteOne(object element)
        {
            Rooms.Remove((Room)element);
        }
        public bool Exists(int id)
        {
            foreach (Room room in Rooms)
            {
                if (room.Id == id)
                    return true;
            }
            return false;
        }
        public void Serialize()
        {
            throw new NotImplementedException();
        }
        public void Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}
