using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System;
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
    /// Interaction logic for NewMedicineWindow.xaml
    /// </summary>
    public partial class NewMedicineWindow : Window
    {
        private static NewMedicineWindow instance = null;
        public static NewMedicineWindow GetInstance()
        {
            if (instance == null)
                instance = new NewMedicineWindow();
            return instance;
        }
        private NewMedicineWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            List<String> typeOfMedicineList = new List<String>();
            typeOfMedicineList.Add("Rastvor");
            typeOfMedicineList.Add("Sirup");
            typeOfMedicineList.Add("Tableta");
            typeComboBox.ItemsSource = typeOfMedicineList;

            ObservableCollection<Medicine> replacementMedicinesList = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
            replacementMedicineComboBox.ItemsSource = null;
            replacementMedicineComboBox.ItemsSource = replacementMedicinesList;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfMedicine typeOfMedicine = TypeOfMedicine.Rastvor;
            if (string.Equals((string)typeComboBox.SelectedItem, "Rastvor"))
                typeOfMedicine = TypeOfMedicine.Rastvor;
            else if (string.Equals((string)typeComboBox.SelectedItem, "Sirupu"))
                typeOfMedicine = TypeOfMedicine.Sirup;
            else if (string.Equals((string)typeComboBox.SelectedItem, "Sirupu"))
                typeOfMedicine = TypeOfMedicine.Tableta;

            MedicineController.GetInstance().AddMedicine(new Medicine(int.Parse(idTextBox.Text), nameTextBox.Text, typeOfMedicine, purposeTextBoxt.Text, useTextBox.Text, null));
            ManagerMainWindow.getInstance().medicineTableUserControl.RefreshTable();
        }
    }
}
