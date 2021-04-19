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
    /// Interaction logic for InsertQuantityOfEquipmentForRemovingWindow.xaml
    /// </summary>
    public partial class InsertQuantityOfEquipmentForRemovingWindow : Window
    {
        private static InsertQuantityOfEquipmentForRemovingWindow instance = null;
        public static bool itSubmitted;
        private static int quantity;
        public static InsertQuantityOfEquipmentForRemovingWindow getInstance()
        {
            if (instance == null)
                instance = new InsertQuantityOfEquipmentForRemovingWindow();
            return instance;
        }
        private InsertQuantityOfEquipmentForRemovingWindow()
        {
            InitializeComponent();
            itSubmitted = false;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            itSubmitted = true;
            quantity = int.Parse(quantityTextBox.Text);
            this.Close();
            instance = null;
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
