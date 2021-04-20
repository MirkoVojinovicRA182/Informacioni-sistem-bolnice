using HospitalInformationSystem.Controller;
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

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {

        private static EditEquipment instance = null;
        private Equipment selectedEquipment;
        private int oldQuantity;

        public static EditEquipment getInstance(Equipment equipment)
        {
            if (instance == null)
                instance = new EditEquipment(equipment);
            return instance;
        }
        private EditEquipment(Equipment equipment)
        {
            InitializeComponent();
            this.selectedEquipment = equipment;
            fillControls();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;
            string name = nameTextBox.Text;
            TypeOfEquipment typeOfEquipment;
            if (typeComboBox.SelectedIndex == 0)
                typeOfEquipment = TypeOfEquipment.Static;
            else if (typeComboBox.SelectedIndex == 1)
                typeOfEquipment = TypeOfEquipment.Dynamic;
            else
                typeOfEquipment = selectedEquipment.Type;
            int quantityInMagacine = int.Parse(quanitityTextBox.Text);
            string description = descriptionTextBox.Text;

            EquipmentController.getInstance().changeEquipment(selectedEquipment, id, name, typeOfEquipment, quantityInMagacine, oldQuantity, description);

            ManagerMainWindow.getInstance().equipmentTable.refreshTable();
            ManagerMainWindow.getInstance().dynamicEquipmentTable.refreshTable();

            this.Close();
            MessageBox.Show("Informacije o opremi su sada izmenjene.", "Izmena prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void fillControls()
        {
            idTextBox.Text = selectedEquipment.Id;
            nameTextBox.Text = selectedEquipment.Name;
            quanitityTextBox.Text = selectedEquipment.QuantityInMagacine.ToString();
            descriptionTextBox.Text = selectedEquipment.Description;

            oldQuantity = selectedEquipment.QuantityInMagacine;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            instance = null;

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
