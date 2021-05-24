using HospitalInformationSystem.Controller;
using HospitalInformationSystem.DTO;
using HospitalInformationSystem.Utility;
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

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {
        private static EditEquipment instance = null;
        private Equipment _selectedEquipment;
        private int _oldQuantity;
        private string _equipmentName;
        private TypeOfEquipment _equipmentType;
        private int _newEquipmentQuantity;
        private string _equipmentDescription;
        public static EditEquipment getInstance(Equipment equipment)
        {
            if (instance == null)
                instance = new EditEquipment(equipment);
            return instance;
        }
        private EditEquipment(Equipment equipment)
        {
            InitializeComponent();
            this._selectedEquipment = equipment;
            fillControls();
        }
        private void fillControls()
        {
            Room magacine = RoomController.GetInstance().GetMagacine();
            nameTextBox.Text = _selectedEquipment.Name;
            quanitityTextBox.Text = magacine.EquipmentInRoom.Equipment[_selectedEquipment.Id].ToString();
            descriptionTextBox.Text = _selectedEquipment.Description;
            loadTypeComboBox();
            _oldQuantity = (int)magacine.EquipmentInRoom.Equipment[_selectedEquipment.Id];
        }
        private void loadTypeComboBox()
        {
            string[] typesOfEquipment = { Constants.STATIC_EQUIPMENT, Constants.DYNAMIC_EQUIPMENT };
            typeComboBox.ItemsSource = new List<String>(typesOfEquipment);
            if (_selectedEquipment.Type == TypeOfEquipment.Static)
                typeComboBox.SelectedIndex = 0;
            else
                typeComboBox.SelectedIndex = 1;
        }
        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            MakeEquipmentAttributes();
            if(ValidateInputs())
            {
                UpdateEquipment();
                ManagerMainWindow.getInstance().equipmentTable.refreshTable();
                this.Close();
                MessageBox.Show("Informacije o opremi su sada izmenjene.", "Izmena prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void MakeEquipmentAttributes()
        {
            _equipmentName = nameTextBox.Text;
            _newEquipmentQuantity = int.TryParse(quanitityTextBox.Text, out _newEquipmentQuantity) ? _newEquipmentQuantity : 0;
            _equipmentDescription = descriptionTextBox.Text;
            if (typeComboBox.SelectedIndex == 0)
                _equipmentType = TypeOfEquipment.Static;
            else
                _equipmentType = TypeOfEquipment.Dynamic;
        }
        private bool ValidateInputs()
        {
            if (string.Compare(nameTextBox.Text, "") == 0)
            {
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (_newEquipmentQuantity == 0 || _newEquipmentQuantity < 0)
            {
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void UpdateEquipment()
        {
            _selectedEquipment.UpdateProperties(new EquipmentDTO(_equipmentName, _equipmentType, _equipmentDescription, _newEquipmentQuantity, _oldQuantity));
            RoomController.GetInstance().GetMagacine().EquipmentInRoom.Equipment[_selectedEquipment.Id] = _newEquipmentQuantity;
        }
        public void checkControls()
        {
            int quantityInMagacine = int.TryParse(quanitityTextBox.Text, out quantityInMagacine) ? quantityInMagacine : 0;

            if (nameTextBox.Text != _selectedEquipment.Name || (string)typeComboBox.SelectedItem != _selectedEquipment.GetStringType
                || descriptionTextBox.Text != _selectedEquipment.Description)
                changeButton.IsEnabled = true;
            else
                changeButton.IsEnabled = false;
        }
        private void nameTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }
        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkControls();
        }
        private void quanitityTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }
        private void descriptionTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
