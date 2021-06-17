using MahApps.Metro.Controls;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class RenovationMessageWindow : MetroWindow
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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
