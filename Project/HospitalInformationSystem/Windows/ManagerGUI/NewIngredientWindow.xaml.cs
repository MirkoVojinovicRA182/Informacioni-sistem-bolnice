using HospitalInformationSystem.Model;
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
    /// Interaction logic for NewIngredientWindow.xaml
    /// </summary>
    public partial class NewIngredientWindow : Window
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
            medicineIngredientList.Add(new MedicineIngredient(nameTextBox.Text, int.Parse(quantityInHundredGramsTextBox.Text), int.Parse(dailyIntakeTextBox.Text)));
            this.Close();
        }
    }
}
