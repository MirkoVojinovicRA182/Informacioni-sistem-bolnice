using Model;
namespace HospitalInformationSystem.DTO
{
    public class RoomDTO
    {
        public RoomDTO(int id, string name, int floor, TypeOfRoom type)
        {
            Id = id;
            Name = name;
            Floor = floor;
            Type = type;
        }
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public int Floor
        {
            get; set;
        }
        public TypeOfRoom Type
        {
            get; set;
        }
    }
}
