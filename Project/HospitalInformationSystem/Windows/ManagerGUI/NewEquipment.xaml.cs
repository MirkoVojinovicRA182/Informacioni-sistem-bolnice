using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Utility;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class NewEquipment : Window
    {
        private static NewEquipment instance = null;
        private string _equipmentId;
        private string _equipmentName;
        private TypeOfEquipment _equipmentType;
        private int _equipmentQuantity;
        private string _equipmentDescription;
        public static NewEquipment getInstance()
        {
            if (instance == null)
                instance = new NewEquipment();
            return instance;
        }
        private NewEquipment()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            LoadTypeOfEquipmentComboBox();
        }
        private void LoadTypeOfEquipmentComboBox()
        {
            string[] typesOfEquipment = { Constants.STATIC_EQUIPMENT, Constants.DYNAMIC_EQUIPMENT };
            typeOfEquipmentComboBox.ItemsSource = new List<String>(typesOfEquipment);
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            MakeEquipmentAttributes();
            if(ValidateInputs())
            {
                CreateNewEquipment();
                RefreshEquipmentTables();
                this.Close();
                MessageBox.Show("Uneta je nova oprema u sistem.", "Nova prostorija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void MakeEquipmentAttributes()
        {
            _equipmentId = idTextBox.Text;
            _equipmentName = nameTextBox.Text;
            _equipmentQuantity = int.TryParse(quanitityTextBox.Text, out _equipmentQuantity) ? _equipmentQuantity : 0;
            _equipmentDescription = decriptionTextBox.Text;
            if (typeOfEquipmentComboBox.SelectedIndex == 0)
                _equipmentType = TypeOfEquipment.Static;
            else
                _equipmentType = TypeOfEquipment.Dynamic;
        }
        private bool ValidateInputs()
        {
            if (string.Compare(_equipmentId, "") == 0)
            {
                MessageBox.Show("Polje za unos šifre ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (EquipmentController.getInstance().findEquipmentById(_equipmentId) != null)
            {
                MessageBox.Show("U sistemu postoji oprema sa ovom šifrom!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.Compare(nameTextBox.Text, "") == 0)
            {
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (typeOfEquipmentComboBox.SelectedItem == null)
            {
                MessageBox.Show("Niste odabrali tip opreme!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (_equipmentQuantity == 0)
            {
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void CreateNewEquipment()
        {
            EquipmentController.getInstance().addNewEquipment(new Equipment(_equipmentId, _equipmentName, _equipmentType, _equipmentQuantity, _equipmentDescription));
        }
        private void RefreshEquipmentTables()
        {
            ManagerMainWindow.getInstance().equipmentTable.refreshTable();
            ManagerMainWindow.getInstance().detailEquipmentTable.LoadAllUserControlComponents();
        }
        private void newEquipmentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
