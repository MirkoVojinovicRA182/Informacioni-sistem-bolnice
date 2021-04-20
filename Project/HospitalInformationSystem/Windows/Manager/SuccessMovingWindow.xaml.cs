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
    /// Interaction logic for SuccessMovingWindow.xaml
    /// </summary>
    public partial class SuccessMovingWindow : Window
    {
        private static SuccessMovingWindow instance = null;
        
        public static SuccessMovingWindow getInstance(int currentQuantity, int finalQuantity)
        {
            if (instance == null)
                instance = new SuccessMovingWindow(currentQuantity, finalQuantity);
            return instance;
        }
        private SuccessMovingWindow(int currentQuantity, int finalQuantity)
        {
            InitializeComponent();

            currentStateTextBlock.Text = currentQuantity.ToString();
            finalStateTextBlock.Text = finalQuantity.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
