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
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for AddEquipmentToRoomWindow.xaml
    /// </summary>
    public partial class AddEquipmentToRoomWindow : Window
    {
        private static AddEquipmentToRoomWindow instance = null;
        private ObservableCollection<Equipment> equipmentList;
        private Equipment selectedEquipment;
        private int quantity;
        private string window;

        public static AddEquipmentToRoomWindow getInstance(Hashtable roomEq, string equipmentType, string window)
        {
            if (instance == null)
                instance = new AddEquipmentToRoomWindow(roomEq, equipmentType, window);
            return instance;
        }
        private AddEquipmentToRoomWindow(Hashtable roomEq, string equipmentType, string window)
        {
            InitializeComponent();
            loadEquipment(equipmentType, roomEq);
            this.window = window;
        }

        private void loadEquipment(string equipmentType, Hashtable roomEq)
        {

            if (string.Equals(equipmentType, "dinamicka"))
            {
                List<Equipment> list = EquipmentController.getInstance().getDynamicEquipment();
                //ako u sistemu nema dinamicke opreme nema smisla bilo sta prikazivati, ovde se funkcija zaustavlja
                if (list.Count == 0)
                    return;
                foreach(Equipment eq in list)
                {
                    if (/*eq.QuantityInMagacine == 0*/(int)RoomController.GetInstance().GetMagacine().EquipmentInRoom.Equipment[eq.Id] == 0)
                        list.Remove(eq);
                    if (list.Count == 0)
                        break;
                }
                equipmentList = new ObservableCollection<Equipment>(list);
                foreach (DictionaryEntry de in roomEq)
                {
                    equipmentList.Remove(EquipmentController.getInstance().findEquipmentById(de.Key.ToString()));
                }
            }
            else
            {
                List<Equipment> list = EquipmentController.getInstance().getStaticEquipment();
                //ako u sistemu nema staticke opreme nema smisla bilo sta prikazivati, ovde se funkcija zaustavlja
                if (list.Count == 0)
                    return;
                foreach (Equipment eq in list)
                {
                    if (/*eq.QuantityInMagacine == 0*/(int)RoomController.GetInstance().GetMagacine().EquipmentInRoom.Equipment[eq.Id] == 0)
                        list.Remove(eq);
                    if (list.Count == 0)
                        break;
                }
                equipmentList = new ObservableCollection<Equipment>(list);
                foreach (DictionaryEntry de in roomEq)
                {
                    equipmentList.Remove(EquipmentController.getInstance().findEquipmentById(de.Key.ToString()));
                }
            }

            equipmentListBox.ItemsSource = null;
            equipmentListBox.ItemsSource = equipmentList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (equipmentListBox.SelectedItem != null)
            {
                selectedEquipment = (Equipment)equipmentListBox.SelectedItem;
                quantity = int.TryParse(quantityTextBox.Text, out quantity) ? quantity : 0;
                if (quantity != 0 && quantity > 0 && quantity <= /*eq.QuantityInMagacine == 0*/(int)RoomController.GetInstance().GetMagacine().EquipmentInRoom.Equipment[selectedEquipment.Id])
                {
                    if (string.Equals(window, "newRoom"))
                        NewRoomWindow.getInstance().addEquipment(selectedEquipment.Id, quantity);
                    else
                        EditRoomWindow.getInstance((Room)ManagerMainWindow.getInstance().roomsUserControl.allRoomsTable.SelectedItem).addEquipment(selectedEquipment.Id, quantity);

                    this.Close();

                }
                else
                    MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Niste odabrali opremu!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            instance = null;
        }
    }
}
