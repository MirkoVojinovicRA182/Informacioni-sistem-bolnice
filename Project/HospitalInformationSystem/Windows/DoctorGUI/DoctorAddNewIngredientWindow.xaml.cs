using HospitalInformationSystem.Model;
using System.Windows;
using System.Windows.Input;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorAddNewIngredientWindow.xaml
    /// </summary>
    public partial class DoctorAddNewIngredientWindow : Window
    {
        private Medicine _medicineToAddNewIngredient;
        private static DoctorAddNewIngredientWindow instance = null;
        public static DoctorAddNewIngredientWindow GetInstance(Medicine medicineToAddNewIngredient)
        {
            if (instance == null)
                instance = new DoctorAddNewIngredientWindow(medicineToAddNewIngredient);
            return instance;
        }
        private DoctorAddNewIngredientWindow(Medicine medicineToAddNewIngredient)
        {
            InitializeComponent();
            this._medicineToAddNewIngredient = medicineToAddNewIngredient;
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckAllInputs())
                _medicineToAddNewIngredient.Ingredients.Add(
                    new MedicineIngredient(nameTextBox.Text, double.Parse(quantityInHundredGramsTextBox.Text), int.Parse(dailyIntakeTextBox.Text)));
        }
        private bool CheckIngredientNameInput()
        {
            if (nameTextBox.Text.Length < 1)
            {
                MessageBox.Show("Morate uneti naziv!", "Naziv", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckQuantityInput()
        {
            double tryParseStringToDouble = double.TryParse(quantityInHundredGramsTextBox.Text, out tryParseStringToDouble) ? tryParseStringToDouble : 0;
            if (tryParseStringToDouble == 0)
            {
                MessageBox.Show("Greska u kolicini!", "Kolicina", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckRdiInput()
        {
            int tryParseStringToInt = int.TryParse(dailyIntakeTextBox.Text, out tryParseStringToInt) ? tryParseStringToInt : 0;
            if (tryParseStringToInt == 0)
            {
                MessageBox.Show("Greska u PDU!", "PDU", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckAllInputs()
        {
            return CheckIngredientNameInput() && CheckQuantityInput() && CheckRdiInput();
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                if (CheckAllInputs())
                    _medicineToAddNewIngredient.Ingredients.Add(
                        new MedicineIngredient(nameTextBox.Text, double.Parse(quantityInHundredGramsTextBox.Text), int.Parse(dailyIntakeTextBox.Text)));
                MedicineInformationPreview.GetInstance(_medicineToAddNewIngredient).RefreshTable();
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
