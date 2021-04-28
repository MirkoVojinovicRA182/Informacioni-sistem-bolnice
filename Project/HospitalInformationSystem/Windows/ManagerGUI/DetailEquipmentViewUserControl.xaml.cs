using HospitalInformationSystem.Controller;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for DetailEquipmentViewUserControl.xaml
    /// </summary>
    public partial class DetailEquipmentViewUserControl : UserControl
    {
        private ObservableCollection<DetailEquipmentDTO> equipmentList;
        public DetailEquipmentViewUserControl()
        {
            InitializeComponent();
            RefreshTable();
        }
        private ObservableCollection<DetailEquipmentDTO> LoadList()
        {
            equipmentList = new ObservableCollection<DetailEquipmentDTO>();

            //prolazak kroz sve prostorije
            foreach (Room room in RoomController.getInstance().getRooms())
            {
                foreach (DictionaryEntry de in room.Equipment)
                {
                    Equipment eq = EquipmentController.getInstance().findEquipment(de.Key.ToString());
                    equipmentList.Add(new DetailEquipmentDTO(eq.Name, eq.GetStringType, (int)de.Value, room.Name));
                }
            }

            //prolazak kroz magacin
            foreach (Equipment eq in EquipmentController.getInstance().getEquipment())
            {
                if (eq.QuantityInMagacine > 0)
                {
                    equipmentList.Add(new DetailEquipmentDTO(eq.Name, eq.GetStringType, eq.QuantityInMagacine, "Magacin"));
                }
            }

            return equipmentList;
        }
        public void RefreshTable()
        {
            detailEquipmentTable.ItemsSource = null;
            detailEquipmentTable.ItemsSource = LoadList();
        }
    }
}
