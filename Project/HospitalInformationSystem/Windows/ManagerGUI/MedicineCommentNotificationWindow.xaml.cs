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
    /// Interaction logic for MedicineCommentNotificationWindow.xaml
    /// </summary>
    public partial class MedicineCommentNotificationWindow : Window
    {
        private static MedicineCommentNotificationWindow _instance;
        public static MedicineCommentNotificationWindow GetInstance()
        {
            if (_instance == null)
                _instance = new MedicineCommentNotificationWindow();
            return _instance;
        }
        private MedicineCommentNotificationWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lookCommentButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}
