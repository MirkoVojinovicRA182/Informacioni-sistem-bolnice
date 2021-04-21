using BusinessLogic;
using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Windows.Manager.Help;
using Model;
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
using WorkWithFiles;

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {

        private static ManagerMainWindow instance;
        private RoomsFileManipulation roomsFileManipulation;

        public static ManagerMainWindow getInstance()
        {
            if (instance == null)
                instance = new ManagerMainWindow();
            return instance;
        }
        private ManagerMainWindow()
        {
            InitializeComponent();
            roomsFileManipulation = new RoomsFileManipulation();

            //EquipmentController.getInstance().loadFromFile();
            //roomsFileManipulation.LoadFromFile();

            roomsTable.refreshTable();
            equipmentTable.refreshTable();

        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            instance = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;

            EquipmentController.getInstance().saveInFile();
            roomsFileManipulation.SaveInFile();
        }

        private void newMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (roomTab.IsSelected)
            {
                NewRoomWindow.getInstance().Show();
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                int selectedEquipment;

                if (staticEquipmentTab.IsSelected)
                    selectedEquipment = 1;
                else
                    selectedEquipment = 2;

                NewEquipment.getInstance(selectedEquipment).Show();
            }
        }

        private void mainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsTable.refreshTable();
            equipmentTable.refreshTable();
            dynamicEquipmentTable.refreshTable();
        }

        private void staticDynamicTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roomsTable.refreshTable();
            equipmentTable.refreshTable();
            dynamicEquipmentTable.refreshTable();
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (roomTab.IsSelected) //tab prostorije
            {
                SelectRoomWindow.getInstance(2).Show();
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                SelectEquipmentWindow.getInstance(2).Show();
            }
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (roomTab.IsSelected) //tab prostorije
            {
                SelectRoomWindow.getInstance(1).Show();
            }
            else if (equipmentTab.IsSelected)//tab oprema
            {
                SelectEquipmentWindow.getInstance(1).Show();
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
