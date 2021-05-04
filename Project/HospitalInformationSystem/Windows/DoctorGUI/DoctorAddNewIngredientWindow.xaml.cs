using HospitalInformationSystem.Model;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for DoctorAddNewIngredientWindow.xaml
    /// </summary>
    public partial class DoctorAddNewIngredientWindow : Window
    {
        private Medicine medicine;
        public DoctorAddNewIngredientWindow(Medicine medicine)
        {
            this.medicine = medicine;
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInputs())
                medicine.Ingredients.Add(new MedicineIngredient(nameTextBox.Text, double.Parse(quantityInHundredGramsTextBox.Text), int.Parse(dailyIntakeTextBox.Text)));
        }

        private bool CheckInputs()
        {
            if(nameTextBox.Text.Length < 1)
            {
                MessageBox.Show("Morate uneti naziv!", "Naziv", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            double tryParseStringToDouble = double.TryParse(quantityInHundredGramsTextBox.Text, out tryParseStringToDouble) ? tryParseStringToDouble : 0;
            if(tryParseStringToDouble == 0)
            {
                MessageBox.Show("Greska u kolicini!", "Kolicina", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            int tryParseStringToInt = int.TryParse(dailyIntakeTextBox.Text, out tryParseStringToInt) ? tryParseStringToInt : 0;
            if(tryParseStringToInt == 0)
            {
                MessageBox.Show("Greska u PDU!", "PDU", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
