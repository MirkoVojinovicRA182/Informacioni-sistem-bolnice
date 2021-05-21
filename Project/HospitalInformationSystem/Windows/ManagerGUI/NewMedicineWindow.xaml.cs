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
    ///
    
    public partial class NewMedicineWindow : Window
    {
        public const string ERROR = "Greška";
        private static NewMedicineWindow instance = null;
        private List<MedicineIngredient> medicineIngredientList = new List<MedicineIngredient>();
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
            typeOfMedicineList.Add("Pilula");
            typeComboBox.ItemsSource = typeOfMedicineList;

            ObservableCollection<Medicine> replacementMedicinesList = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
            replacementMedicineComboBox.ItemsSource = null;
            replacementMedicineComboBox.ItemsSource = replacementMedicinesList;

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfMedicine typeOfMedicine = TypeOfMedicine.Dilution;
            if (string.Equals((string)typeComboBox.SelectedItem, "Rastvor"))
                typeOfMedicine = TypeOfMedicine.Dilution;
            else if (string.Equals((string)typeComboBox.SelectedItem, "Sirup"))
                typeOfMedicine = TypeOfMedicine.Syrup;
            else if (string.Equals((string)typeComboBox.SelectedItem, "Tableta"))
                typeOfMedicine = TypeOfMedicine.Tablet;
            else if (string.Equals((string)typeComboBox.SelectedItem, "Pilula"))
                typeOfMedicine = TypeOfMedicine.Pill;
            Medicine replacementMedicine;
            if (replacementMedicineComboBox.SelectedItem != null)
                replacementMedicine = (Medicine)replacementMedicineComboBox.SelectedItem;
            else
                replacementMedicine = null;
            if (CheckControlsInputCorrection())
            {
                MedicineController.GetInstance().AddMedicine(new Medicine(int.Parse(idTextBox.Text), nameTextBox.Text, typeOfMedicine, purposeTextBoxt.Text, useTextBox.Text, replacementMedicine, medicineIngredientList));
                ManagerMainWindow.getInstance().medicineTableUserControl.RefreshTable();
                this.Close();
                MessageBox.Show("Unet je novi lek u sistem.", "Novi lek", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void addNewIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            NewIngredientWindow.GetInstance(medicineIngredientList).ShowDialog();
            RefreshIngredientsListBox();
        }
        private void RefreshIngredientsListBox()
        {
            ingredientsListBox.ItemsSource = null;
            ingredientsListBox.ItemsSource = medicineIngredientList;
        }

        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            medicineIngredientList.Remove((MedicineIngredient)ingredientsListBox.SelectedItem);
            RefreshIngredientsListBox();
        }

        private bool CheckControlsInputCorrection()
        {
            if (!IdTextBoxCorrection())
                return CreateErrorMessageBox("Šifra mora biti ceo broj!");
            if (CheckTheExistenceOfId())
                return CreateErrorMessageBox("U sistemu postoji lek sa ovom šifrom!");
            if (!NameTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos imena ne može biti prazno!");
            if (!PurposeTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos namene ne može biti prazno!");
            if (!WayOfUseTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos načina upotrebe ne može biti prazno!");
            if(CheckTheExistenceOfAnIngredient() == 0)
                return CreateErrorMessageBox("Morate uneti bar jedan sastojak!");
            return true;
        }
        private bool NameTextBoxCorrection()
        {
            return !(nameTextBox.Text == "");
        }
        private bool IdTextBoxCorrection()
        {
            int tryParseStringToInt = int.TryParse(idTextBox.Text, out tryParseStringToInt) ? tryParseStringToInt : 0;
            return !(tryParseStringToInt == 0);
        }
        private bool CheckTheExistenceOfId()
        {
            return !(MedicineController.GetInstance().FindMedicineById(int.Parse(idTextBox.Text)) == null);
        }
        private bool PurposeTextBoxCorrection()
        {
            return !(purposeTextBoxt.Text == "");
        }
        private bool WayOfUseTextBoxCorrection()
        {
            return !(useTextBox.Text == "");
        }
        private int CheckTheExistenceOfAnIngredient()
        {
            return ingredientsListBox.Items.Count;
        }
        private bool CreateErrorMessageBox(string result)
        {
            MessageBox.Show(result, ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
}
