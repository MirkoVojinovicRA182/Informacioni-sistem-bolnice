using HospitalInformationSystem.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Controller
{
    class RoomController
    {
        RoomService roomService;
        
        private static RoomController instance = null;

        public static RoomController getInstance()
        {
            if (instance == null)
                instance = new RoomController();
            return instance;
        }
        private RoomController()
        {
            roomService = new RoomService();
        }

        public void saveInFile()
        {
            roomService.saveInFile();
        }

        public void loadFromFile()
        {
            roomService.loadFromFile();
        }

        public List<Room> getRooms()
        {
            return roomService.getRooms();
        }

        public void setRooms(List<Room> rooms)
        {
            roomService.setRooms(rooms);
        }
    }
}
