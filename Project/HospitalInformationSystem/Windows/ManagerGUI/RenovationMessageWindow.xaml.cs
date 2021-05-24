using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class RenovationMessageWindow : Window
    {
        private static RenovationMessageWindow instance = null;
        public static RenovationMessageWindow GetInstance()
        {
            if (instance == null)
                instance = new RenovationMessageWindow();
            return instance;
        }
        private RenovationMessageWindow()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
