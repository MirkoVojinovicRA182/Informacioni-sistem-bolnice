using HospitalInformationSystem.Model;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for NewIngredientWindow.xaml
    /// </summary>
    public partial class NewIngredientWindow : MetroWindow
    {
        List<MedicineIngredient> medicineIngredientList;
        private static NewIngredientWindow instance = null;
        public static NewIngredientWindow GetInstance(List<MedicineIngredient> medicineIngredientList)
        {
            if (instance == null)
                instance = new NewIngredientWindow(medicineIngredientList);
            return instance;
        }
        private NewIngredientWindow(List<MedicineIngredient> medicineIngredientList)
        {
            InitializeComponent();
            this.medicineIngredientList = medicineIngredientList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckControlsInputCorrection())
            {
                medicineIngredientList.Add(new MedicineIngredient(nameTextBox.Text, double.Parse(quantityInHundredGramsTextBox.Text), int.Parse(dailyIntakeTextBox.Text)));
                this.Close();
            }
        }
        private bool CheckControlsInputCorrection()
        {
            if (!NameTextBoxCorrection())
                return CreateErrorMessageBox("Niste uneli naziv sastojka!");
            if (!QuantityInHundredGramsTextBoxCorrection())
                return CreateErrorMessageBox("Niste pravilno uneli količinu u 100 grama!");
            if (!DailyIntakeTextBoxCorrection())
                return CreateErrorMessageBox("Niste pravilno uneli preporučeni dnevni unos!");
            return true;
        }
        private bool NameTextBoxCorrection()
        {
            return !(nameTextBox.Text == "");
        }
        private bool QuantityInHundredGramsTextBoxCorrection()
        {
            double tryParseStringToDouble = double.TryParse(quantityInHundredGramsTextBox.Text, out tryParseStringToDouble) ? tryParseStringToDouble : 0;
            return !(tryParseStringToDouble == 0);
        }
        private bool DailyIntakeTextBoxCorrection()
        {
            int tryParseStringToInt = int.TryParse(dailyIntakeTextBox.Text, out tryParseStringToInt) ? tryParseStringToInt : 0;
            return !(tryParseStringToInt == 0);
        }
        private bool CreateErrorMessageBox(string message)
        {
            MessageBox.Show(message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
}
