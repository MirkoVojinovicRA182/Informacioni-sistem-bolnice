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
    /// Interaction logic for InsertQuantityOfEquipmentForRemovingWindow.xaml
    /// </summary>
    public partial class InsertQuantityOfEquipmentForRemovingWindow : Window
    {
        private static InsertQuantityOfEquipmentForRemovingWindow instance = null;
        public static bool itSubmitted;
        private static int quantity;
        private int currentQuantity;
        public static InsertQuantityOfEquipmentForRemovingWindow getInstance(int currentQuantity)
        {
            if (instance == null)
                instance = new InsertQuantityOfEquipmentForRemovingWindow(currentQuantity);
            return instance;
        }
        private InsertQuantityOfEquipmentForRemovingWindow(int currentQuantity)
        {
            InitializeComponent();
            itSubmitted = false;
            this.currentQuantity = currentQuantity;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            quantity = int.TryParse(quantityTextBox.Text, out quantity) ? quantity : 0;
            if (quantity != 0 && quantity > 0 && quantity <= currentQuantity)
            {
                itSubmitted = true;
                this.Close();
                instance = null;
            }
            else
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static int getQuantity()
        {
            return quantity;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
