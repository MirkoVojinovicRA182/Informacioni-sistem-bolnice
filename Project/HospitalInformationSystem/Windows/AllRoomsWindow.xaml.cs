using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Model;

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for AllRoomsWindow.xaml
    /// </summary>
    public partial class AllRoomsWindow : Window
    {
        public AllRoomsWindow()
        {
            InitializeComponent();

            showRooms();
        }

        private void showRooms()
        {
            //RoomDataBase roomDataBase = new RoomDataBase();

            //this.roomsTextBox.Text = roomDataBase.GetRoom().Count.ToString();
        }
    }
}
