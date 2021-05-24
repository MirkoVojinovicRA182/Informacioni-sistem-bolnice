using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
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
